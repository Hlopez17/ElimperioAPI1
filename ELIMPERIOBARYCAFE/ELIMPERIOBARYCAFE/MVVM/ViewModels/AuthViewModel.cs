using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets;
using ELIMPERIOBARYCAFE.MVVM.View;
using System.Text.Json;
using ELIMPERIOBARYCAFE.MVVM.Models;

namespace ELIMPERIOBARYCAFE.MVVM.ViewModels
{
    public partial class AuthViewModel: ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página

        //Propiedades para el binding de comandos
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        
        public AuthViewModel(Page page)
        {
            _httpClient = new HttpClient();
            _page = page;

            //Crear comandos manualmente 
            RegisterCommand = new Command(async () => await RegisterAsync());
            LoginCommand = new Command(async () => await LoginAsync());
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty] 
        private string contraseña;
        [ObservableProperty]
        private string nombre;
        [ObservableProperty]
        private string rol;
        [ObservableProperty]
        private string email;

        //Tarea Async para registrar
        public async Task RegisterAsync()
        {
            var user = new User
            {
                Nombre_Completo = nombre,
                Username = username,
                Contraseña = contraseña,
                Email = email,
                Rol = rol

            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5002/api/Auth/Register", user);

                if (response.IsSuccessStatusCode)
                {
                    await _page.DisplayAlert("Registro", "Registro creado exitosamente!", "OK");
                    //╚╔╩╦╚╔╩╦D //🚂-🚃-🚃-🚃
                }
                else
                {
                    await _page.DisplayAlert("Error", "Ocurrió un error al crear el registro", "OK");
                }
            }
            catch (Exception ex)
            {
                await _page.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        public async Task LoginAsync()
        {
            var user = new User
            {
                Username = username,
                Contraseña = contraseña
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://10.0.2.2:5002/api/Auth/login", user);

                
                if (response.IsSuccessStatusCode)

                {
                    // Obtener el token de la respuesta
                    //var token = await response.Content.ReadAsStringAsync();

                    // Deserializar la respuesta para obtener solo el token
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var tokenObj = JsonSerializer.Deserialize<TokenResponse>(jsonResponse);
                    var token = tokenObj.Token; // Extraer solo el valor del token

                    // Almacenar el token (puedes usar una propiedad o variable estática, depende de cómo manejes tu estado)
                    //Token = token;

                    // Mostrar una alerta para notificar el éxito del login
                    //await _page.DisplayAlert("Login", "Login exitoso!. Tu token es: " + token.ToString(), "OK");

                    // Redirigir a la vista de estudiantes pasando el token como parámetro
                    //await _page.Navigation.PushAsync(new EstudianteView(token));

                    try
                    {
                        await _page.Navigation.PushAsync(new PresentacionInicio(token));
                    }
                    catch (Exception ex)
                    {
                        await _page.DisplayAlert("Error", $"Ocurrió un error al cargar la vista de estudiantes: {ex.Message}", "OK");
                    }
                }
                else
                {
                    await _page.DisplayAlert("Error", "Credenciales Incorrectas", "OK");
                }
            
            }
            catch (Exception ex)
            {
                await _page.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
    }

}
          
