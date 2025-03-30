using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using funnytext.Models;
using funnytext.Services;
using funnytext.Views;

namespace funnytext.ViewModels;

public class StatsViewModel : ViewModelBase
{
    private string _stats1;
    public int[] texts;
    public string stats1
    {
        get => _stats1;
        set => this.RaiseAndSetIfChanged(ref _stats1, value);
    }
    private string _stats2;
    public string stats2
    {
        get => _stats2;
        set => this.RaiseAndSetIfChanged(ref _stats2, value);
    }
    private string _stats3;
    public string stats3
    {
        get => _stats3;
        set => this.RaiseAndSetIfChanged(ref _stats3, value);
    }
    private string _favorite;
    public string favorite
    {
        get => _favorite;
        set => this.RaiseAndSetIfChanged(ref _favorite, value);
    }
    public ReactiveCommand<Unit, Unit> ShareCommand { get; }
    public ReactiveCommand<Unit, Unit> MainViewCommand { get; }

    public StatsViewModel()
    {
        ((App)Application.Current).SetThemeShort();
        string s1 = FileSettingsService.LoadInt("changedtext").ToString();
        string s2 = "";
        string sharetext = "";
        if (FileSettingsService.LoadString("language", "en") == "ru") {
            s2 = FileSettingsService.LoadString("favouritetextRU", "Оригинальный текст");
            sharetext = "В приложении \"Смешной текст\" я изменил текст " + s1 + " раз, а мой любимый текст — \"" + s2 + "\". А ты? https://play.google.com/store/apps/details?id=com.antislang.funnytext";
        } else {
            s2 = FileSettingsService.LoadString("favouritetextEN", "Original text");
            sharetext = "In \"Funny text\" application I changed text " + s1 + " times, and my favourite text — \"" + s2 + "\". What about you? https://play.google.com/store/apps/details?id=com.antislang.funnytext";
        }

        ShareCommand = ReactiveCommand.Create(() => App.ShareService.ShareText(sharetext));
        MainViewCommand = ReactiveCommand.Create(OpenMain);

        var chtext = FileSettingsService.LoadInt("changedtext");
        var chtextr = FileSettingsService.LoadInt("changedtextrand");
        var chtexte = FileSettingsService.LoadInt("changedtextencryption");

        if (FileSettingsService.LoadString("language", "en") == "en") {
            stats1 = "You changed text " + chtext + " times";
        } else if (((chtext % 10 == 2) || (chtext % 10 == 3) || (chtext % 10 == 4)) && (chtext != 12) && (chtext != 13) && (chtext != 14))  {
            stats1 = "Вы изменили текст " + chtext + " раза";
        } else {
            stats1 = "Вы изменили текст " + chtext + " раз";
        }

        if (FileSettingsService.LoadString("language", "en") == "en") {
            stats2 = "You generated text " + chtextr + " times";
        } else if (((chtextr % 10 == 2) || (chtextr % 10 == 3) || (chtextr % 10 == 4)) && (chtextr != 12) && (chtextr != 13) && (chtextr != 14))  {
            stats2 = "Вы сгенерировали текст " + chtextr + " раза";
        } else {
            stats2 = "Вы сгенерировали текст " + chtextr + " раз";
        }

        if (FileSettingsService.LoadString("language", "en") == "en") {
            stats3 = "You encrypted text " + chtexte + " times";
        } else if (((chtexte % 10 == 2) || (chtexte % 10 == 3) || (chtexte % 10 == 4)) && (chtexte != 12) && (chtexte != 13) && (chtexte != 14))  {
            stats3 = "Вы шифровали текст " + chtexte + " раза";
        } else {
            stats3 = "Вы шифровали текст " + chtexte + " раз";
        }

        if (FileSettingsService.LoadString("language", "en") == "en") {
            favorite = "Favorite text — ";
        } else {
            favorite = "Любимый текст — ";
        }

        favorite += "\"";

        texts = new int[8];
        texts[0] = FileSettingsService.LoadInt("love0");
        texts[1] = FileSettingsService.LoadInt("love1");
        texts[2] = FileSettingsService.LoadInt("love2");
        texts[3] = FileSettingsService.LoadInt("love3");

        if (love(0)) {
            if (FileSettingsService.LoadString("language", "en") == "ru") {
                favorite += "Оригинальный текст";
                FileSettingsService.Save("favouritetextRU", "Оригинальный текст");
            } else {
                favorite += "Original text";
                FileSettingsService.Save("favouritetextEN", "Original text");
            }
        } else if (love(1)) {
            if (FileSettingsService.LoadString("language", "en") == "ru") {
                favorite += "строчной текст";
                FileSettingsService.Save("favouritetextRU", "Строчной текст");
            } else {
                favorite += "lowercase text";
                FileSettingsService.Save("favouritetextEN", "Lowercase text");
            }
        } else if (love(2)) {
            if (FileSettingsService.LoadString("language", "en") == "ru") {
                favorite += "Первая Буква";
                FileSettingsService.Save("favouritetextRU", "Первая Буква");
            } else {
                favorite += "First Letter";
                FileSettingsService.Save("favouritetextEN", "First Letter");
            }
        } else if (love(3)) {
            if (FileSettingsService.LoadString("language", "en") == "ru") {
                favorite += "Строчная-прописная";
                FileSettingsService.Save("favouritetextRU", "Строчная-прописная");
            } else {
                favorite += "Lowercase-uppercase";
                FileSettingsService.Save("favouritetextEN", "Lowercase-uppercase");
            }
        }

        favorite += "\"";
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(StatsViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(StatsViewModel)))
        {
            StartTutorial();
        }
    }
    
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "На этом экране находится статистика ваших действий в программе, а также любимый вид текста в Изменении"
        };
        
        Tutorial.StartTutorial(steps);
    }
    private bool love(int number) {
        for (int i = 0; i < texts.Length; i++) {
            if (texts[number] < texts[i]) {return false;}
        }
        return true;
    }
    private void OpenMain() { 
        ((App)Application.Current).OpenScreen<MainView, MainViewModel>();
    }
}