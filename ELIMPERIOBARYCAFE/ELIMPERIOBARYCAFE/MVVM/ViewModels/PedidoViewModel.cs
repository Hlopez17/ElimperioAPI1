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


namespace ELIMPERIOBARYCAFE.MVVM.ViewModels
{
    public partial class PedidoViewModel: ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página
        //public ObservableCollection<> Producto { get; } = new ObservableCollection<Producto>();
        private readonly string _token;
        private readonly string? _productoid;
        public readonly string productos;

    }
}
