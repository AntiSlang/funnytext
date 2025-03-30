using System;
using Avalonia.Controls;
using Avalonia.Input;
using funnytext.ViewModels;
using System.Text.RegularExpressions;

namespace funnytext.Views;
using ViewModels;

public partial class ScriptView : UserControl
{
    public ScriptView()
    {
        InitializeComponent();
        DataContext = new ScriptViewModel();
    }
}