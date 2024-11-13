using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ELIMPERIOBARYCAFE.MVVM.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ELIMPERIOBARYCAFE.MVVM.ViewModels
{
    public partial class UserViewModel: ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Page _page; // Referencia  a la página
        public ObservableCollection<Producto> Producto { get; } = new ObservableCollection<Producto>();
        private readonly string _token;
        private readonly string? _productoid;
        public readonly string productos;
    }
}
