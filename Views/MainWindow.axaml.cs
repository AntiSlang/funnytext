using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace funnytext.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var contentControl = this.FindControl<ContentControl>("ContentControl") ?? ContentControl;
            contentControl.Content = new MainView();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void ShowView(UserControl view)
        {
            var contentControl = this.FindControl<ContentControl>("ContentControl") ?? ContentControl;
            contentControl.Content = view;
        }
    }
}