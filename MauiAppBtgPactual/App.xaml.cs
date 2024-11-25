
using MauiAppBtgPactual.ViewModels;
using MauiAppBtgPactual.Views;

namespace MauiAppBtgPactual
{
    public partial class App : Application
    {
        public App(ClienteViewModel viewModel)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MauiAppBtgPactual.Views.MainPage(viewModel));
        }
    }
}
