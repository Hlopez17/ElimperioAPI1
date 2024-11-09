using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE.MVVM.View;

public partial class Productos : ContentPage
{
    private readonly string _token;
    public Productos(string token)
	{
		InitializeComponent();
        _token = token;
        BindingContext = new ProductViewModel(this, _token);
        viewModel = new ProductViewModel(this, _token);
    }

    private ProductViewModel viewModel;


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.GetProductosAsync();
    }

}