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

        //Propiedades para el Binding de Comandos
        public ICommand CrearCommand { get; }
        public ICommand EliminarCommand { get; }

        public ProductViewModel(Page page, string token, string? productoid = null)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5002") };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _page = page;
            _token = token;
            _productoid = productoid;

            //CalificacionesDinamicas = new ObservableCollection<CalificacionDinamica>();

            //AddCalificacionGroupCommand = new Command(AddCalificacionGroup);
            //SaveNuevaCalificacionCommand = new Command(async () => await SaveNuevaCalificacionAsync());
            //SaveEstudianteCommand = new Command(async () => await SaveEstudianteAsync());
            //DeleteCalificacionCommand = new Command<Calificacion>(async (calificacion) => await DeleteCalificacionAsync(calificacion));
            //EditCalificacionCommand = new Command<Calificacion>(EditCalificacion);

            //IsAddingCalificacion = false; // Iniciar en falso

            //if (!string.IsNullOrEmpty(_estudianteId))
            //{
            //    LoadEstudianteCommand = new Command(async () => await LoadEstudianteAsync());
            //    LoadEstudianteCommand.Execute(null);
            //}
        }

        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private string precio;
        [ObservableProperty]
        private string stock;

        //Tarea Async para crear productos 
        public async Task RegisterProd()
        {
            var prod = new Producto
            {
                Descripcion = description,
                Precio = precio,
                Stock = stock
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5002/api/Producto/Create", prod);
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
