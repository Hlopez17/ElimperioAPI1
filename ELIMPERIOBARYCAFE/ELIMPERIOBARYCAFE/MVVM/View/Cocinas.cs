amespace ELIMPERIOBARYCAFE.MVVM.View;

public class Cocinas : ContentPage
{
	public Cocinas()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}