using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GestionCommandes.Services;
using GestionCommandes.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.VisualBasic;
using Windows.System;
using Windows.UI.Input.Preview.Injection;

namespace GestionCommandes.Views;

public sealed partial class GestionSNPage : Page
{
    public GestionSNViewModel ViewModel
    {
        get;
    }
    public GestionSNPage()
    {
        ViewModel = App.GetService<GestionSNViewModel>();
        DataContext = ViewModel;
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
