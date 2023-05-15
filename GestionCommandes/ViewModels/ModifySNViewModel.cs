using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Threading.Channels;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Core.Contracts.Services;
using GestionCommandes.Core.Models;
using GestionCommandes.Models;
using GestionCommandes.Services;

namespace GestionCommandes.ViewModels;

public class ModifySNViewModel : ObservableRecipient, INotifyPropertyChanged
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
    public ModifySNViewModel()
    {
        ValidationCommande = new RelayCommand(Validation);
    }
    public ModifySNViewModel(Commande commande, ICommandeDataService commandeService) : this()
    {
        SelectedCommande = commande;
        SetupString();
        _sampleDataService = commandeService;
    }

    private void SetupString()
    {
        if (SelectedCommande.DateReception == null)
            SelectedCommande.DateReception = DateTime.Now;
        if (!String.IsNullOrWhiteSpace(SelectedCommande.SN))
        {
            var e = SelectedCommande.SN.Split(' ').ToList();
            MyStrings = new ObservableCollection<Strong>();
            foreach (var e2 in e)
            {
                if (!String.IsNullOrEmpty(e2))
                    MyStrings.Add(new Strong() { Value = e2 });
            }
        }
        if (SelectedCommande.QuantiteRecu != 0)
            NumberOfStrings = (int)SelectedCommande.QuantiteRecu;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public async void Validation()
    {
        if (NumberOfStrings != null)
        {
            SelectedCommande.QuantiteRecu = NumberOfStrings;
        }
        if (MyStrings != null)
        {
            string leString = "";
            foreach (Strong s in MyStrings)
            {
                leString += s.Value + " ";
            }
            SelectedCommande.SN = leString;
        }
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

    public void InitializeMyStrings()
    {
        if (MyStrings == null)
        {
            MyStrings = new ObservableCollection<Strong>();
        }
        while (MyStrings.Count < NumberOfStrings)
        {
            MyStrings.Add(new Strong { Value = "" });
        }

        while (MyStrings.Count > NumberOfStrings)
        {
            MyStrings.RemoveAt(MyStrings.Count - 1);
        }

    }
}
