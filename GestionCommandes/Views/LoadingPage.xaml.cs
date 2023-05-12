using GestionCommandes.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GestionCommandes.Views;

public sealed partial class LoadingPage : Page
{
    public LoadingViewModel ViewModel
    {
        get;
    }

    public LoadingPage()
    {
        ViewModel = App.GetService<LoadingViewModel>();
        InitializeComponent();
    }
}
