
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
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace ELIMPERIOBARYCAFE.MVVM.ViewModels
{
    public partial class SupplierViewModel :ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página

        private readonly string _token;

        //Propiedades para el Binding de Comandos
        public ICommand CrearCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand GetCommand { get; }

        public SupplierViewModel(Page page, string token)
        {
            _httpClient = new HttpClient();
            _page = page;
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:5002");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _token = token;

            //Crear comandos manualmente
            CrearCommand = new Command(async () => await RegisterProv());
            //GetCommand = new Command(async () => await GetProductosAsync());
        }

        [ObservableProperty]
        public string razonsocial;
        [ObservableProperty]
        public string representante;
        [ObservableProperty]
        public int telefono;
        [ObservableProperty]
        public string direccion;


        //Tarea Async para crear productos 
        public async Task RegisterProv()
        {
            var prov = new Proveedor //Se crea una variable donde se guardaran los datos del modelo
            {
                Razon_Social = razonsocial,
                Representante = representante,
                Telefono = telefono,
                Direccion = direccion

            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://10.0.2.2:5002/api/Proveedor/Create", prov);
                if (response.IsSuccessStatusCode)
                {
                    await _page.DisplayAlert("Registro", "Producto Creado exitosamente!", "OK");
                }
                else
                {
                    await _page.DisplayAlert("Error", "Error en el registro", "OK");
                }
            }
            catch (Exception ex)
            {
                await _page.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

    }
}
