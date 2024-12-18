﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using ElimperioAPI.Models;
using ElimperioAPI.Services;
using Microsoft.AspNetCore.Identity.Data;


namespace UniveridadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {

            var existingUser = await _userService.ObtenerUsuarioAsync(user.Username);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.Contraseña, existingUser.Contraseña))
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas" });
            }

            var token = GenerateJwtToken(existingUser.Username);
            return Ok(new { Token = token });
        }
    
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {

            var existingUser = await _userService.ObtenerUsuarioAsync(user.Username);
            if (existingUser != null)
                return Conflict("El usuario ya existe.");

            // Encriptamos la contraseña antes de guardarla
            user.Contraseña = BCrypt.Net.BCrypt.HashPassword(user.Contraseña);
            await _userService.CrearUsuarioAsync(user);
            return Ok("Usuario registrado exitosamente.");
        }

        private string GenerateJwtToken(string username) 
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
