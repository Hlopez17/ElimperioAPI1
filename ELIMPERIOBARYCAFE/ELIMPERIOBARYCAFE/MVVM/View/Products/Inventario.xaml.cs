using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE.MVVM.View;

public partial class Inventario : ContentPage
{
    private ProductViewModel viewModel;
    private readonly string _token;
    public Inventario(string token)
    {
		InitializeComponent();
        _token = token;

        viewModel = new ProductViewModel(this, _token);//La posición en la que llamaba esta onda
        viewModel.GetProductosAsync();
        BindingContext = viewModel;
    }


}