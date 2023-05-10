using GestionCommandes.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input.Preview.Injection;

namespace GestionCommandes.Views;

public sealed partial class InsertSNPage : Page
{
    public InsertSNViewModel ViewModel
    {
        get;
    }

    public InsertSNPage()
    {
        ViewModel = App.GetService<InsertSNViewModel>();
        this.DataContext = ViewModel;
        InitializeComponent();
    }

    private void IntTextBox_TextChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
        if (sender.Value > 200)
        {
            sender.Value = 200;
        }

        if (sender.Value > 0)
        {
            ParametresItemsControl.ItemsSource = Enumerable.Range(1, (int)sender.Value).Select(i => $"SN {i}");
        }
        else
        {
            ParametresItemsControl.ItemsSource = null;
        }

    }
    private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            InputInjector inputInjector = InputInjector.TryCreate();
            var tab = new InjectedInputKeyboardInfo();
            tab.VirtualKey = (ushort)(VirtualKey.Tab);
            tab.KeyOptions = InjectedInputKeyOptions.None;

            inputInjector.InjectKeyboardInput(new[] { tab });

        }
    }


}
