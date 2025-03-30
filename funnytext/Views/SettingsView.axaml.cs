using System;
using Avalonia.Controls;
using Avalonia.Input;
using funnytext.ViewModels;
using System.Text.RegularExpressions;

namespace funnytext.Views;
using ViewModels;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
    }

    private void OnNumberInputChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox) return;
        textBox.Text = Regex.Replace(textBox.Text, "[^0-9]", "");
        if (textBox.Text.Length > 2)
        {
            textBox.Text = textBox.Text[..4];
        }
        if (textBox.Text.Length == 0 || textBox.Text == "0")
        {
            textBox.Text = "1";
        }
        if (DataContext is RandomViewModel viewModel)
        {
            viewModel.OnChangeSomething();
        }
    }
}