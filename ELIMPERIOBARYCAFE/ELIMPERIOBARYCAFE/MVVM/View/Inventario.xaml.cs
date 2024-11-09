using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE.MVVM.View;

public partial class Inventario : ContentPage
{
    private ProductViewModel viewModel;
    private readonly string _token;
    public Inventario()
	{
		InitializeComponent();
        viewModel = new ProductViewModel(this, _token);
        BindingContext = viewModel;
    }

   
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.GetProductosAsync();
    }

}