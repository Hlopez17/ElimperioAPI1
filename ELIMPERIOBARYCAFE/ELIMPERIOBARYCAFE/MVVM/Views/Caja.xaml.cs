namespace ELIMPERIOBARYCAFE;

public partial class Caja : ContentPage
{
	public Caja()
	{
		InitializeComponent();
	}

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        // Mostrar el men� de opciones al hacer clic en el bot�n de tres puntos
        string action = await DisplayActionSheet("Opciones", "Cancelar", null, "Reportes",  "Cierre de sesi�n");

       
    }
}