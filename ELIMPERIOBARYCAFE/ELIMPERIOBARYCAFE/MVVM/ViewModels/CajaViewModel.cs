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
    public partial class CajaViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página
        private readonly string _token;
        public ObservableCollection<Mesa> MesasPendientes { get; set; } = new ObservableCollection<Mesa>();

        public Mesa? MesaSeleccionada { get; set; }
        public decimal Total { get; set; }
        public string TipoPago { get; set; }

        //Propiedades para el Binding de Comandos
        public ICommand FinalizarPagoCommand { get; }

        public CajaViewModel(Page page, string token)
        {
            _httpClient = new HttpClient();
            _page = page;
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:5002");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _token = token;

            CargarMesasPendientes();

            FinalizarPagoCommand = new Command(async () => await FinalizarPagoAsync());
        }

       

        private async void CargarMesasPendientes()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://10.0.2.2:5002/api/Caja/ObtenerMesasPendientes");
                if (response.IsSuccessStatusCode)
                {
                    var mesas = await response.Content.ReadFromJsonAsync<List<Mesa>>();
                    MesasPendientes.Clear();
                    foreach (var mesa in mesas)
                    {
                        MesasPendientes.Add(mesa);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async Task FinalizarPagoAsync()
        {
            if (MesaSeleccionada == null || string.IsNullOrWhiteSpace(TipoPago))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Selecciona una mesa y el tipo de pago.", "OK");
                return;
            }

            try
            {
                var nuevaCaja = new Cajas
                {
                    NumeroMesa = MesaSeleccionada.NumeroMesa,
                    Total = MesaSeleccionada.Total,
                    Descuento = 0,
                    TipoPago = TipoPago
                };

                var response = await _httpClient.PostAsJsonAsync("http://10.0.2.2:5002/api/Caja/PayRegister", nuevaCaja);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pago registrado y mesa cancelada exitosamente.", "OK");
                    CargarMesasPendientes();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo registrar el pago.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }




    }
}
