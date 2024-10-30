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


namespace ELIMPERIOBARYCAFE.MVVM.ViewModels
{
    internal class ProductViewModel: ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página

        //Propiedades para el Binding de Comandos
        public ICommand CrearCommand { get; }
        public ICommand EliminarCommand { get; }

        public ProductViewModel(Page page)
        {
            _httpClient = new HttpClient();
            _page = new Page();

            //Se crean los comandos manualmente

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
    }
}
