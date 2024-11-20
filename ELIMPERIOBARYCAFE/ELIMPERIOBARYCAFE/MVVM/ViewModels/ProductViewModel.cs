using CommunityToolkit.Mvvm.ComponentModel;
using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.View.Products;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Input;

namespace ELIMPERIOBARYCAFE.MVVM.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página
        public ObservableCollection<Producto> Producto { get; } = new ObservableCollection<Producto>();
        private readonly string _token;
        private readonly string? _productoid;
        public readonly string productos;

        //public string Descripcion {  get; set; }
        //public double Precio { get; set; }
        //public string Categoria { get; set; }
        //public int Stock { get; set; }

        //Propiedades para el Binding de Comandos
        public ICommand CrearCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand GetCommand { get; }
        public ICommand VerCommand { get; }

        public ProductViewModel(Page page, string token)
        {
            _httpClient = new HttpClient();
            _page = page;
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:5002");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _token = token;
            string id;
            //Crear comandos manualmente 
            CrearCommand = new Command(async () => await RegisterProd());
            GetCommand = new Command(async () => await GetProductosAsync());
            EliminarCommand = new Command<Producto>(async producto => await EliminarProducto(producto));
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


        // Método para eliminar un producto
        public async Task EliminarProducto(Producto producto)
        {
            var confirmado = await _page.DisplayAlert("Eliminar", $"¿Desea eliminar el producto {producto.Descripcion}?", "Sí", "No");
            if (confirmado)
            {
                var response = await _httpClient.DeleteAsync($"api/Producto/{producto.Id}");
                if (response.IsSuccessStatusCode)
                {
                    Producto.Remove(producto);
                    await _page.DisplayAlert("Eliminar", "Producto eliminado exitosamente.", "OK");
                }
                else
                {
                    await _page.DisplayAlert("Error", "Error al eliminar el producto", "OK");
                }
            }
        }
        public async Task EditClientesAsync(string id)
        {
            // Navegar a la vista de editar Producto
            await _page.Navigation.PushAsync(new EditProducts(id));
        }


        //public async Task Crearcliente()
        //{
        //    await _page.Navigation.PushAsync(new CrearCliente());
        //}
        //public async Task StoreCliente()
        //{
        //    try
        //    {
        //        var nuevoCliente = new Clientes
        //        {
        //            Nombre = Nombre ?? string.Empty,
        //            Cedula = Cedula ?? string.Empty,
        //            Telefono = Telefono,
        //            Correo = Correo ?? string.Empty,
        //            Password = Password ?? string.Empty,
        //            Estado = "Activo",
        //            Direccion = Direccion ?? string.Empty,
        //            Fecha = Fecha,
        //            HoraReservacion = HoraReservacion.ToString(),  // Convertir a string
        //            HoraFinanReservacion = HoraFinanReservacion.ToString()  // Convertir a string
        //        };

        //        var response = await _httpClient.PostAsJsonAsync("/api/ControllerCliente", nuevoCliente);
        //        response.EnsureSuccessStatusCode();

        //        Clientes.Add(nuevoCliente);
        //        GetCustomers();
        //        await App.Current.MainPage.DisplayAlert("Éxito", "Cliente guardado correctamente", "OK");
        //        await Application.Current.MainPage.Navigation.PopAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Aquí puedes usar un sistema de logging, por ejemplo, Serilog o NLog
        //        System.Diagnostics.Debug.WriteLine($"Error al guardar el cliente: {ex}");

        //        await App.Current.MainPage.DisplayAlert("Error", $"No se pudo guardar el cliente: {ex.Message}", "OK");

        //    }
        //}

        public async Task GetProductosAsync()
        {

            try
            {
                var response = await _httpClient.GetAsync("http://10.0.2.2:5002/api/Producto/Get");
                if (response.IsSuccessStatusCode)
                {
                    var productos = await response.Content.ReadFromJsonAsync<List<Producto>>();
                    Producto.Clear();
                    foreach (var prod in productos)
                    {
                        Producto.Add(prod);
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

        //response.EnsureSuccessStatusCode();
        //var productos = await response.Content.ReadFromJsonAsync<List<Producto>>();
        ////Producto.Clear();
        //if(productos != null)
        //{
        //    foreach (var item in productos)
        //    {
        //        Producto.Add(item);
        //    }

        //}



    }
}
