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
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;


namespace ELIMPERIOBARYCAFE.MVVM.ViewModels
{
    public partial class ProductViewModel: ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página

        private readonly string _token;
        private readonly string? _productoid;
        public readonly string productos;

        //Propiedades para el Binding de Comandos
        public ICommand CrearCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand GetCommand { get; }

        public ProductViewModel(Page page, string token)
        {
            _httpClient = new HttpClient();
            _page = page;
             _httpClient.BaseAddress = new Uri("http://10.0.2.2:5002");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _token = token;

            //Crear comandos manualmente 
            CrearCommand = new Command(async () => await RegisterProd());
            GetCommand = new Command(async () => await GetProductosAsync());
        }

        [ObservableProperty]
        private string descripcion;
        [ObservableProperty]
        private double precio;
        [ObservableProperty]
        private int stock;
        [ObservableProperty]
        private string categoria;

        //Tarea Async para crear productos 
        public async Task RegisterProd()
        {
            var prod = new Producto //Se crea una variable donde se guardaran los datos del modelo
            {
                Descripcion = descripcion,
                Precio = precio,
                Stock = stock,
                Categoria = categoria

            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://10.0.2.2:5002/api/Producto/Create", prod);
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

        private  ObservableCollection<Producto> producto { get; set; } = new ObservableCollection<Producto>();

        public async Task GetProductosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://10.0.2.2:5002/api/Producto/Get");
                if (response.IsSuccessStatusCode)
                {
                    var productos = await response.Content.ReadFromJsonAsync<List<Producto>>();
                    producto.Clear();
                    foreach (var prod in productos)
                    {
                        producto.Add(prod);
                    }
                }
                else
                {
                    await _page.DisplayAlert("Error", "Error al obtener productos", "OK");
                }
            }
            catch (Exception ex)
            {
                await _page.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }




    }
}
