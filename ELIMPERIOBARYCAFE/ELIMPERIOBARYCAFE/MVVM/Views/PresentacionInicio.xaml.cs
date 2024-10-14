namespace ELIMPERIOBARYCAFE;

public partial class PresentacionInicio : ContentPage
{
	public PresentacionInicio()
	{
		InitializeComponent();
	}
    private async void OnComenzarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
}