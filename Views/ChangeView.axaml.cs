using System;
using Avalonia.Controls;
using Avalonia.Input;
using funnytext.ViewModels;
using System.Text.RegularExpressions;

namespace funnytext.Views;
using ViewModels;

public partial class ChangeView : UserControl
{
    public ChangeView()
    {
        InitializeComponent();
        DataContext = new ChangeViewModel();
    }

    private void NumberInput(object? sender, string number)
    {
        if (sender is not TextBox textBox) return;
        textBox.Text = Regex.Replace(textBox.Text, "[^0-9]", "");
        if (textBox.Text.Length > 4)
        {
            textBox.Text = textBox.Text[..4];
        }
        if (string.IsNullOrEmpty(textBox.Text))
        {
            textBox.Text = number;
        }
        if (DataContext is ChangeViewModel viewModel)
        {
            viewModel.OnChangeSomething();
        }
    }

    private void OnNumberInputChanged_0(object? sender, TextChangedEventArgs e)
    {
        NumberInput(sender, "0");
    }

    private void OnNumberInputChanged_1(object? sender, TextChangedEventArgs e)
    {
        NumberInput(sender, "1");
    }

    private void OnNumberInputChanged_2(object? sender, TextChangedEventArgs e)
    {
        NumberInput(sender, "2");
    }
    
    private void OnInputChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox) return;
        if (DataContext is ChangeViewModel viewModel)
        {
            viewModel.OnChangeSomething();
        }
    }
}