namespace ELIMPERIOBARYCAFE;

public partial class Cosina : ContentPage
{
	public Cosina()
	{
		InitializeComponent();
	}

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        // Mostrar el men� de opciones al hacer clic en el bot�n de tres puntos
        string action = await DisplayActionSheet("Opciones", "Cancelar", null, "Pedidos", "Cierre de sesi�n");

        // Acci�n dependiendo de la opci�n seleccionada
        switch (action)
        {
            case "Pedidos":
                // L�gica para ir a la p�gina de Pedidos
                await Navigation.PushAsync(new Pedidos());
                break;
            case "Cierre de sesi�n":
                // L�gica para cerrar sesi�n
                bool cerrarSesion = await DisplayAlert("Confirmar", "�Est�s seguro de que deseas cerrar sesi�n?", "S�", "No");
                if (cerrarSesion)
                {
                    // Aqu� puedes navegar a la pantalla de login o cerrar la sesi�n de alguna forma
                    await Navigation.PopToRootAsync(); // Regresar a la pantalla de login
                }
                break;

        }
    }
}