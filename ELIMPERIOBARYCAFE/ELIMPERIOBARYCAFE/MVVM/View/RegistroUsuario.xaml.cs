using ELIMPERIOBARYCAFE.MVVM.View;

namespace ELIMPERIOBARYCAFE;

public partial class RegistroUsuario : ContentPage
{
	public RegistroUsuario()
	{
		InitializeComponent();
	}

    private void OnLoginTapped(object sender, EventArgs e)
    {
        // Navegar de regreso al login
        Navigation.PushAsync(new Auth());
    }
}