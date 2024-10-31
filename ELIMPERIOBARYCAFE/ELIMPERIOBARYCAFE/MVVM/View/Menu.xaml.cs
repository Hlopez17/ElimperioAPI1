using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE.MVVM.View;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
	}

    int count = 0;

  
    // M�todo manejador del evento OnMenuClicked
    private void OnMenuClicked(object sender, EventArgs e)
    {
        // Aqu� va la l�gica para abrir el men� lateral
        Shell.Current.FlyoutIsPresented = true;
    }
    private async void OnCocinaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cocinas());
    }

    private async void OnUsuarioClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroUser());
    }

    private async void OnCajaClicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("CajaPage"); // Aseg�rate de registrar esta ruta si existe
    }
}

