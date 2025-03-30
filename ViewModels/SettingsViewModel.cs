using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ReactiveUI;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using funnytext.Models;
using funnytext.Services;
using funnytext.Views;

namespace funnytext.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private ItemModel _selectedItemFont;
    private ItemModel _selectedItemLanguage;
    private ItemModel _selectedItemTheme;
    private ItemModel _selectedItemSchedule1;
    private ItemModel _selectedItemSchedule2;
    private ItemModel _selectedItemMusic;
    private ItemModel _selectedItemMake;
    private ItemModel _selectedItemTray;
    private ItemModel _selectedItemStart;
    private bool _isDropdownOpenFont;
    private bool _isDropdownOpenLanguage;
    private bool _isDropdownOpenTheme;
    private bool _isDropdownOpenSchedule1;
    private bool _isDropdownOpenSchedule2;
    private bool _isDropdownOpenMusic;
    private bool _isDropdownOpenMake;
    private bool _isDropdownOpenTray;
    private bool _isDropdownOpenStart;
    private FontFamily _selectedFontFamily;

    public ObservableCollection<ItemModel> ItemsFont { get; set; } = new()
    {
        new ItemModel("Sans Serif", "avares://funnytext/Assets/SansSerif.png", "Sans Serif"),
        new ItemModel("Times New Roman", "avares://funnytext/Assets/TimesNewRoman.png", "Times New Roman"),
        new ItemModel("Courier New", "avares://funnytext/Assets/CourierNew.png", "Courier New")
    };
    public ObservableCollection<ItemModel> ItemsLanguage { get; set; } = new()
    {
        new ItemModel("English", "avares://funnytext/Assets/en.png", "en"),
        new ItemModel("Русский", "avares://funnytext/Assets/ru.png", "ru")
    };
    public ObservableCollection<ItemModel> ItemsTheme { get; set; } = new()
    {
        new ItemModel("Светлая", "avares://funnytext/Assets/day.png", "0"),
        new ItemModel("Тёмная", "avares://funnytext/Assets/night.png", "1"),
        new ItemModel("Арабская светлая", "avares://funnytext/Assets/humanarab.png", "2"),
        new ItemModel("Арабская тёмная", "avares://funnytext/Assets/humanarab.png", "3"),
        new ItemModel(FileSettingsService.LoadString("custom_name_1", "Кастом 1"), "avares://funnytext/Assets/1.png", "4"),
        new ItemModel(FileSettingsService.LoadString("custom_name_2", "Кастом 2"), "avares://funnytext/Assets/2.png", "5"),
        new ItemModel(FileSettingsService.LoadString("custom_name_3", "Кастом 3"), "avares://funnytext/Assets/3.png", "6"),
        new ItemModel(FileSettingsService.LoadString("custom_name_4", "Кастом 4"), "avares://funnytext/Assets/4.png", "7"),
        new ItemModel(FileSettingsService.LoadString("custom_name_5", "Кастом 5"), "avares://funnytext/Assets/5.png", "8"),
        new ItemModel("Расписание", "avares://funnytext/Assets/night.png", "9"),
    };
    public ObservableCollection<ItemModel> ItemsSchedule1 { get; set; } = new()
    {
        new ItemModel("Светлая", "avares://funnytext/Assets/day.png", "0"),
        new ItemModel("Тёмная", "avares://funnytext/Assets/night.png", "1"),
        new ItemModel("Арабская светлая", "avares://funnytext/Assets/humanarab.png", "2"),
        new ItemModel("Арабская тёмная", "avares://funnytext/Assets/humanarab.png", "3"),
        new ItemModel(FileSettingsService.LoadString("custom_name_1", "Кастом 1"), "avares://funnytext/Assets/1.png", "4"),
        new ItemModel(FileSettingsService.LoadString("custom_name_2", "Кастом 2"), "avares://funnytext/Assets/2.png", "5"),
        new ItemModel(FileSettingsService.LoadString("custom_name_3", "Кастом 3"), "avares://funnytext/Assets/3.png", "6"),
        new ItemModel(FileSettingsService.LoadString("custom_name_4", "Кастом 4"), "avares://funnytext/Assets/4.png", "7"),
        new ItemModel(FileSettingsService.LoadString("custom_name_5", "Кастом 5"), "avares://funnytext/Assets/5.png", "8")
    };
    public ObservableCollection<ItemModel> ItemsSchedule2 { get; set; } = new()
    {
        new ItemModel("Светлая", "avares://funnytext/Assets/day.png", "0"),
        new ItemModel("Тёмная", "avares://funnytext/Assets/night.png", "1"),
        new ItemModel("Арабская светлая", "avares://funnytext/Assets/humanarab.png", "2"),
        new ItemModel("Арабская тёмная", "avares://funnytext/Assets/humanarab.png", "3"),
        new ItemModel(FileSettingsService.LoadString("custom_name_1", "Кастом 1"), "avares://funnytext/Assets/1.png", "4"),
        new ItemModel(FileSettingsService.LoadString("custom_name_2", "Кастом 2"), "avares://funnytext/Assets/2.png", "5"),
        new ItemModel(FileSettingsService.LoadString("custom_name_3", "Кастом 3"), "avares://funnytext/Assets/3.png", "6"),
        new ItemModel(FileSettingsService.LoadString("custom_name_4", "Кастом 4"), "avares://funnytext/Assets/4.png", "7"),
        new ItemModel(FileSettingsService.LoadString("custom_name_5", "Кастом 5"), "avares://funnytext/Assets/5.png", "8")
    };
    public ObservableCollection<ItemModel> ItemsMusic { get; set; } = new()
    {
        new ItemModel("Без звуков", "avares://funnytext/Assets/muso.png", "0"),
        new ItemModel("Звуки", "avares://funnytext/Assets/mus.png", "1")
    };
    public ObservableCollection<ItemModel> ItemsMake { get; set; } = new()
    {
        new ItemModel("По нажатию", "avares://funnytext/Assets/click.png", "0"),
        new ItemModel("По изменению", "avares://funnytext/Assets/menu_settings.png", "1")
    };
    public ObservableCollection<ItemModel> ItemsTray { get; set; } = new()
    {
        new ItemModel("Обычно", "avares://funnytext/Assets/window.png", "0"),
        new ItemModel("Трей", "avares://funnytext/Assets/tray.png", "1")
    };
    public ObservableCollection<ItemModel> ItemsStart { get; set; } = new()
    {
        new ItemModel("Нет", "avares://funnytext/Assets/menu_quit.png", "0"),
        new ItemModel("1", "avares://funnytext/Assets/1.png", "1"),
        new ItemModel("2", "avares://funnytext/Assets/2.png", "2"),
        new ItemModel("3", "avares://funnytext/Assets/3.png", "3"),
        new ItemModel("4", "avares://funnytext/Assets/4.png", "4"),
        new ItemModel("5", "avares://funnytext/Assets/5.png", "5")
    };
    public async void OnExportClick(bool theme)
    {
        await FileSettingsService.ExportToFolder(theme);
    }

    public async void OnImportClick()
    {
        await FileSettingsService.ImportFromFile();
        ReloadTheme();
    }
    public ItemModel SelectedItemFont
    {
        get => _selectedItemFont;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemFont, value);
            var value_parameter = ((App)Application.Current).FontToAndroid(value.Parameter);
            SelectedFontFamily = new FontFamily(value_parameter);
            FileSettingsService.Save("font", value_parameter);
        }
    }

    public ItemModel SelectedItemLanguage
    {
        get => _selectedItemLanguage;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemLanguage, value);
            FileSettingsService.Save("language", value.Parameter);
        }
    }

    public ItemModel SelectedItemTheme
    {
        get => _selectedItemTheme;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemTheme, value);
            ChangeTheme(Int32.Parse(value.Parameter));
        }
    }
    public ItemModel SelectedItemSchedule1
    {
        get => _selectedItemSchedule1;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemSchedule1, value);
            FileSettingsService.Save("schedule_theme_1", value.Parameter);
            ((App)Application.Current).SetThemeShort();
            ReloadTheme();
        }
    }
    public ItemModel SelectedItemSchedule2
    {
        get => _selectedItemSchedule2;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemSchedule2, value);
            FileSettingsService.Save("schedule_theme_2", value.Parameter);
            ((App)Application.Current).SetThemeShort();
            ReloadTheme();
        }
    }

    public ItemModel SelectedItemMusic
    {
        get => _selectedItemMusic;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemMusic, value);
            FileSettingsService.Save("music", Int32.Parse(value.Parameter));
        }
    }

    public ItemModel SelectedItemMake
    {
        get => _selectedItemMake;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemMake, value);
            FileSettingsService.Save("make", Int32.Parse(value.Parameter));
        }
    }

    public ItemModel SelectedItemTray
    {
        get => _selectedItemTray;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemTray, value);
            FileSettingsService.Save("tray", Int32.Parse(value.Parameter));
        }
    }

    public ItemModel SelectedItemStart
    {
        get => _selectedItemStart;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemStart, value);
            FileSettingsService.Save("start", Int32.Parse(value.Parameter));
        }
    }

    public bool IsDropdownOpenFont
    {
        get => _isDropdownOpenFont;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenFont, value);
    }
    public bool IsDropdownOpenLanguage
    {
        get => _isDropdownOpenLanguage;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenLanguage, value);
    }
    public bool IsDropdownOpenTheme
    {
        get => _isDropdownOpenTheme;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenTheme, value);
    }
    public bool IsDropdownOpenSchedule1
    {
        get => _isDropdownOpenSchedule1;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenSchedule1, value);
    }
    public bool IsDropdownOpenSchedule2
    {
        get => _isDropdownOpenSchedule2;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenSchedule2, value);
    }
    public bool IsDropdownOpenMusic
    {
        get => _isDropdownOpenMusic;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenMusic, value);
    }
    public bool IsDropdownOpenMake
    {
        get => _isDropdownOpenMake;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenMake, value);
    }
    public bool IsDropdownOpenTray
    {
        get => _isDropdownOpenTray;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenTray, value);
    }
    public bool IsDropdownOpenStart
    {
        get => _isDropdownOpenStart;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenStart, value);
    }

    public FontFamily SelectedFontFamily
    {
        get => _selectedFontFamily;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedFontFamily, value);
            Application.Current.Resources["AppFontFamily"] = value;
        }
    }

    public ReactiveCommand<Unit, bool> ToggleDropdownFontCommand { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownLanguageCommand { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownThemeCommand { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownSchedule1Command { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownSchedule2Command { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownMusicCommand { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownMakeCommand { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownTrayCommand { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownStartCommand { get; }
    public ReactiveCommand<Unit, Unit> MainViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ThemeViewCommand { get; }
    public ReactiveCommand<Unit, Unit> RestartCommand { get; }
    public ReactiveCommand<Unit, Unit> GuideResetCommand { get; }
    public ReactiveCommand<int, Unit> ChangeZnachCommand { get; }

    public void ChangeTheme(int theme, bool filesettings = true)
    {
        IsSchedule = theme == 9;
        if (filesettings)
        {
            FileSettingsService.Save("theme", theme);
        }
        string[] themes =
        [
            "Light", "Dark", "ArabLight", "ArabDark", "Custom1", "Custom2", "Custom3", "Custom4", "Custom5", "Schedule"
        ];
        ((App)Application.Current).SetTheme(themes[theme]);
    }
    private string _tipoftheday = "1";
    public string tipoftheday
    {
        get => _tipoftheday;
        set => this.RaiseAndSetIfChanged(ref _tipoftheday, value);
    }

    public void Restart()
    {
        SelectedItemFont = ItemsFont[0];
        SelectedFontFamily = new FontFamily(((App)Application.Current).FontToAndroid(ItemsFont[0].Parameter));
        SelectedItemLanguage = ItemsLanguage[0];
        SelectedItemTheme = ItemsTheme[0];
        SelectedItemSchedule1 = ItemsSchedule1[0];
        SelectedItemSchedule2 = ItemsSchedule2[1];
        SelectedItemMusic = ItemsMusic[0];
        SelectedItemMake = ItemsMake[0];
        SelectedItemTray = ItemsTray[0];
        SelectedItemStart = ItemsStart[0];
    }
    
    private bool _isSchedule;
    public bool IsSchedule
    {
        get => _isSchedule;
        set => this.RaiseAndSetIfChanged(ref _isSchedule, value);
    }

    private bool _isAndroidDropdownVisible = true;
    public bool IsAndroidDropdownVisible
    {
        get => _isAndroidDropdownVisible;
        set => this.RaiseAndSetIfChanged(ref _isAndroidDropdownVisible, value);
    }

    public void ReloadTheme()
    {
        int current_theme = FileSettingsService.LoadInt("theme");
        ChangeTheme(0, false);
        ChangeTheme(current_theme, false);
    }

    public SettingsViewModel()
    {
        InputKolvo = FileSettingsService.LoadInt("plusminus", 1).ToString();
        ButtonKolvoInteractable = int.Parse(InputKolvo) > 1;
        if (((App)Application.Current).ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            IsAndroidDropdownVisible = false;
        }
        ReloadTheme();
        ((App)Application.Current).InTheme = false;
        ((App)Application.Current).SetThemeShort();
        int number = FileSettingsService.LoadString("font", "Sans Serif") == "Sans Serif"
            ? 0
            : (FileSettingsService.LoadString("font", "Sans Serif") == "Times New Roman" ? 1 : 2);
        Debug.WriteLine($"дебаг {number} {ItemsFont[number].Parameter}");
        SelectedItemFont = ItemsFont[number];
        // _selectedFontFamily = new FontFamily(((App)Application.Current).FontToAndroid(ItemsFont[number].Parameter));

        number = FileSettingsService.LoadString("language", "en") == "en" ? 0 : 1;
        _selectedItemLanguage = ItemsLanguage[number];

        number = FileSettingsService.LoadInt("theme");
        _selectedItemTheme = ItemsTheme[number];

        number = FileSettingsService.LoadInt("schedule_theme_1");
        _selectedItemSchedule1 = ItemsSchedule1[number];

        number = FileSettingsService.LoadInt("schedule_theme_2", 1);
        _selectedItemSchedule2 = ItemsSchedule2[number];

        number = FileSettingsService.LoadInt("music");
        _selectedItemMusic = ItemsMusic[number];

        number = FileSettingsService.LoadInt("make");
        _selectedItemMake = ItemsMake[number];

        number = FileSettingsService.LoadInt("tray");
        _selectedItemTray = ItemsTray[number];

        number = FileSettingsService.LoadInt("start");
        _selectedItemStart = ItemsStart[number];
        
        ToggleDropdownFontCommand = ReactiveCommand.Create(() => IsDropdownOpenFont = !IsDropdownOpenFont);
        ToggleDropdownLanguageCommand = ReactiveCommand.Create(() => IsDropdownOpenLanguage = !IsDropdownOpenLanguage);
        ToggleDropdownThemeCommand = ReactiveCommand.Create(() => IsDropdownOpenTheme = !IsDropdownOpenTheme);
        ToggleDropdownSchedule1Command = ReactiveCommand.Create(() => IsDropdownOpenSchedule1 = !IsDropdownOpenSchedule1);
        ToggleDropdownSchedule2Command = ReactiveCommand.Create(() => IsDropdownOpenSchedule2 = !IsDropdownOpenSchedule2);
        ToggleDropdownMusicCommand = ReactiveCommand.Create(() => IsDropdownOpenMusic = !IsDropdownOpenMusic);
        ToggleDropdownMakeCommand = ReactiveCommand.Create(() => IsDropdownOpenMake = !IsDropdownOpenMake);
        ToggleDropdownTrayCommand = ReactiveCommand.Create(() => IsDropdownOpenTray = !IsDropdownOpenTray);
        ToggleDropdownStartCommand = ReactiveCommand.Create(() => IsDropdownOpenStart = !IsDropdownOpenStart);
        MainViewCommand = ReactiveCommand.Create(OpenMain);
        ThemeViewCommand = ReactiveCommand.Create(OpenTheme);
        RestartCommand = ReactiveCommand.Create(Restart);
        GuideResetCommand = ReactiveCommand.Create(TutorialService.ResetTutorials);
        ChangeZnachCommand = ReactiveCommand.Create<int>(changeznach);

        int r = new Random().Next(1, 10);
        if (FileSettingsService.LoadString("language", "en") == "en") {
            tipoftheday = "Tip of the day: ";
            if (r == 1) {tipoftheday += "In this section, you can configure the app, for example, turn off the music or change the font";} else
            if (r == 2) {tipoftheday += "in the \"Links\" section, you can download other developer applications, or go to social networks";} else
            if (r == 3) {tipoftheday += "find out your statistics in the \"Statistics\" section ";} else
            if (r == 4) {tipoftheday += "the app is updated regularly! Have you already downloaded the latest version?";} else
            if (r == 5) {tipoftheday += "share the game with a friend in the \"Links\" section";} else
            if (r == 6) {tipoftheday += "when you hover over the buttons in the settings, the corresponding tooltip will be displayed";} else
            if (r == 7) {tipoftheday += "you can save the text configuration in the \"Profile\" section and use it later";} else
            if (r == 8) {tipoftheday += "in the \"Training\" section, you can learn the points of interest";} else
            {tipoftheday += "leave a review in the market, please";}
        } else {
            tipoftheday = "Совет дня: ";
            if (r == 1) {tipoftheday += "в этом разделе можно настроить приложение, например, выключить музыку или поменять шрифт";} else
            if (r == 2) {tipoftheday += "в разделе \"Ссылки\" можно скачать другие приложения разработчика, или перейти в соц-сети";} else
            if (r == 3) {tipoftheday += "узнай свою статистику в разделе \"Статистика\"";} else
            if (r == 4) {tipoftheday += "приложение регулярно обновляется! А ты уже скачал последнюю версию?";} else
            if (r == 5) {tipoftheday += "поделись приложением с другом в разделе \"Ссылки\"";} else
            if (r == 6) {tipoftheday += "при наведении на кнопки в настройках отобразится соответствующая подсказка";} else
            if (r == 7) {tipoftheday += "вы можете сохранить конфигурацию текста в разделе \"Профиль\" и позже использовать её";} else
            if (r == 8) {tipoftheday += "в разделе \"Обучение\" можно научиться интересующим моментам";} else
            {tipoftheday += "оставь отзыв в маркете, пожалуйста";}
        }
        IsSchedule = FileSettingsService.LoadInt("theme") == 9;
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(SettingsViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(SettingsViewModel)))
        {
            StartTutorial();
        }
    }
    
    private string _inputKolvo = "1";
    public string InputKolvo
    {
        get => _inputKolvo;
        set => this.RaiseAndSetIfChanged(ref _inputKolvo, value);
    }
    
    private bool _buttonKolvoInteractable;
    public bool ButtonKolvoInteractable
    {
        get => _buttonKolvoInteractable;
        set => this.RaiseAndSetIfChanged(ref _buttonKolvoInteractable, value);
    }

    public void changeznach(int z)
    {
        int q = (Int32.Parse(InputKolvo) + z);
        if (q <= 0) {q = 1;}
        InputKolvo = q.ToString();
        ButtonKolvoInteractable = q > 1;
        FileSettingsService.Save("plusminus", q);
    }
    
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "Здесь вы можете настраивать различные параметры, некоторые из которых доступны только на ПК",
            "Также возможно экспортировать или импортировать созданные темы и профили + скрипты",
            "Если захочется пройти это обучение заново, то поможет кнопка Сбросить обучение",
            "А с помощью кнопки Темы можно перейти на экран создания кастомных тем"
        };
        
        Tutorial.StartTutorial(steps);
    }
    private void OpenMain() { 
        ((App)Application.Current).OpenScreen<MainView, MainViewModel>();
    }
    private void OpenTheme() { 
        ((App)Application.Current).OpenScreen<ThemeView, ThemeViewModel>();
    }
}