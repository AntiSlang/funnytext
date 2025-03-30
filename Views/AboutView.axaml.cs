using Avalonia.Controls;
using Avalonia.Input;
using funnytext.ViewModels;
using System.Text.RegularExpressions;
using Avalonia.Markup.Xaml;

namespace funnytext.Views;
using ViewModels;

public partial class AboutView : UserControl
{
    public AboutView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}