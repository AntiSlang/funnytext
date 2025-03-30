using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Threading;
using funnytext.Views;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using funnytext.Services;

namespace funnytext.ViewModels;


public class AboutViewModel : ViewModelBase
{
    public ReactiveCommand<string, Unit> OpenLinkCommand { get; }
    public ReactiveCommand<Unit, Unit> ShareCommand { get; }
    public ReactiveCommand<Unit, Unit> MainViewCommand { get; }

    public AboutViewModel()
    {
        ((App)Application.Current).SetThemeShort();
        OpenLinkCommand = ReactiveCommand.Create<string>(OpenUrlInBrowser);
        MainViewCommand = ReactiveCommand.Create(OpenMain);
        string text = "дЕлАй ТеКсТ сМеШнЕе В пРиЛоЖеНиИ \"сМеШнОй ТеКсТ\": https://play.google.com/store/apps/details?id=com.antislang.funnytext";
        if (FileSettingsService.LoadString("language", "en") == "en")
        {
            text = "mAkE tHe TeXt FuNnIeR iN tHe ApPlIcAtIoN \"fUnNy TeXt\": https://play.google.com/store/apps/details?id=com.antislang.funnytext";
        }
        ShareCommand = ReactiveCommand.Create(() => App.ShareService.ShareText(text));
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(AboutViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(AboutViewModel)))
        {
            StartTutorial();
        }
    }
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "В этом разделе находятся ссылки на мои социальные сети",
            "С помощью кнопки \"Поделиться\" вы можете поделиться информацией о программе",
            "Также можно прочитать информацию о последнем обновлении, она же доступна в Telegram"
        };
        
        Tutorial.StartTutorial(steps);
    }
    private void OpenMain() { 
        ((App)Application.Current).OpenScreen<MainView, MainViewModel>();
    }

    private void OpenUrlInBrowser(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось открыть ссылку: {ex.Message}");
        }
    }
}