using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Text;
using Avalonia;
using Avalonia.Threading;
using funnytext.Views;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using funnytext.Models;

namespace funnytext.ViewModels;
using Services;


public class ChangeViewModel : ViewModelBase
{
    public int global_copied;
    private bool _copied, canSave = true;
    public bool copied
    {
        get => _copied;
        set => this.RaiseAndSetIfChanged(ref _copied, value);
    }
    
    private bool _sharebtn;
    public bool sharebtn
    {
        get => _sharebtn;
        set => this.RaiseAndSetIfChanged(ref _sharebtn, value);
    }
    
    private string _input = "";
    public string Input
    {
        get => _input;
        set => this.RaiseAndSetIfChanged(ref _input, value);
    }

    public ObservableCollection<string> Profiles { get; } = new ObservableCollection<string>();

    private bool _isProfileSelectionVisible;
    public bool IsProfileSelectionVisible
    {
        get => _isProfileSelectionVisible;
        set => this.RaiseAndSetIfChanged(ref _isProfileSelectionVisible, value);
    }

    private bool _isSaving;
    public bool IsSaving
    {
        get => _isSaving;
        set => this.RaiseAndSetIfChanged(ref _isSaving, value);
    }

    private bool _isConfirmVisible;
    public bool IsConfirmVisible
    {
        get => _isConfirmVisible;
        set => this.RaiseAndSetIfChanged(ref _isConfirmVisible, value);
    }

    private string _selectedProfile;
    public string SelectedProfile
    {
        get => _selectedProfile;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedProfile, value);
            OnProfileSelected(value);
        }
    }
    
    private bool _savingIsEnabled;
    public bool SavingIsEnabled
    {
        get => _savingIsEnabled;
        set => this.RaiseAndSetIfChanged(ref _savingIsEnabled, value);
    }
    
    private bool _buttonStartInteractable;
    public bool ButtonStartInteractable
    {
        get => _buttonStartInteractable;
        set => this.RaiseAndSetIfChanged(ref _buttonStartInteractable, value);
    }
    private void OnProfileSelected(string? profile)
    {
        if (profile != null)
        {
            SavingIsEnabled = true;
            if (IsSaving) {
                SavingIsEnabled = canSave;
            }
            SavingIsEnabled = SavingIsEnabled && !string.IsNullOrEmpty(SelectedProfile);
        }
    }

    private string _presetName;
    public string PresetName
    {
        get => _presetName;
        set => this.RaiseAndSetIfChanged(ref _presetName, value);
    }

    private string _result = "";
    public string result
    {
        get => _result;
        set => this.RaiseAndSetIfChanged(ref _result, value);
    }
    
    public ReactiveCommand<Unit, Unit> ShowPresetMenuCommand { get; }
    public ReactiveCommand<Unit, Unit> SavePresetCommand { get; }
    public ReactiveCommand<Unit, Unit> LoadPresetCommand { get; }
    public ReactiveCommand<Unit, Unit> ConfirmPresetCommand { get; }
    public ReactiveCommand<Unit, Unit> MakeCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeViewCommand { get; }
    public ReactiveCommand<Unit, Unit> MainViewCommand { get; }
    public ReactiveCommand<Unit, Unit> RandomSettingsCommand { get; }
    public ReactiveCommand<Unit, Unit> ShareCommand { get; }
    public ChangeViewModel()
    {
        ((App)Application.Current).SetThemeShort();
        MakeCommand = ReactiveCommand.Create(make);
        RandomSettingsCommand = ReactiveCommand.Create(RandomSettings);
        ChangeViewCommand = ReactiveCommand.Create(OpenChange);
        MainViewCommand = ReactiveCommand.Create(OpenMain);
        ShareCommand = ReactiveCommand.Create(() => App.ShareService.ShareText(result));
        ShowPresetMenuCommand = ReactiveCommand.Create(ShowMenu);
        SavePresetCommand = ReactiveCommand.Create(StartSaving);
        LoadPresetCommand = ReactiveCommand.Create(StartLoading);
        ConfirmPresetCommand = ReactiveCommand.Create(ConfirmAction);

        translit = new Dropdown(Itemstranslit, _selectedItemtranslit, v => SelectedItemtranslit = v);
        raskladka = new Dropdown(Itemsraskladka, _selectedItemraskladka, v => SelectedItemraskladka = v);
        repeat = new Dropdown(Itemsrepeat, _selectedItemrepeat, v => SelectedItemrepeat = v);
        afterevery = new Dropdown(Itemsafterevery, _selectedItemafterevery, v => SelectedItemafterevery = v);
        whatevery = new Dropdown(Itemswhatevery, _selectedItemwhatevery, v => SelectedItemwhatevery = v);
        mixdropdown = new Dropdown(Itemsmixdropdown, _selectedItemmixdropdown, v => SelectedItemmixdropdown = v);
        nrwhat = new Dropdown(Itemsnrwhat, _selectedItemnrwhat, v => SelectedItemnrwhat = v);
        nrof = new Dropdown(Itemsnrof, _selectedItemnrof, v => SelectedItemnrof = v);
        nrmode = new Dropdown(Itemsnrmode, _selectedItemnrmode, v => SelectedItemnrmode = v);
        
        shag = "1";
        randommininput = "1";
        randommaxinput = "2";
        inputsmile = "";
        ieb1 = "";
        ieb2 = "";
        nrinpwhat = "";
        nrinphow = "0";
        inputpovtor = "2";
        krome = " ";
        kromesmile = " ";
        podryad = " ";
        
        _toggles.Add(true);
        for (int i = 1; i < 16; i++)
        {
            _toggles.Add(false);
        }

        invtwo_1 = true;
        invtwo = new RadioGroup(
            [() => invtwo_1, () => invtwo_2],
            [val => invtwo_1 = val, val => invtwo_2 = val]
        );
        randomorshagtwo_1 = true;
        randomorshagtwo = new RadioGroup(
            [() => randomorshagtwo_1, () => randomorshagtwo_2],
            [val => randomorshagtwo_1 = val, val => randomorshagtwo_2 = val]
        );
        radiogroup_1 = true;
        radiogroup = new RadioGroup(
            [() => radiogroup_1, () => radiogroup_2, () => radiogroup_3, () => radiogroup_4],
            [val => radiogroup_1 = val, val => radiogroup_2 = val, val => radiogroup_3 = val, val => radiogroup_4 = val]
        );
        
        for (int i = 1; i <= 5; i++)
        {
            Profiles.Add(FileSettingsService.LoadString($"profile_change_{i}", $"Профиль {i}"));
        }
        if (FileSettingsService.LoadInt("start") != 0)
        {
            LoadProfile2(FileSettingsService.LoadInt("start"));
        }
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(ChangeViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(ChangeViewModel)))
        {
            StartTutorial();
        }
    }
    
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "Раздел изменения текста. Текст из поля ввода будет изменяться соответственно различным настройкам",
            "Также вы можете сохранить / загрузить профиль настроек, а позже использовать его в трее или в скриптах"
        };
        
        Tutorial.StartTutorial(steps);
    }
    public void OnChangeSomething()
    {
        coefchecker();
        emptyinputchecker();
        // OnDropdownRepeatValueChanged
        if (repeat.Get() == 0) {
            krome_limit = 1;
            if (krome != "") {
                krome = krome[0].ToString();
            }
        } else {
            krome_limit = 0;
        }
        // OnDropdownSmileValueChanged
        if (whatevery.Get() == 0) {
            kromesmile_limit = 1;
            if (kromesmile != "") {
                kromesmile = kromesmile[0].ToString();
            }
        } else {
            kromesmile_limit = 0;
        }
        // ButtonStartInteractable = Input != "";
        if (IsSaving) {
            SavingIsEnabled = canSave;
        }
        SavingIsEnabled = SavingIsEnabled && !string.IsNullOrEmpty(SelectedProfile);
        if ((FileSettingsService.LoadInt("make") == 1) && (ButtonStartInteractable)) {
            if ((FileSettingsService.LoadInt("music") == 1) && ((FileSettingsService.LoadInt("theme") == 2) || (FileSettingsService.LoadInt("theme") == 3))) {
                AudioPlayer.PlayMp3Async("avares://funnytext/Assets/bruh.mp3");
            }
            resulttext = bruh();
            if (resulttext != result) {
                result = resulttext;
                if (!string.IsNullOrEmpty(resulttext) && resulttext != " ") {
                    CopyText(resulttext, 3);
                    sharebtn = true;
                    FileSettingsService.Save("changedtext", FileSettingsService.LoadInt("changedtext") + 1);
                    string pp = "love" + (radiogroup.Get()).ToString();
                    FileSettingsService.Save(pp, FileSettingsService.LoadInt(pp) + 1);
                }
            }
        }
    }
    public async void make() {
        if ((FileSettingsService.LoadInt("music") == 1) && ((FileSettingsService.LoadInt("theme") == 2) || (FileSettingsService.LoadInt("theme") == 3))) {
            AudioPlayer.PlayMp3Async("avares://funnytext/Assets/bruh.mp3");
        }
        resulttext = bruh();
        result = resulttext;
        if (!string.IsNullOrEmpty(resulttext) && resulttext != " ") {
            CopyText(resulttext, 3);
            sharebtn = true;
            FileSettingsService.Save("changedtext", FileSettingsService.LoadInt("changedtext") + 1);
            string pp = "love" + (radiogroup.Get()).ToString();
            FileSettingsService.Save(pp, FileSettingsService.LoadInt(pp) + 1);
        }
    }
    private void OpenChange()
    {
        ((App)Application.Current).OpenScreen<ChangeView, ChangeViewModel>();
    }
    private void OpenMain() { 
        ((App)Application.Current).OpenScreen<MainView, MainViewModel>();
    }

    private void ShowMenu()
    {
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = desktop.MainWindow;
            var menu = new ContextMenu();
            menu.Items.Add(new MenuItem { Header = "Сохранить профиль", Command = SavePresetCommand });
            menu.Items.Add(new MenuItem { Header = "Загрузить профиль", Command = LoadPresetCommand });

            menu.Open(mainWindow);
        }else if (Avalonia.Application.Current.ApplicationLifetime is ISingleViewApplicationLifetime android)
        {
            if (!(android.MainView is Control targetControl))
                return;

            var flyoutContent = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Spacing = 5,
                Margin = new Thickness(5)
            };

            var btnSave = new Button
            {
                Content = "Сохранить профиль",
                Command = SavePresetCommand
            };

            var btnLoad = new Button
            {
                Content = "Загрузить профиль",
                Command = LoadPresetCommand
            };

            var btnCancel = new Button
            {
                Content = "Отмена"
            };

            flyoutContent.Children.Add(btnSave);
            flyoutContent.Children.Add(btnLoad);
            flyoutContent.Children.Add(btnCancel);

            var flyout = new Flyout
            {
                Content = flyoutContent
            };

            btnCancel.Click += (_, __) => flyout.Hide();

            flyout.ShowAt(targetControl);
        }
    }


    private void StartSaving()
    {
        IsProfileSelectionVisible = true;
        IsSaving = true;
        IsConfirmVisible = true;
        SavingIsEnabled = canSave && !string.IsNullOrEmpty(SelectedProfile);
    }

    private void StartLoading()
    {
        IsProfileSelectionVisible = true;
        IsSaving = false;
        IsConfirmVisible = true;
        SavingIsEnabled = !string.IsNullOrEmpty(SelectedProfile);
    }
    public int StrToInt(string str)
    {
        return Int32.Parse(str.Last().ToString());
    }

    private void ConfirmAction()
    {
        if (IsSaving)
        {
            if (!string.IsNullOrEmpty(SelectedProfile))
            {
                if (string.IsNullOrEmpty(PresetName))
                {
                    PresetName = SelectedProfile.Split(" ")[0];
                }

                int strtoint = StrToInt(SelectedProfile);
                FileSettingsService.Save($"profile_change_{strtoint}", $"{PresetName}_{strtoint}");
                
                FileSettingsService.Save($"profile_change_{strtoint}_input1", shag);
                FileSettingsService.Save($"profile_change_{strtoint}_input2", randommininput);
                FileSettingsService.Save($"profile_change_{strtoint}_input3", randommaxinput);
                FileSettingsService.Save($"profile_change_{strtoint}_input4", inputsmile);
                FileSettingsService.Save($"profile_change_{strtoint}_input5", ieb1);
                FileSettingsService.Save($"profile_change_{strtoint}_input6", ieb2);
                FileSettingsService.Save($"profile_change_{strtoint}_input7", nrinpwhat);
                FileSettingsService.Save($"profile_change_{strtoint}_input8", nrinphow);
                FileSettingsService.Save($"profile_change_{strtoint}_input9", inputpovtor);
                FileSettingsService.Save($"profile_change_{strtoint}_input10", krome);
                FileSettingsService.Save($"profile_change_{strtoint}_input11", kromesmile);
                FileSettingsService.Save($"profile_change_{strtoint}_input12", podryad);

                for (int i = 0; i < 16; i++)
                {
                    FileSettingsService.Save($"profile_change_{strtoint}_toggles{i}", toggles[i]);
                }
                
                FileSettingsService.Save($"profile_change_{strtoint}_radiogroup1", radiogroup.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_radiogroup2", invtwo.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_radiogroup3", randomorshagtwo.Get());
            
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown1", translit.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown2", raskladka.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown3", repeat.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown4", afterevery.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown5", whatevery.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown6", mixdropdown.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown7", nrwhat.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown8", nrof.Get());
                FileSettingsService.Save($"profile_change_{strtoint}_dropdown9", nrmode.Get());
                
                Profiles[StrToInt(SelectedProfile) - 1] = $"{PresetName}_{strtoint}";
                ((App)Application.Current).RebuildTrayMenu();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(SelectedProfile))
            {
                int strtoint = StrToInt(SelectedProfile);
                LoadProfile2(strtoint);
            }
        }
        IsProfileSelectionVisible = false;
        IsSaving = false;
        IsConfirmVisible = false;
    }

    public void LoadProfile2(int strtoint)
    {
        shag = FileSettingsService.LoadString($"profile_change_{strtoint}_input1", "1");
        randommininput = FileSettingsService.LoadString($"profile_change_{strtoint}_input2", "1");
        randommaxinput = FileSettingsService.LoadString($"profile_change_{strtoint}_input3", "2");
        inputsmile = FileSettingsService.LoadString($"profile_change_{strtoint}_input4");
        ieb1 = FileSettingsService.LoadString($"profile_change_{strtoint}_input5");
        ieb2 = FileSettingsService.LoadString($"profile_change_{strtoint}_input6");
        nrinpwhat = FileSettingsService.LoadString($"profile_change_{strtoint}_input7");
        nrinphow = FileSettingsService.LoadString($"profile_change_{strtoint}_input8", "0");
        inputpovtor = FileSettingsService.LoadString($"profile_change_{strtoint}_input9", "2");
        krome = FileSettingsService.LoadString($"profile_change_{strtoint}_input10", " ");
        kromesmile = FileSettingsService.LoadString($"profile_change_{strtoint}_input11", " ");
        podryad = FileSettingsService.LoadString($"profile_change_{strtoint}_input12", " ");

        for (int i = 0; i < 16; i++)
        {
            _toggles[i] = FileSettingsService.LoadBool($"profile_change_{strtoint}_toggles{i}", toggles[i]);
        }
        
        radiogroup.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_radiogroup1"));
        invtwo.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_radiogroup2"));
        randomorshagtwo.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_radiogroup3"));
        Inverse_some_radiogroup();
    
        translit.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown1"));
        raskladka.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown2"));
        repeat.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown3"));
        afterevery.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown4"));
        whatevery.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown5"));
        mixdropdown.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown6"));
        nrwhat.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown7"));
        nrof.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown8"));
        nrmode.Set(FileSettingsService.LoadInt($"profile_change_{strtoint}_dropdown9"));
    }

    void copied_off(int local_copied)
    {
        if (local_copied == global_copied)
        {
            copied = false;
        }
    }

    public void CopyText(string textToCopy, int seconds)
    {
        copied = true;
        var clipboard = Clipboard.Get();
        int local_copied = new Random().Next(1, 100000000);
        global_copied = local_copied;
        clipboard.SetTextAsync(textToCopy);
        if (seconds > 0)
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(seconds)
            };
            timer.Tick += (sender, e) =>
            {
                copied_off(local_copied);
                timer.Stop();
            };
            timer.Start();
        }
    }
    
    private ObservableCollection<bool> _toggles = new ObservableCollection<bool>();
    public ObservableCollection<bool> toggles
    {
        get => _toggles;
        set => this.RaiseAndSetIfChanged(ref _toggles, value);
    }
    public ObservableCollection<EmptyItemModel> Itemstranslit { get; set; } = new()
    {
        new EmptyItemModel("Транслит ru к en", 0),
        new EmptyItemModel("Транслит en к ru", 1)
    };
    private EmptyItemModel _selectedItemtranslit;
    public EmptyItemModel SelectedItemtranslit
    {
        get => _selectedItemtranslit;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemtranslit, value);
        }
    }
    public ObservableCollection<EmptyItemModel> Itemsraskladka { get; set; } = new()
    {
        new EmptyItemModel("Раскладка ru к en", 0),
        new EmptyItemModel("Раскладка en к ru", 1)
    };
    private EmptyItemModel _selectedItemraskladka;
    public EmptyItemModel SelectedItemraskladka
    {
        get => _selectedItemraskladka;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemraskladka, value);
        }
    }
    public ObservableCollection<EmptyItemModel> Itemsrepeat { get; set; } = new()
    {
        new EmptyItemModel("символов", 0),
        new EmptyItemModel("слов", 1),
        new EmptyItemModel("текста", 2),
    };
    private EmptyItemModel _selectedItemrepeat;
    public EmptyItemModel SelectedItemrepeat
    {
        get => _selectedItemrepeat;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemrepeat, value);
        }
    }
    public ObservableCollection<EmptyItemModel> Itemsafterevery { get; set; } = new()
    {
        new EmptyItemModel("после каждого", 0),
        new EmptyItemModel("перед каждым", 1)
    };
    private EmptyItemModel _selectedItemafterevery;
    public EmptyItemModel SelectedItemafterevery
    {
        get => _selectedItemafterevery;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemafterevery, value);
        }
    }
    public ObservableCollection<EmptyItemModel> Itemswhatevery { get; set; } = new()
    {
        new EmptyItemModel("символа", 0),
        new EmptyItemModel("слова", 1)
    };
    private EmptyItemModel _selectedItemwhatevery;
    public EmptyItemModel SelectedItemwhatevery
    {
        get => _selectedItemwhatevery;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemwhatevery, value);
        }
    }
    public ObservableCollection<EmptyItemModel> Itemsmixdropdown { get; set; } = new()
    {
        new EmptyItemModel("слов (cвои буквы)", 0),
        new EmptyItemModel("слов (общие буквы)", 1),
        new EmptyItemModel("текста", 2)
    };
    private EmptyItemModel _selectedItemmixdropdown;
    public EmptyItemModel SelectedItemmixdropdown
    {
        get => _selectedItemmixdropdown;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemmixdropdown, value);
        }
    }
    public ObservableCollection<EmptyItemModel> Itemsnrwhat { get; set; } = new()
    {
        new EmptyItemModel("слева", 0),
        new EmptyItemModel("посередине", 1),
        new EmptyItemModel("справа", 2)
    };
    private EmptyItemModel _selectedItemnrwhat;
    public EmptyItemModel SelectedItemnrwhat
    {
        get => _selectedItemnrwhat;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemnrwhat, value);
        }
    }
    public ObservableCollection<EmptyItemModel> Itemsnrof { get; set; } = new()
    {
        new EmptyItemModel("слова", 0),
        new EmptyItemModel("текста", 1)
    };
    private EmptyItemModel _selectedItemnrof;
    public EmptyItemModel SelectedItemnrof
    {
        get => _selectedItemnrof;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemnrof, value);
        }
    }
    public ObservableCollection<EmptyItemModel> Itemsnrmode { get; set; } = new()
    {
        new EmptyItemModel("переполнение", 0),
        new EmptyItemModel("недобор", 1)
    };
    private EmptyItemModel _selectedItemnrmode;
    public EmptyItemModel SelectedItemnrmode
    {
        get => _selectedItemnrmode;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemnrmode, value);
        }
    }
    public RadioGroup invtwo, randomorshagtwo, radiogroup;
    public Dropdown translit, raskladka, repeat, afterevery, whatevery, mixdropdown, nrwhat, nrof, nrmode;
    public string resulttext, b1;
    private bool _shagsgroup, _krome_parent, _krome_parent_smile, _krome_probel;
    public int sh, randomshag1, randomshag2;
    private char[] chars;
    public bool _changeznachbtnminus, _changeznachprobbtnminus, _changeznachnrbtnminus, _randomminbtnmin, _randommaxbtnmin;
    private bool _invtwo_1, _invtwo_2, _randomorshagtwo_1, _randomorshagtwo_2, _radiogroup_1, _radiogroup_2, _radiogroup_3, _radiogroup_4;
    private string _rgrouptext1 = "Оригинальный текст", _rgrouptext2 = "строчной текст", _rgrouptext3 = "Первая Буква", _rgrouptext4 = "Строчная-прописная с...",
        _shag, _randommininput, _randommaxinput, _inputsmile, _ieb1, _ieb2, _nrinpwhat, _nrinphow, _inputpovtor, _krome = "", _kromesmile = "", _podryad;
    private int _krome_limit, _kromesmile_limit;
    public string rgrouptext1
    {
        get => _rgrouptext1;
        set => this.RaiseAndSetIfChanged(ref _rgrouptext1, value);
    }
    public string rgrouptext2
    {
        get => _rgrouptext2;
        set => this.RaiseAndSetIfChanged(ref _rgrouptext2, value);
    }
    public string rgrouptext3
    {
        get => _rgrouptext3;
        set => this.RaiseAndSetIfChanged(ref _rgrouptext3, value);
    }
    public string rgrouptext4
    {
        get => _rgrouptext4;
        set => this.RaiseAndSetIfChanged(ref _rgrouptext4, value);
    }
    public bool shagsgroup
    {
        get => _shagsgroup;
        set => this.RaiseAndSetIfChanged(ref _shagsgroup, value);
    }
    public bool krome_parent
    {
        get => _krome_parent;
        set => this.RaiseAndSetIfChanged(ref _krome_parent, value);
    }
    public bool krome_parent_smile
    {
        get => _krome_parent_smile;
        set => this.RaiseAndSetIfChanged(ref _krome_parent_smile, value);
    }
    public bool krome_probel
    {
        get => _krome_probel;
        set => this.RaiseAndSetIfChanged(ref _krome_probel, value);
    }
    public bool invtwo_1
    {
        get => _invtwo_1;
        set => this.RaiseAndSetIfChanged(ref _invtwo_1, value);
    }
    public bool invtwo_2
    {
        get => _invtwo_2;
        set => this.RaiseAndSetIfChanged(ref _invtwo_2, value);
    }
    public bool randomorshagtwo_1
    {
        get => _randomorshagtwo_1;
        set => this.RaiseAndSetIfChanged(ref _randomorshagtwo_1, value);
    }
    public bool randomorshagtwo_2
    {
        get => _randomorshagtwo_2;
        set => this.RaiseAndSetIfChanged(ref _randomorshagtwo_2, value);
    }
    public bool radiogroup_1
    {
        get => _radiogroup_1;
        set => this.RaiseAndSetIfChanged(ref _radiogroup_1, value);
    }
    public bool radiogroup_2
    {
        get => _radiogroup_2;
        set => this.RaiseAndSetIfChanged(ref _radiogroup_2, value);
    }
    public bool radiogroup_3
    {
        get => _radiogroup_3;
        set => this.RaiseAndSetIfChanged(ref _radiogroup_3, value);
    }
    public bool radiogroup_4
    {
        get => _radiogroup_4;
        set => this.RaiseAndSetIfChanged(ref _radiogroup_4, value);
    }
    public int krome_limit
    {
        get => _krome_limit;
        set => this.RaiseAndSetIfChanged(ref _krome_limit, value);
    }
    public int kromesmile_limit
    {
        get => _kromesmile_limit;
        set => this.RaiseAndSetIfChanged(ref _kromesmile_limit, value);
    }
    public string shag
    {
        get => _shag;
        set => this.RaiseAndSetIfChanged(ref _shag, value);
    }
    public string randommininput
    {
        get => _randommininput;
        set => this.RaiseAndSetIfChanged(ref _randommininput, value);
    }
    public string randommaxinput
    {
        get => _randommaxinput;
        set => this.RaiseAndSetIfChanged(ref _randommaxinput, value);
    }
    public string inputsmile
    {
        get => _inputsmile;
        set => this.RaiseAndSetIfChanged(ref _inputsmile, value);
    }
    public string ieb1
    {
        get => _ieb1;
        set => this.RaiseAndSetIfChanged(ref _ieb1, value);
    }
    public string ieb2
    {
        get => _ieb2;
        set => this.RaiseAndSetIfChanged(ref _ieb2, value);
    }
    public string nrinpwhat
    {
        get => _nrinpwhat;
        set => this.RaiseAndSetIfChanged(ref _nrinpwhat, value);
    }
    public string nrinphow
    {
        get => _nrinphow;
        set => this.RaiseAndSetIfChanged(ref _nrinphow, value);
    }
    public string inputpovtor
    {
        get => _inputpovtor;
        set => this.RaiseAndSetIfChanged(ref _inputpovtor, value);
    }
    public string krome
    {
        get => _krome;
        set => this.RaiseAndSetIfChanged(ref _krome, value);
    }
    public string kromesmile
    {
        get => _kromesmile;
        set => this.RaiseAndSetIfChanged(ref _kromesmile, value);
    }
    public string podryad
    {
        get => _podryad;
        set => this.RaiseAndSetIfChanged(ref _podryad, value);
    }
    public bool changeznachbtnminus
    {
        get => _changeznachbtnminus;
        set => this.RaiseAndSetIfChanged(ref _changeznachbtnminus, value);
    }
    public bool changeznachprobbtnminus
    {
        get => _changeznachprobbtnminus;
        set => this.RaiseAndSetIfChanged(ref _changeznachprobbtnminus, value);
    }
    public bool changeznachnrbtnminus
    {
        get => _changeznachnrbtnminus;
        set => this.RaiseAndSetIfChanged(ref _changeznachnrbtnminus, value);
    }
    public bool randomminbtnmin
    {
        get => _randomminbtnmin;
        set => this.RaiseAndSetIfChanged(ref _randomminbtnmin, value);
    }
    public bool randommaxbtnmin
    {
        get => _randommaxbtnmin;
        set => this.RaiseAndSetIfChanged(ref _randommaxbtnmin, value);
    }
    private void Start() {
        sharebtn = false;
        if (FileSettingsService.LoadInt("lang", 0) == 0) {
            rgrouptext4 = "Lower-uppercase with...";
        }
        string[] ss = rgrouptext4.Split(' ');
        int counter = 0;
        string b = "";
        bool boolb = true;
        for (int i = 0; i < ss.Length; i++) {
            chars = ss[i].ToCharArray();
            randomshag1 = new Random().Next(1, 3);
            randomshag2 = new Random().Next(randomshag1 + 1, 6);
            sh = new Random().Next(randomshag1, randomshag2);
            for (int j = 0; j < ss[i].Length; j++) {
                counter++;
                if (boolb) {
                    chars[j] = Char.ToUpper(chars[j]);
                } else {
                    chars[j] = Char.ToLower(chars[j]);
                }
                if (counter >= sh) {
                    sh = new Random().Next(randomshag1, randomshag2);
                    boolb = !boolb;
                    counter = 0;
                }
            }
            b += new string(chars) + ' ';
        }
        rgrouptext4 = b.Trim();
    }

    public string GenerateRandom(int len) {
        char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        var s = "";
        var ll = alphabet.Length;
        for (int i = 0; i < len; i++) {
            var rrange = new Random().Next(0, ll);
            s += alphabet[rrange];
            string str = new string(alphabet);
            str = str.Remove(rrange, 1);
            alphabet = str.ToCharArray();
            ll -= 1;
        }
        return s;
    }

    public void RandomSettings() {
        RandomSettingsOne();
        string oldInput = Input;
        Input = " ";
        while (!canSave)
        {
            RandomSettingsOne();
        }
        Input = oldInput;
    }

    public void RandomSettingsOne()
    {
        for (int i = 0; i < _toggles.Count; i++) {
            _toggles[i] = new Random().Next(0, 2) == 1;
        }
        
        if (new Random().Next(0, 2) == 1) {
            invtwo.Set(0);
        } else {
            invtwo.Set(1);
        }
        
        if (new Random().Next(0, 2) == 1) {
            randomorshagtwo.Set(0);
        } else {
            randomorshagtwo.Set(1);
        }
        
        radiogroup.Set(new Random().Next(0, 4));

        translit.Set(new Random().Next(0, 2));
        raskladka.Set(new Random().Next(0, 2));
        repeat.Set(new Random().Next(0, 3));
        afterevery.Set(new Random().Next(0, 2));
        whatevery.Set(new Random().Next(0, 2));
        mixdropdown.Set(new Random().Next(0, 3));
        nrwhat.Set(new Random().Next(0, 3));
        nrof.Set(new Random().Next(0, 2));
        nrmode.Set(new Random().Next(0, 2));

        shag = new Random().Next(1, 5).ToString();
        randommininput = new Random().Next(1, 3).ToString();
        randommaxinput = new Random().Next(Convert.ToInt32(randommininput) + 1, 5).ToString();
        inputsmile = GenerateRandom(new Random().Next(1, 4));
        ieb1 = GenerateRandom(1);
        ieb2 = GenerateRandom(1);
        nrinpwhat = GenerateRandom(new Random().Next(1, 4));
        nrinphow = new Random().Next(1, 4).ToString();
        inputpovtor = new Random().Next(2, 4).ToString();
        podryad = GenerateRandom(1);

        if (kromesmile_limit == 1) {
            kromesmile = GenerateRandom(1);
        } else {
            kromesmile = GenerateRandom(new Random().Next(2, 4));
        }

        if (krome_limit == 1) {
            krome = GenerateRandom(1);
        } else {
            krome = GenerateRandom(new Random().Next(2, 4));
        }

        result = "";
        copied = false;
        OnChangeSomething();
    }

    public string bruh() {
        // Action[] actionarr = new Action[] { () => MyMethod(), ()=> MyMethod2() };
        string b = Input;
        if (string.IsNullOrEmpty(b))
        {
            return "";
        }

        if (toggles[3]) {
            b = trans(b, (translit.Get() == 0));
        }

        if (toggles[4]) {
            b = raskladkavoid(b, raskladka.Get());
        }

        char[] spaces = new char[99999];
        var cc = 0;
        for (int i = 0; i < b.Length; i++) {
            if ((b[i] == ' ') || (b[i] == '\n')) {
                spaces[cc] = b[i];
                cc++;
            }
        }
        string[] ss = b.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);;
        int[] mixlengths = new int[99999];
        string mlenchar = "";
        int counter = 0;
        int jj = 0;
        b = "";
        bool boolb = true;
        for (int i = 0; i < ss.Length; i++) {
            if ((toggles[15]) && (nrof.Get() == 0)) {
                ss[i] = NewReplace(ss[i]);
            }

            if ((toggles[8]) && (mixdropdown.Get() == 0)) {
                char[] newss = new char[]{};
                newss = ss[i].ToCharArray();
                string newssb = "";
                for (int j = 0; j < ss[i].Length; j++) {
                    var rrange = new Random().Next(0, newss.Length);
                    newssb += newss[rrange];
                    string str = new string(newss);
                    str=str.Remove(rrange, 1);
                    newss = str.ToCharArray();
                }

                ss[i] = newssb;
            }

            if ((toggles[13]) && (whatevery.Get() == 1) && !((toggles[14]) && (ss[i] == kromesmile))) {
                if (afterevery.Get() == 0) {
                    ss[i] += inputsmile;
                } else {
                    ss[i] = ss[i].Insert(0, inputsmile);
                }
            }

            if ((toggles[10]) && (repeat.Get() == 1)) {
                if (((toggles[11]) && (ss[i] != krome)) || (!(toggles[11]))) {
                    ss[i] += ss[i];
                }
            }

            if (toggles[0]) {
                if (i != ss.Length - 1) {
                    ss[i] += spaces[i];
                }
            }

            chars = ss[i].ToCharArray();
            if (radiogroup.Get() == 3) {
                if (randomorshagtwo.Get() == 0) {
                    sh = Int32.Parse(shag);
                } else {
                    randomshag1 = Int32.Parse(randommininput);
                    randomshag2 = Int32.Parse(randommaxinput);
                    sh = new Random().Next(randomshag1, randomshag2 + 1);
                }
                for (int j = 0; j < ss[i].Length; j++) {
                    if (randomorshagtwo.Get() == 0) {
                        if ((counter % (sh * 2) >= sh) && (counter % (sh * 2) < sh * 2)) {chars[j] = Char.ToUpper(chars[j]);}
                            else {chars[j] = Char.ToLower(chars[j]);}
                        if (char.IsLetter(chars[j])) {counter++;}
                    } else {
                        counter++;
                        if (boolb) {
                            chars[j] = Char.ToUpper(chars[j]);
                        } else {
                            chars[j] = Char.ToLower(chars[j]);
                        }
                        if (counter >= sh) {
                            sh = new Random().Next(randomshag1, randomshag2 + 1);
                            boolb = !boolb;
                            counter = 0;
                        }
                    }
                }
            }

            if (radiogroup.Get() == 2) {
                chars[0] = Char.ToUpper(chars[0]);
                for (int k = 1; k < chars.Length; k++) {
                    chars[k] = Char.ToLower(chars[k]);
                }
            }

            if ((toggles[8]) && (mixdropdown.Get() == 1)) {
                mixlengths[i] = ss[i].Length;
                for (int j = 0; j < ss[i].Length; j++) {
                    if (ss[i][j] != ' ') {
                        mlenchar += ss[i][j];
                    }
                }
            }

            ss[i] = new string(chars);

            b += ss[i];
        }

        if ((toggles[8]) && (mixdropdown.Get() == 1)) {
            string dsff = "";
            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < mixlengths[i] - 1; j++) {
                    var rrrange = new Random().Next(0, mlenchar.Length);
                    var mlenchararr = mlenchar.ToCharArray();
                    dsff += mlenchararr[rrrange];
                    mlenchar = mlenchar.Remove(rrrange, 1);
                }
                dsff += spaces[i];
            }
            
            b = dsff;
        }

        if (radiogroup.Get() == 1) {b = b.ToLower();}

        if (toggles[2]) {
            chars = b.ToCharArray();
            int l = b.Length;
            b = "";
            for (int i = l - 1; i >= 0; i--) {
                b = b + chars[i];
            }
            chars = b.ToCharArray();
            b = new string(chars);
        }

        if (toggles[9]) {
            string[] arr_ru = {"а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ь", "ы", "ъ", "э", "ю", "я"};
            string[] arr_RU = {"А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ь", "Ы", "Ъ", "Э", "Ю", "Я"};
            string[] arr_ru_replace = {"/-|", "6", "8", "r", "9", "3", "3", ">|<", "'/_", "|/|", "|/|*", "|<", "JI", @"|\/|", "|-|", "0", "/7", "|>", "(", "7", "'/", "<|>", "><", "|_|_", "4", "LLI", "LLL", "'b", "bI", "b", "-)", "|-O", "9I"};
            string[] arr_en = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
            string[] arr_EN = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            string[] arr_en_replace = {"/-|", "8", "(", "|)", "3", "|=", "6", "|-|", "|", ")", "|<", "1", @"|\/|", @"|\|", "0", "|?", "9", "|?", "5", "7", "|_|", @"\/", @"\/\/", "><", "'/", "2"};

            for (int i = 0; i < arr_ru.Length; i++) {
                b = b.Replace(arr_ru[i], arr_ru_replace[i]);
                b = b.Replace(arr_RU[i], arr_ru_replace[i]);
            }

            for (int i = 0; i < arr_en.Length; i++) {
                b = b.Replace(arr_en[i], arr_en_replace[i]);
                b = b.Replace(arr_EN[i], arr_en_replace[i]);
            }
        }

        if (toggles[6]) {
            b = b.Replace(ieb1, ieb2);
        }

        if ((toggles[15]) && (nrof.Get() == 1)) {
            b = NewReplace(b);
        }

        if (toggles[7]) {
            string[] arr1 = new string[]{"e", "E", "е", "Е", "s", "S", "p", "P", "р", "Р", "В", "B", "в", "W", "w", "VV", "vv", "G", "f", "Y", "У", "y", "у", "K", "К", "к", "k", "C", "С", "с", "c", "N", "T", "Т", "т"};
            string[] arr2 = new string[]{"€", "€", "€", "€", "$", "$", "₽", "₽", "₽", "₽", "฿", "฿", "฿", "₩", "₩", "₩", "₩", "₲", "ƒ", "¥", "¥", "¥", "¥", "₭", "₭", "₭", "₭", "₡", "₡", "₡", "₡", "₦", "₸", "₸", "₸"};

            for (int i = 0; i < arr1.Length; i++) {
                b = b.Replace(arr1[i], arr2[i]);
            }
        }

        if (toggles[5]) {
            string[] ch = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            // string[] smesh_ch = {"₀", "₁", "₂", "₃", "₄", "₅", "₆", "₇", "₈", "₉"};
            string[] smesh_ch = {"🕛", "🕐", "🕑", "🕒", "🕓", "🕔", "🕕", "🕖", "🕗", "🕘"};

            for (int i = 0; i < ch.Length; i++) {
                b = b.Replace(ch[i], smesh_ch[i]);
            }
        }

        if ((toggles[13]) && (whatevery.Get() == 0)) {
            string c = b;
            int l = b.Length;
            b = "";

            for (int i = 0; i < l; i++) {
                if (!((toggles[14]) && (c[i] == kromesmile[0]))) {
                    if (afterevery.Get() == 0) {
                        b += c[i];
                        b += inputsmile;
                    } else {
                        b += inputsmile;
                        b += c[i];
                    }
                } else {
                    b += c[i];
                }
            }
        }

        if ((toggles[10]) && (repeat.Get() == 0)) {
            string c = b;
            int l = b.Length;
            b = "";

            for (int i = 0; i < l; i++) {
                b += c[i];
                if (Int32.Parse(inputpovtor) > 1) {
                    for (int j = 0; j < Int32.Parse(inputpovtor) - 1; j++) {
                        if (((toggles[11]) && (c[i] != krome[0])) || (!(toggles[11]))) {
                            b += c[i];
                        }
                    }
                }
            }
        }

        if (toggles[1]) {
            string c = b;
            int l = b.Length;
            b = "";

            for (int i = 0; i < l; i++) {
                b += c[i];
                b += " ";
            }
        }

        if (toggles[12]) {
            string x1 = podryad;
            string x2 = podryad + podryad;
            for (int i = 0; i < 1000; i++) {
                b = b.Replace(x2, x1);
            }
        }

        if (invtwo.Get() == 1) {
            b = Inverse(b);
        }

        //b = b.Replace("  ", " ");

        // b = CodesToText(b);

        if ((toggles[10]) && (repeat.Get() == 2)) {
            if (((toggles[11]) && (b != krome)) || (!(toggles[11]))) {
                b += b;
            }
        }
        
        if ((toggles[8]) && (mixdropdown.Get() == 2)) {
            b = b.TrimEnd();
            char[] newbs = new char[]{};
            newbs = b.ToCharArray();
            string newbbs = "";
            for (int j = 0; j < b.Length; j++) {
                var rrange = new Random().Next(0, newbs.Length);
                newbbs += newbs[rrange];
                string str = new string(newbs);
                str=str.Remove(rrange, 1);
                newbs = str.ToCharArray();
            }

            b = newbbs;
        }

        return b;
    }
    public string NewReplace(string b) {
        int nrint = Int32.Parse(nrinphow);
        string nrstr = nrinpwhat;
        // if ((b.Length == nrint) || ((b.Length <= nrint) && ((nrstr.Length <= nrint) || (nrmode.value == 0)))) {
        if ((b.Length == nrint) || ((b.Length <= nrint) && ((nrmode.Get() == 0) || ((nrmode.Get() == 1) && (nrstr.Length <= nrint))))) {
            b = nrstr;
        } else if ((b.Length < nrint) && (nrmode.Get() == 1)) {
            if (nrwhat.Get() == 0) {
                b = nrstr.Substring(0, b.Length);
            } else if (nrwhat.Get() == 1) {
                try {
                    b = nrstr.Substring(b.Length / 2 - nrint, b.Length / 2 + nrint);
                } catch (Exception e) {
                    b = nrstr.Substring(0, b.Length);
                }
            } else {
                // b = nrstr.Substring(b.Length - nrint);
                b = nrstr.Substring(nrint - b.Length);
            }
        } else {
            if (nrwhat.Get() == 0) {
                var bb = nrstr;
                bb += b.Substring(nrint);
                b = bb;
            } else if (nrwhat.Get() == 2) {
                // var bb = b.Substring(0, b.Length - nrint - 1);
                var bb = b.Substring(0, b.Length - nrint);
                bb += nrstr;
                b = bb;
            } else {
                if (((b.Length % 2 == 1) || (b.Length % 2 == 0)) && (nrint % 2 == 0)) {
                    var bb = b.Substring(0, b.Length / 2 - nrint / 2);
                    bb += nrstr;
                    bb += b.Substring(b.Length / 2 + nrint / 2);
                    b = bb;
                } else if ((b.Length % 2 == 1) && (nrint % 2 == 1)) {
                    var bb = b.Substring(0, b.Length / 2 - nrint / 2);
                    bb += nrstr;
                    bb += b.Substring(b.Length / 2 + nrint / 2 + 1);
                    b = bb;
                } else if ((b.Length % 2 == 0) && (nrint % 2 == 1)) {
                    var bb = b.Substring(0, b.Length / 2 - nrint / 2 - 1);
                    bb += nrstr;
                    bb += b.Substring(b.Length / 2 + nrint / 2);
                    b = bb;
                }
            }
        }
        return b;
    }
    public void Inverse_some_radiogroup() {
        if (char.IsUpper(rgrouptext1[0]) != (invtwo.Get() == 0))
        {
            rgrouptext1 = Inverse(rgrouptext1);
            rgrouptext2 = Inverse(rgrouptext2);
            rgrouptext3 = Inverse(rgrouptext3);
            rgrouptext4 = Inverse(rgrouptext4);
        }
        OnChangeSomething();
    }
    public string Inverse(string b) {
        chars = b.ToCharArray();
        int l = b.Length;
        b = "";
        for (int i = l - 1; i >= 0; i--) {
            if (Char.IsUpper(chars[i])) {
                chars[i] = Char.ToLower(chars[i]);
            } else if (Char.IsLower(chars[i])) {
                chars[i] = Char.ToUpper(chars[i]);
            }
        }
        b = new string(chars);
        return b;
    }

    public void changeznach(int z) {
        var zz = FileSettingsService.LoadInt("plusminus", 1);
        if (z < 0) {
            zz = -zz;
        }
        if ((shag == "-") || (shag == "+")){
            int q = 1;
            shag = q.ToString();
        } else {
            int q = (Int32.Parse(shag) + zz);
            if (q <= 0) {q = 1;}
            shag = q.ToString();
            if (q <= 1) {changeznachbtnminus = false;}
                else {changeznachbtnminus = true;}
        }
    }

    public void changeznachprob(int z) {
        var zz = FileSettingsService.LoadInt("plusminus", 1);
        if (z < 0) {
            zz = -zz;
        }
        if ((inputpovtor == "-") || (inputpovtor == "+")){
            int q = 1;
            inputpovtor = q.ToString();
        } else {
            int q = (Int32.Parse(inputpovtor) + zz);
            if (q <= 1) {q = 2;}
            inputpovtor = q.ToString();
            if (q <= 2) {changeznachprobbtnminus = false;}
                else {changeznachprobbtnminus = true;}
        }
    }

    public void changeznachrandommin(int z) {
        var zz = FileSettingsService.LoadInt("plusminus", 1);
        if (z < 0) {
            zz = -zz;
        }
        if ((randommininput == "-") || (randommininput == "+")){
            int q = 1;
            randommininput = q.ToString();
        } else {
            int q = (Int32.Parse(randommininput) + zz);
            if (q <= 0) {q = 1;}
            randommininput = q.ToString();
            if (q <= 1) {randomminbtnmin = false;}
                else {randomminbtnmin = true;}
        }
    }

    public void changeznachrandommax(int z) {
        var zz = FileSettingsService.LoadInt("plusminus", 1);
        if (z < 0) {
            zz = -zz;
        }
        if ((randommaxinput == "-") || (randommaxinput == "+")){
            int q = 1;
            randommaxinput = q.ToString();
        } else {
            int q = (Int32.Parse(randommaxinput) + zz);
            if (q <= 1) {q = 2;}
            randommaxinput = q.ToString();
            if (q <= 2) {randommaxbtnmin = false;}
                else {randommaxbtnmin = true;}
        }
    }

    public void changeznachnr(int z) {
        var zz = FileSettingsService.LoadInt("plusminus", 1);
        if (z < 0) {
            zz = -zz;
        }
        if ((nrinphow == "-") || (nrinphow == "+")){
            int q = 1;
            nrinphow = q.ToString();
        } else {
            int q = (Int32.Parse(nrinphow) + zz);
            if (q <= -1) {q = 0;}
            nrinphow = q.ToString();
            if (q <= 0) {changeznachnrbtnminus = false;}
                else {changeznachnrbtnminus = true;}
        }
    }

    public void coefchecker() {
        if (!string.IsNullOrEmpty(shag) && (shag != "-")) {
            if (Int32.Parse(shag) <= 1) {changeznachbtnminus = false; shag = "1";}
                else {changeznachbtnminus = true;}
        } else {
            changeznachbtnminus = false;
            shag = "1";
        }

        
        if ((!string.IsNullOrEmpty(inputpovtor)) && (inputpovtor != "-")) {
            if (Int32.Parse(inputpovtor) <= 2) {changeznachprobbtnminus = false; inputpovtor = "2";}
                else {changeznachprobbtnminus = true;}
        } else {
            changeznachprobbtnminus = false;
            inputpovtor = "2";
        }

        
        if ((!string.IsNullOrEmpty(nrinphow)) && (nrinphow != "-")) {
            if (Int32.Parse(nrinphow) <= 0) {changeznachnrbtnminus = false; nrinphow = "0";}
                else {changeznachnrbtnminus = true;}
        } else {
            changeznachnrbtnminus = false;
            nrinphow = "0";
        }

        
        if ((!string.IsNullOrEmpty(randommininput)) && (randommininput != "-")) {
            if (Int32.Parse(randommininput) <= 1) {randomminbtnmin = false; randommininput = "1";}
                else {randomminbtnmin = true;}
        } else {
            randomminbtnmin = false;
            randommininput = "1";
        }

        
        if ((!string.IsNullOrEmpty(randommaxinput)) && (randommaxinput != "-")) {
            if (Int32.Parse(randommaxinput) <= 2) {randommaxbtnmin = false; randommaxinput = "2";}
                else {randommaxbtnmin = true;}
        } else {
            randommaxbtnmin = false;
            randommaxinput = "2";
        }

        
        if ((!string.IsNullOrEmpty(randommaxinput)) && (randommaxinput != "-") && (!string.IsNullOrEmpty(randommininput)) && (randommininput != "-")) {
            if (Int32.Parse(randommaxinput) < Int32.Parse(randommininput)) {
                var temp = randommaxinput;
                randommaxinput = randommininput;
                randommininput = temp;
                emptyinputchecker();
                coefchecker();
            }
        }
    }

    public void emptyinputchecker() {
        canSave = !(((toggles[11]) && (krome == "")) || ((toggles[14]) && (kromesmile == "")) || ((toggles[6]) && (ieb1 == "")) || ((toggles[13]) && (inputsmile == "")) || ((toggles[12]) && (podryad == "")) || ((toggles[10]) && (inputpovtor == "")) || (!(toggles[10]) && (toggles[11])) || (!(toggles[13]) && (toggles[14])));
        ButtonStartInteractable = canSave && !String.IsNullOrEmpty(Input);
    }

    public string CodesToText(string s)
    {
        string res = "";

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '\\') {
                res = res + HexesToChar(s.Substring(i + 2, 4));
                i += 5;
            }
            else
            {
                res = res + s[i];
            }
        }

        return res;
    }

    string HexesToChar(string s) {
        return Encoding.UTF8.GetString(HexesToByte(s));
    }

    byte[] HexesToByte(string s) {
        return new byte[] { HexToByte(s.Substring(2, 2)), HexToByte(s.Substring(0, 2)) };
    }

    byte HexToByte(string s)
    {
        return (byte)(HexToByte(s[0]) * 16 + HexToByte(s[1]));
    }

    byte HexToByte(char c)
    {
        if ('0' <= c && c <= '9')
            return byte.Parse(c.ToString());
        switch (c) {
            case 'A': return 10; 
            case 'B': return 11; 
            case 'C': return 12; 
            case 'D': return 13; 
            case 'E': return 14; 
            case 'F': return 15; 
        }
        return 0;
    }
    public void activeshags(bool activeshagsis) {
        shagsgroup = activeshagsis;
    }
    public void activekrome() {
        krome_parent = !krome_parent;
        krome_probel = krome_parent || krome_parent_smile;
        toggles[11] = false;
    }
    public void activekromesmile() {
        krome_parent_smile = !krome_parent_smile;
        krome_probel = krome_parent || krome_parent_smile;
        toggles[14] = false;
    }

    private string trans(string s, bool ruoren)
    {
        string ret = s;
        string[] rus = {"Ё", "Ж", "Ч", "Щ", "Ш", "Ъ", "Ы", "Ь", "Э", "Ю", "Я", "Ц", "А", "Б", "В", "Г", "Д", "Е", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "К", "В"};
        string[] rus2 = {"ё", "ж", "ч", "щ", "ш", "ъ", "ы", "ь", "э", "ю", "я", "ц", "а", "б", "в", "г", "д", "е", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "к", "в"};
        string[] eng = {"Jo", "Zh", "Ch", "Shh", "Sh", "#", "Y", "'", "Je", "Ju", "Ja", "C", "A", "B", "V", "G", "D", "E", "Z", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "H", "Q", "W"};
        string[] eng3 = {"JO", "ZH", "CH", "SHH", "SH", "#", "Y", "'", "JE", "JU", "JA", "C", "A", "B", "V", "G", "D", "E", "Z", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "H", "Q", "W"};
        string[] eng4 = {"jO", "zH", "cH", "ShH", "sH", "#", "Y", "'", "jE", "jU", "jA", "C", "A", "B", "V", "G", "D", "E", "Z", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "H", "Q", "W"};
        string[] eng2 = {"jo", "zh", "ch", "shh", "sh", "#", "y", "'", "je", "ju", "ja", "c", "a", "b", "v", "g", "d", "e", "z", "i", "j", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h", "q", "w"};

        if (ruoren) {
            ret = ret.Replace("SHh", "Щ");
            ret = ret.Replace("sHH", "Щ");
            ret = ret.Replace("sHh", "Щ");
            ret = ret.Replace("shH", "Щ");
        }
        
        for (int i = 0; i < rus.Length; i++) {
            if (ruoren) {
                ret = ret.Replace(rus[i], eng[i]);
                ret = ret.Replace(rus2[i], eng2[i]);
            } else {
                ret = ret.Replace(eng[i], rus[i]);
                ret = ret.Replace(eng3[i], rus[i]);
                ret = ret.Replace(eng4[i], rus[i]);
                ret = ret.Replace(eng2[i], rus2[i]);
            }
        }
        
        return ret;
    }

    public string raskladkavoid (string s, int langfrom = 0) {
        char[] arrs = new char[]{'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'};
        char[] arrb = new char[]{'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я'};
        char[] arrs2 = new char[]{'f', ',', 'd', 'u', 'l', 't', '`', ';', 'p', 'b', 'q', 'r', 'k', 'v', 'y', 'j', 'g', 'h', 'c', 'n', 'e', 'a', 'х', '[', 'x', 'i', 'o', ']', 's', 'm', '\'', '.', 'z'};
        char[] arrb2 = new char[]{'F', ',', 'D', 'U', 'L', 'T', '`', ';', 'P', 'B', 'Q', 'R', 'K', 'V', 'Y', 'J', 'G', 'H', 'C', 'N', 'E', 'A', 'Х', '[', 'X', 'I', 'O', ']', 'S', 'M', '\'', '.', 'Z'};
        
        if (langfrom == 0) {
            for (int i = 0; i < arrs.Length; i++) {
                s = s.Replace(arrs[i], arrs2[i]);
                s = s.Replace(arrb[i], arrb2[i]);
            }
        } else {
            for (int i = 0; i < arrs.Length; i++) {
                s = s.Replace(arrs2[i], arrs[i]);
                s = s.Replace(arrb2[i], arrb[i]);
            }
        }
        return s;
    }
}