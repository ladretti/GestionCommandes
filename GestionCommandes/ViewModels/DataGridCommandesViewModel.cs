using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Contracts.ViewModels;
using GestionCommandes.Core.Contracts.Services;
using GestionCommandes.Core.Models;
using GestionCommandes.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using GestionCommandes.Views;
using GestionCommandes.Contracts.Services;

namespace GestionCommandes.ViewModels;

public class DataGridCommandesViewModel : ObservableRecipient, INavigationAware, INotifyPropertyChanged
{
    private readonly ICommandeDataService _sampleDataService;
    private readonly IFournisseurDataService _fournisseursDataService;
    private readonly IClientDataService _clientsDataService;

    public ObservableCollection<Commande> Source { get; } = new ObservableCollection<Commande>();
    public ObservableCollection<Commande> SourceFiltered { get; set; } = new ObservableCollection<Commande>();
    public CollectionViewSource SourceFiltered2 { get; private set; } = new CollectionViewSource();
    public ObservableCollection<Fournisseur> SourceFournisseurs { get; set; } = new ObservableCollection<Fournisseur>();
    public ObservableCollection<Client> SourceClients { get; set; } = new ObservableCollection<Client>();
    private Client selectedClientValue;
    public Client SelectedClientValue
    {
        get
        {
            return selectedClientValue;
        }

        set
        {
            selectedClientValue = value;
            OnPropertyChanged(nameof(SelectedClientValue));
        }
    }
    private Fournisseur selectedFournisseurValue;
    public Fournisseur SelectedFournisseurValue
    {
        get
        {
            return selectedFournisseurValue;
        }

        set
        {
            selectedFournisseurValue = value;
            OnPropertyChanged(nameof(SelectedFournisseurValue));
        }

    }
    private DateTimeOffset? dateSelected;
    public DateTimeOffset? DateSelected
    {
        get
        {
            return dateSelected;
        }

        set
        {
            dateSelected = value;
            OnPropertyChanged(nameof(DateSelected));
        }
    }
    private string searchText;
    public string SearchText
    {
        get
        {
            return searchText;
        }

        set
        {
            searchText = value;
            OnPropertyChanged(nameof(SearchText));
        }
    }
    private Commande selectedItem;
    public Commande SelectedItem
    {
        get
        {
            return selectedItem;
        }

        set
        {
            selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
        }
    }
    public ICommand RefreshFilter
    {
        get;
    }
    public ICommand AddSN
    {
        get;
    }
    public INavigationService NavigationService
    {
        get;
    }
    public DataGridCommandesViewModel(INavigationService navigationService, ICommandeDataService sampleDataService, IFournisseurDataService fournisseurDataService, IClientDataService clientDataService)
    {
        NavigationService = navigationService;

        _sampleDataService = sampleDataService;
        _fournisseursDataService = fournisseurDataService;
        _clientsDataService = clientDataService;
        RefreshFilter = new RelayCommand(RemoveFilter);
        AddSN = new RelayCommand(OnMenuViewsInsertSN);
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();
        SourceFournisseurs.Clear();
        SourceClients.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();
        List<Fournisseur> dataFournisseurs = new List<Fournisseur>();
        List<Client> dataCLients = new List<Client>();

        SourceClients.Add(new Client());
        SourceFournisseurs.Add(new Fournisseur());

        foreach (var item in data)
        {
            Source.Add(item);
            if (!string.IsNullOrEmpty(item.Client.Name))
                if (!dataCLients.Any(p => p.Name.ToUpper() == item.Client.Name.ToUpper()))
                    dataCLients.Add(new Client(item.Client.Name.ToUpper()));
            if (!string.IsNullOrEmpty(item.Fournisseur.Name))
                if (!dataFournisseurs.Any(p => p.Name.ToUpper() == item.Fournisseur.Name.ToUpper()))
                    dataFournisseurs.Add(new Fournisseur(item.Fournisseur.Name.ToUpper()));
        }
        dataCLients = dataCLients.OrderBy(q => q.Name).ToList();
        dataFournisseurs = dataFournisseurs.OrderBy(q => q.Name).ToList();
        foreach (var item in dataCLients)
        {
            SourceClients.Add(item);
        }
        foreach (var item in dataFournisseurs)
        {
            SourceFournisseurs.Add(item);
        }
        SourceFiltered2.Source = Source.OrderByDescending(q => q.DateCommande).ThenBy(q => q.NumCommande2).ThenBy(q => q.NumCommande).ToList();
    }

    public void OnNavigatedFrom()
    {
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async void UpdateData()
    {
        var data = Source.OrderByDescending(q => q.DateCommande).ThenBy(q => q.NumCommande).ToList();


        if (SelectedClientValue?.Name != null || SelectedFournisseurValue?.Name != null || DateSelected != null || SearchText != null)
        {
            if (SelectedClientValue != null)
            {
                if (SelectedClientValue.Name != null)
                {
                    data = (from r in data where r.Client?.Name?.ToUpper() == SelectedClientValue.Name select r).ToList();
                }
            }

            if (SelectedFournisseurValue != null)
            {
                if (SelectedFournisseurValue.Name != null)
                    data = (from item in data where item.Fournisseur?.Name?.ToUpper() == SelectedFournisseurValue.Name select item).ToList();
            }

            if (DateSelected != null)
            {
                data = (from item in data where item.DateCommande == DateSelected.Value.Date select item).ToList();
            }
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                data = (from item in data where item.NumCommande != null && item.NumCommande.Contains(SearchText) select item).ToList();
            }

            SourceFiltered2.Source = data;
        }

        else
        {
            SourceFiltered2.Source = Source.OrderByDescending(q => q.DateCommande).ThenBy(q => q.NumCommande2).ThenBy(q => q.NumCommande).ToList();
        }
    }

    private void RemoveFilter()
    {
        SelectedClientValue = null;
        SelectedFournisseurValue = null;
        DateSelected = null;
        SearchText = null;
    }

    private async void AddSNCommande()
    {
    }
    private async void OnMenuViewsInsertSN()
    {
        ContentDialog _addDialog;

        var insertionPage = new InsertSNPage();
        var insertionViewModel = new InsertSNViewModel(SelectedItem);
        insertionPage.DataContext = insertionViewModel;

        _addDialog = new ContentDialog
        {
            Content = insertionPage,
            Title = "Enregistrement",
            CloseButtonText = "Annuler",
            PrimaryButtonText = "Valider",
            //PrimaryButtonCommand = insertionViewModel.AddRenouvellementCommand,
        };
        _addDialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        await _addDialog.ShowAsync();
        //RefreshData();
    }

}


