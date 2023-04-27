using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using GestionCommandes.Contracts.ViewModels;
using GestionCommandes.Core.Contracts.Services;
using GestionCommandes.Core.Models;

namespace GestionCommandes.ViewModels;

public class DataGridCommandesViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    public DataGridCommandesViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
