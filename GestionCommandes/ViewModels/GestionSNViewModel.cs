using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Core.Contracts.Services;
using GestionCommandes.Core.Models;
using GestionCommandes.Core.Services;
using GestionCommandes.Models;

namespace GestionCommandes.ViewModels;

public class GestionSNViewModel : ObservableRecipient, INotifyPropertyChanged
{
    private readonly ICommandeDataService _sampleDataService;
    public ICommand ValidationCommande
    {
        get;
    }
    public Commande SelectedCommande
    {
        get; set;
    }
    private ObservableCollection<Strong> _myStrings;
    private int _numberOfStrings;

    public ObservableCollection<Strong> MyStrings
    {
        get
        {
            return _myStrings;
        }
        set
        {
            _myStrings = value;
            OnPropertyChanged("MyStrings");
        }
    }

    public int NumberOfStrings
    {
        get
        {
            return _numberOfStrings;
        }
        set
        {
            _numberOfStrings = value;
            InitializeMyStrings();
            OnPropertyChanged("NumberOfStrings");
        }
    }
    private bool _isCommandExecuted = false;
    public bool IsCommandExecuted
    {
        get
        {
            return _isCommandExecuted;
        }
        set
        {
            _isCommandExecuted = value; OnPropertyChanged(nameof(IsCommandExecuted));
        }
    }
    public GestionSNViewModel()
    {
        ValidationCommande = new RelayCommand(Validation);
    }
    public GestionSNViewModel(Commande commande, ICommandeDataService commandeService) : this()
    {
        SelectedCommande = commande;
        _sampleDataService = commandeService;
        SelectedCommande.DateReception = DateTime.Now;
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public async void Validation()
    {
        SelectedCommande.QuantiteRecu = NumberOfStrings;
        string leString = "";
        foreach (Strong s in MyStrings)
        {
            leString += s.Value + " ";
        }
        SelectedCommande.SN = leString;
        try
        {
            await _sampleDataService.ModifyCommandeAsync(SelectedCommande);
            IsCommandExecuted = true;
        }
        catch (Exception ex)
        {
            IsCommandExecuted = false;
        }
    }

    private void InitializeMyStrings()
    {
        MyStrings = new ObservableCollection<Strong>();
        for (int i = 0; i < NumberOfStrings; i++)
        {
            MyStrings.Add(new Strong { Value = "" });
        }
    }

}
