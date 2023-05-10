using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GestionCommandes.Contracts.Services;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

namespace GestionCommandes.ViewModels;

public class ShellViewModel : ObservableRecipient
{
    private bool _isBackEnabled;

    public ICommand MenuFileExitCommand
    {
        get;
    }

    public ICommand MenuViewsInsertSNCommand
    {
        get;
    }

    public ICommand MenuViewsBlankCommand
    {
        get;
    }

    public ICommand MenuSettingsCommand
    {
        get;
    }

    public ICommand MenuViewsDataGridCommandesCommand
    {
        get;
    }

    public ICommand MenuViewsAccueilCommand
    {
        get;
    }

    public INavigationService NavigationService
    {
        get;
    }

    public bool IsBackEnabled
    {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public ShellViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;

        MenuFileExitCommand = new RelayCommand(OnMenuFileExit);
        MenuViewsInsertSNCommand = new RelayCommand(OnMenuViewsInsertSN);
        MenuSettingsCommand = new RelayCommand(OnMenuSettings);
        MenuViewsDataGridCommandesCommand = new RelayCommand(OnMenuViewsDataGridCommandes);
        MenuViewsAccueilCommand = new RelayCommand(OnMenuViewsAccueil);
    }

    private void OnNavigated(object sender, NavigationEventArgs e) => IsBackEnabled = NavigationService.CanGoBack;

    private void OnMenuFileExit() => Application.Current.Exit();

    private void OnMenuViewsInsertSN() => NavigationService.NavigateTo(typeof(InsertSNViewModel).FullName!);

    private void OnMenuSettings() => NavigationService.NavigateTo(typeof(SettingsViewModel).FullName!);

    private void OnMenuViewsDataGridCommandes() => NavigationService.NavigateTo(typeof(DataGridCommandesViewModel).FullName!);

    private void OnMenuViewsAccueil() => NavigationService.NavigateTo(typeof(AccueilViewModel).FullName!);
}
