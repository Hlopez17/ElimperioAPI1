
using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE;

public partial class Mesero : ContentPage
{
    private readonly string _token;
    private PedidoViewModel viewModel;
    public Mesero(string token)
    {
		InitializeComponent();
        _token = token;
        BindingContext = new PedidoViewModel(this, token);
    }

  
}
