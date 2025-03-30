// using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Reactive;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using funnytext.Services;
using funnytext.Views;
using ReactiveUI;

namespace funnytext.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public ICommand RandomCommand { get; }
    public ICommand EncryptionCommand { get; }
    public ICommand ScriptCommand { get; }
    public ICommand TextChangeCommand { get; }
    public ICommand StatisticsCommand { get; }
    public ICommand LinksCommand { get; }
    public ICommand SettingsCommand { get; }
    public ICommand CloseCommand { get; }
    public ReactiveCommand<Unit, Unit> MinimizeToTrayCommand { get; }

    private bool _isTrayActive = true;
    public bool IsTrayActive
    {
        get => _isTrayActive;
        set => this.RaiseAndSetIfChanged(ref _isTrayActive, value);
    }

    public MainViewModel()
    {
        if (((App)Application.Current).ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            IsTrayActive = false;
        }
        ((App)Application.Current).SetThemeShort(true);
        ((App)Application.Current).InMain = true;
        RandomCommand = new RelayCommand(OpenRandom);
        EncryptionCommand = new RelayCommand(OpenEncryption);
        ScriptCommand = new RelayCommand(OpenScript);
        TextChangeCommand = new RelayCommand(OpenTextChange);
        StatisticsCommand = new RelayCommand(OpenStatistics);
        LinksCommand = new RelayCommand(OpenLinks);
        SettingsCommand = new RelayCommand(OpenSettings);
        CloseCommand = new RelayCommand(CloseApp);
        MinimizeToTrayCommand = ReactiveCommand.Create(MinimizeToTray);
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(MainViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(MainViewModel)))
        {
            StartTutorial();
        }
    }
    
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "Добро пожаловать в Cool Text",
            "Это обучение будет продолжаться на выбранных вами экранах. Его можно перепройти в Настройках"
        };
        
        Tutorial.StartTutorial(steps);
    }
    
    public void MinimizeToTray()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow != null)
            {
                desktop.MainWindow.Hide();
                App._trayIcon.IsVisible = true;
            }
        }
    }

    private void OpenRandom()
    {
        ((App)Application.Current).OpenScreen<RandomView, RandomViewModel>(inmain: true);
    }
    
    private void OpenEncryption() { 
        ((App)Application.Current).OpenScreen<EncryptionView, EncryptionViewModel>(inmain: true);
    }

    private void OpenScript()
    {
        ((App)Application.Current).OpenScreen<ScriptView, ScriptViewModel>(inmain: true);
    }

    private void OpenTextChange()
    {
        ((App)Application.Current).OpenScreen<ChangeView, ChangeViewModel>(inmain: true);
    }

    private void OpenStatistics()
    {
        ((App)Application.Current).OpenScreen<StatsView, StatsViewModel>(inmain: true);
    }

    private void OpenLinks()
    {
        ((App)Application.Current).OpenScreen<AboutView, AboutViewModel>(inmain: true);
    }

    private void OpenSettings()
    {
        ((App)Application.Current).OpenScreen<SettingsView, SettingsViewModel>(inmain: true);
    }

    private void CloseApp()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            if (App._trayIcon != null)
            {
                App._mainWindow.Closing -= App.OnMainWindowClosing;
                App._trayIcon.IsVisible = false;
            }
        App.AppCloser.Close();
    }
}

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    public RelayCommand(Action execute) => _execute = execute;
    public event EventHandler CanExecuteChanged;
    public bool CanExecute(object parameter) => true;
    public void Execute(object parameter) => _execute();
}