namespace ELIMPERIOBARYCAFE;

public partial class Caja : ContentPage
{
	public Caja()
	{
		InitializeComponent();
	}

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        // Mostrar el menú de opciones al hacer clic en el botón de tres puntos
        string action = await DisplayActionSheet("Opciones", "Cancelar", null, "Reportes",  "Cierre de sesión");

       
    }
}