using System;
using Avalonia.Controls;
using Avalonia.Input;
using funnytext.ViewModels;
using System.Text.RegularExpressions;

namespace funnytext.Views;
using ViewModels;

public partial class ThemeView : UserControl
{
    public ThemeView()
    {
        InitializeComponent();
        DataContext = new ThemeViewModel();
    }
}