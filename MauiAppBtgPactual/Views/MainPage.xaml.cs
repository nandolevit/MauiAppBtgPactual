using MauiAppBtgPactual.ViewModels;

namespace MauiAppBtgPactual.Views;

public partial class MainPage : ContentPage
{
    private readonly ClienteViewModel _viewModel;
    public MainPage(ClienteViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroCliente(_viewModel));
    }
}
