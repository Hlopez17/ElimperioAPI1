using ELIMPERIOBARYCAFE.MVVM.Models;
using ELIMPERIOBARYCAFE.MVVM.ViewModels;

namespace ELIMPERIOBARYCAFE.MVVM.View.Products;

public partial class EditProducts : ContentPage
{
    private readonly string _token;
    private readonly string? _productid;
    public EditProducts(string? productid)
	{
		InitializeComponent();
        _productid = productid;
    }
}