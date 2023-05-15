using GestionCommandes.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GestionCommandes.Views;

public sealed partial class AccueilPage : Page
{
    public AccueilViewModel ViewModel
    {
        get;
    }

    public AccueilPage()
    {
        ViewModel = App.GetService<AccueilViewModel>();
        this.DataContext = ViewModel;
        InitializeComponent();
    }
}
