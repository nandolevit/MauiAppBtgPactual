using MauiAppBtgPactual.ViewModels;

namespace MauiAppBtgPactual.Views;

public partial class MainPage : ContentPage
{
    public MainPage(ClienteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroCliente((ClienteViewModel)BindingContext));
    }
}
