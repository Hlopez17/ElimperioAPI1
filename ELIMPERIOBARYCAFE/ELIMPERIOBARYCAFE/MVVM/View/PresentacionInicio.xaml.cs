using ELIMPERIOBARYCAFE.MVVM.View;

namespace ELIMPERIOBARYCAFE;

public partial class PresentacionInicio : ContentPage
{
	public PresentacionInicio(string token)
	{
		InitializeComponent();
	}
    private async void OnComenzarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Menu());
    }
}