namespace ELIMPERIOBARYCAFE;

public partial class Mesero : ContentPage
{
	public Mesero()
	{
		InitializeComponent();
	}

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        // Mostrar el menú de opciones al hacer clic en el botón de tres puntos
        string action = await DisplayActionSheet("Opciones", "Cancelar", null, "Promociones", "Reserva", "Pedidos", "Cierre de sesión");

        // Acción dependiendo de la opción seleccionada
        switch (action)
        {
            case "Promociones":
                // Lógica para ir a la página de Promociones
                await Navigation.PushAsync(new Promociones());
                break;
            case "Reserva":
                // Lógica para ir a la página de Reserva
                await Navigation.PushAsync(new Reserva());
                break;
            case "Pedidos":
                // Lógica para ir a la página de Pedidos
                await Navigation.PushAsync(new Pedidos());
                break;
            case "Cierre de sesión":
                // Lógica para cerrar sesión
                bool cerrarSesion = await DisplayAlert("Confirmar", "¿Estás seguro de que deseas cerrar sesión?", "Sí", "No");
                if (cerrarSesion)
                {
                    // Aquí puedes navegar a la pantalla de login o cerrar la sesión de alguna forma
                    await Navigation.PopToRootAsync(); // Regresar a la pantalla de login
                }
                break;
            
        }
    }
}
