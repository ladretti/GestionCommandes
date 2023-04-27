using GestionCommandes.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GestionCommandes.Views;

// TODO: Change the grid as appropriate for your app. Adjust the column definitions on DataGridPage.xaml.
// For more details, see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid.
public sealed partial class DataGridCommandesPage : Page
{
    public DataGridCommandesViewModel ViewModel
    {
        get;
    }

    public DataGridCommandesPage()
    {
        ViewModel = App.GetService<DataGridCommandesViewModel>();
        InitializeComponent();
    }
}
