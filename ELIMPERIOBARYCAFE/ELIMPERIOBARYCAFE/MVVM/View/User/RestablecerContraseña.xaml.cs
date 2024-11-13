using ELIMPERIOBARYCAFE.MVVM.View;

namespace ELIMPERIOBARYCAFE;

public partial class RestablecerContraseña : ContentPage
{
	public RestablecerContraseña()
	{
		InitializeComponent();
	}
    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Auth());
    }

}