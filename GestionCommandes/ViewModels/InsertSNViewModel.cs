using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Core.Models;

namespace GestionCommandes.ViewModels;

public class InsertSNViewModel : ObservableRecipient
{
    public ICommand ValidationCommande
    {
        get;
    }
    private Commande selectedCommande;
    public Commande SelectedCommande
    {
        get
        {
            return selectedCommande;
        }

        set
        {
            selectedCommande = value;
            OnPropertyChanged(nameof(SelectedCommande));
        }
    }
    
    private ObservableCollection<string> stringList = new ObservableCollection<string>();
    public ObservableCollection<string> StringList
    {
        get
        {
            return stringList;
        }
        set
        {
            stringList = value;
            OnPropertyChanged(nameof(StringList));
        }
    }

    public InsertSNViewModel()
    {
    
    }
    public InsertSNViewModel(Commande commande)
    {
        SelectedCommande = commande;
        ValidationCommande = new RelayCommand(Validation);
        SelectedCommande.DateReception = DateTime.Now;
    }
    public void Validation()
    {
        var e = StringList;
        var z = SelectedCommande;

    }
}
