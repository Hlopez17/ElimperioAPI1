using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.ViewModels;
namespace ELIMPERIOBARYCAFE.MVVM.View;

public partial class Auth : ContentPage
{
	public Auth()
	{
		InitializeComponent();

		BindingContext = new AuthViewModel(this);
	}
}