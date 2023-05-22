using GestionCommandes.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

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
        DataContext = ViewModel;
        InitializeComponent();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ViewModel.UpdateData();
    }
    private void myDatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
    {
        ViewModel.UpdateData();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
       ViewModel.UpdateData();
    }
    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        ViewModel.OnMenuViewsInsertSN();
    }
}
