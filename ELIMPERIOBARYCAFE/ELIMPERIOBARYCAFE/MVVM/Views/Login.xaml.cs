namespace ELIMPERIOBARYCAFE;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RestablecerContraseña());
    }

    private async void OnSignUpTapped(object sender, EventArgs e)
    {
        // Aquí iría la navegación a la página de registro (por ejemplo, SignUpPage)
        await Navigation.PushAsync(new RegistroUsuario());
    }
}