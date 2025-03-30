using System;
using Avalonia.Controls;
using Avalonia.Input;
using funnytext.ViewModels;
using System.Text.RegularExpressions;

namespace funnytext.Views;
using ViewModels;

public partial class EncryptionView : UserControl
{
    public EncryptionView()
    {
        InitializeComponent();
        DataContext = new EncryptionViewModel();
    }

    private void OnNumberInputChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox) return;
        textBox.Text = Regex.Replace(textBox.Text, "[^0-9]", "");
        if (Int32.TryParse(textBox.Text, out int value) && value > 32)
        {
            textBox.Text = "32";
        }
        if (textBox.Text.Length == 0 || textBox.Text == "0")
        {
            textBox.Text = "1";
        }
        if (DataContext is EncryptionViewModel viewModel)
        {
            viewModel.OnChangeSomething();
        }
    }
    
    private void OnInputChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox) return;
        if (DataContext is EncryptionViewModel viewModel)
        {
            viewModel.OnChangeSomething();
        }
    }
}