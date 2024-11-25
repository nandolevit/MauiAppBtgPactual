using MauiAppBtgPactual.ViewModels;

namespace MauiAppBtgPactual.Views;

public partial class CadastroCliente : ContentPage
{
	public CadastroCliente(ClienteViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}