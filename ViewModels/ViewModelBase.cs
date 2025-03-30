using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using funnytext.Services;
using ReactiveUI;

namespace funnytext.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    private FontFamily _appFontFamily;

    public FontFamily AppFontFamily
    {
        get => _appFontFamily;
        set
        {
            this.RaiseAndSetIfChanged(ref _appFontFamily, value);
            Application.Current.Resources["AppFontFamily"] = value;
        }
    }

    public ViewModelBase()
    {
        string savedFont = ((App)Application.Current).FontToAndroid(FileSettingsService.LoadString("font", "Sans Serif"));
        AppFontFamily = new FontFamily(savedFont);
    }
}