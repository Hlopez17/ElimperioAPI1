using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
using System.Collections.ObjectModel;

using System;
using System.Linq;
using System.Net.Http.Headers;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ELIMPERIOBARYCAFE.MVVM.ViewModels
{
    public partial class PedidoViewModel: ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página

        private readonly string _token;
        private readonly string? _productoid;

        // Diccionario para almacenar pedidos temporales por mesa
        private readonly Dictionary<int, ObservableCollection<Pedido>> _pedidosPorMesa
            = new Dictionary<int, ObservableCollection<Pedido>>();
        //Funciona para llamar a la colección en la vista
        public ObservableCollection<Producto> Producto { get; } = new ObservableCollection<Producto>();
        //Se define una variable que este relacionada con la clase producto
        private Producto _productoSeleccionado;

        private int _mesaSeleccionada;
       
        public Producto ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                _productoSeleccionado = value;
                OnPropertyChanged();
                RellenarCampos(); // Rellena los campos cuando se selecciona un producto
            }
        }

        public int MesaSeleccionada
        {
            get => _mesaSeleccionada;
            set
            {
                _mesaSeleccionada = value;
                OnPropertyChanged(nameof(MesaSeleccionada));
                CargarPedidosTemporales(); // Cargar pedidos de la mesa seleccionada
            }
        }

        //Se crean los comandos, para luego asignarles una función
        public ICommand CrearMesa {  get;}
        public ICommand CancelMesa {  get; }
        public ICommand AgregarPedidoCommand { get; }

        public PedidoViewModel(Page page, string token)
        {
            _httpClient = new HttpClient();
            _page = page;
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:5002");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _token = token;
            GetProductosAsync();

            //Crear comandos manualmente 
            CrearMesa = new Command(async () => await FinalizarVentaAsync());
            AgregarPedidoCommand = new Command(AgregarPedido);
            //GetCommand = new Command(async () => await GetProductosAsync());
            //EliminarCommand = new Command<Producto>(async producto => await EliminarProducto(producto));
        }

        //Datos de Mesas
        [ObservableProperty]
        private int numeromesa;
        [ObservableProperty]
        private DateTime fecha; 
        [ObservableProperty]
        private int total;
        //Datos de pedido
        [ObservableProperty]
        private string productop;
        [ObservableProperty]
        private int cantidad;
        [ObservableProperty]
        private decimal precio;
        [ObservableProperty]
        private decimal importe;

       //Tarea async para cargar los productos en PedidoViewModel
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

        //Función para rellenar los campos que se muestran en el Xaml
        private void RellenarCampos()
        {
            if (ProductoSeleccionado != null)
            {
                Precio = (decimal)ProductoSeleccionado.Precio;
                OnPropertyChanged(nameof(Precio));
                Productop = ProductoSeleccionado.Descripcion;
                OnPropertyChanged(nameof(Productop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //Función que permite que los datos cambien en "RellenarCampos"
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //Sirve para almacenar los pedidos, de manera temporal antes de ser creados
        public ObservableCollection<Pedido> PedidosTemporales { get; set; } = new ObservableCollection<Pedido>();


        private void AgregarPedido()
        {
            if (Cantidad <= 0 || Precio <= 0)
            {
                // Alerta o validación opcional
                return;
            }

            var pedido = new Pedido
            {
                Id = PedidosTemporales.Count + 1,  // Autoincremental local
                Productop = Productop,
                Cantidad = Cantidad,
                Precio = Precio,
                Importe = Cantidad * Precio,
                Estado = "En preparación"
            };

            PedidosTemporales.Add(pedido);

            // Estos es para limpiar los valores después de agregar
            productop = string.Empty;
            Cantidad = 0;
            Precio = 0;
            OnPropertyChanged(nameof(Producto));
            OnPropertyChanged(nameof(Cantidad));
            OnPropertyChanged(nameof(Precio));
            OnPropertyChanged(nameof(total));
        }

        private void CargarPedidosTemporales()
        {
            // Si hay pedidos guardados para esta mesa, cárgalos
            if (_pedidosPorMesa.TryGetValue(MesaSeleccionada, out var pedidos))
            {
                PedidosTemporales = pedidos;
            }
            else
            {
                // Si no hay pedidos, inicializa la lista para esta mesa
                PedidosTemporales = new ObservableCollection<Pedido>();
                _pedidosPorMesa[MesaSeleccionada] = PedidosTemporales;
            }

            OnPropertyChanged(nameof(PedidosTemporales));
            OnPropertyChanged(nameof(Total));
        }


        private async Task FinalizarVentaAsync()
        {
            var mesa = new Mesa
            {
                NumeroMesa = MesaSeleccionada, // Cambiar según la lógica
                Pedidos1 = PedidosTemporales.ToList(),
                Total = total,
                Fecha = DateTime.Now
            };


            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://10.0.2.2:5002/api/Mesas/Create", mesa);
                if (response.IsSuccessStatusCode)
                {
                    await _page.DisplayAlert("Registro", "Pedido Creado exitosamente", "OK");

                    // Limpia los datos locales después de guardar
                    _pedidosPorMesa.Remove(MesaSeleccionada);
                    PedidosTemporales.Clear();
                    OnPropertyChanged(nameof(total));
                    // Mostrar un mensaje de éxito
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
