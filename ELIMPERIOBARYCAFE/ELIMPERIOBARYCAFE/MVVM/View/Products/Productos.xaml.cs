using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE.MVVM.View;

public partial class Productos : ContentPage
{
    private readonly string _token;
    private ProductViewModel viewModel;
    public Productos(string token)
	{
		InitializeComponent();
        _token = token;
        BindingContext = new ProductViewModel(this, token);
    }

    private async void VerClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Inventario(_token));
    }





}