using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE.MVVM.View;

public partial class RegistroUser : ContentPage
{
	public RegistroUser()
	{
		InitializeComponent();
        BindingContext = new AuthViewModel(this);
    }
}