using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Contracts.Services;
using GestionCommandes.Services;
using Microsoft.UI.Xaml.Navigation;

namespace GestionCommandes.ViewModels;

public class AccueilViewModel : ObservableRecipient
{
    public ICommand GoToCommandesCommand
    {
        get;
    } 
    public INavigationService NavigationService
    {
        get;
    }
    public AccueilViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        //NavigationService.Navigated += OnNavigated;

        GoToCommandesCommand = new RelayCommand(OnMenuViewsDataGridCommandes);
    }
    //private void OnNavigated(object sender, NavigationEventArgs e) => IsBackEnabled = NavigationService.CanGoBack;
    private void OnMenuViewsDataGridCommandes()
    {
        NavigationService.NavigateTo(typeof(DataGridCommandesViewModel).FullName!);
    }
}
