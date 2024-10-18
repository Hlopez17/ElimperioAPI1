using ElimperioAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ElimperioAPI.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.Configure<ImperioDBsettings>(
builder.Configuration.GetSection("ConfiguracionBaseDatos"));

builder.Services.AddSingleton<ProductoService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ReservaServices>();

builder.Services.AddControllers()
.AddJsonOptions(
options => options.JsonSerializerOptions.PropertyNamingPolicy =
null);



// Add services to the container.

// Configuración de JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // Habilita la autenticación
app.UseAuthorization(); // Habilita la autorización
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

