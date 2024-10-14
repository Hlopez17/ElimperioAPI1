namespace ELIMPERIOBARYCAFE;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RestablecerContrase�a());
    }

    private async void OnSignUpTapped(object sender, EventArgs e)
    {
        // Aqu� ir�a la navegaci�n a la p�gina de registro (por ejemplo, SignUpPage)
        await Navigation.PushAsync(new RegistroUsuario());
    }
}