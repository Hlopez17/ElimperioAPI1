using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE;

public partial class Caja : ContentPage
{
    private readonly string _token;
    public Caja(string token)
	{
		InitializeComponent();
        _token = token;
        BindingContext = new CajaViewModel(this, token);
    }

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        // Mostrar el men� de opciones al hacer clic en el bot�n de tres puntos
        string action = await DisplayActionSheet("Opciones", "Cancelar", null, "Reportes",  "Cierre de sesi�n");

       
    }
}