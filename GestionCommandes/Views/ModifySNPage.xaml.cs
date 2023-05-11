using GestionCommandes.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using Windows.UI.Input.Preview.Injection;

namespace GestionCommandes.Views;

public sealed partial class ModifySNPage : Page
{
    public ModifySNViewModel ViewModel
    {
        get;
    }

    public ModifySNPage()
    {
        ViewModel = App.GetService<ModifySNViewModel>();
        this.DataContext = ViewModel;
        InitializeComponent();
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
