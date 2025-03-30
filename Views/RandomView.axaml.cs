using System;
using Avalonia.Controls;
using Avalonia.Input;
using funnytext.ViewModels;
using System.Text.RegularExpressions;

namespace funnytext.Views;
using ViewModels;

public partial class RandomView : UserControl
{
    public RandomView()
    {
        InitializeComponent();
        DataContext = new RandomViewModel();
    }

    private void OnNumberInputChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox) return;
        textBox.Text = Regex.Replace(textBox.Text, "[^0-9]", "");
        if (textBox.Text.Length > 4)
        {
            textBox.Text = textBox.Text[..4];
        }
        if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == "0")
        {
            textBox.Text = "1";
        }
        if (DataContext is RandomViewModel viewModel)
        {
            viewModel.OnChangeSomething();
        }
    }
}