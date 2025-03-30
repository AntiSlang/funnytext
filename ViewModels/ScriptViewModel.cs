using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Eremex.AvaloniaUI.Controls.Editors;
using funnytext.Models;
using funnytext.Services;
using funnytext.Views;

namespace funnytext.ViewModels;

public class ScriptViewModel : ViewModelBase
{
    private ItemModel _selectedItemCreate;
    private int c = 2;
    public ObservableCollection<ItemModel> ItemsCreate { get; set; } = new()
    {
        new ItemModel("Буфер обмена", "avares://funnytext/Assets/tray.png", "0"),
        new ItemModel(FileSettingsService.LoadString("profile_rand_1", "Рандом 1"), "avares://funnytext/Assets/menu_random.png", "1"),
        new ItemModel(FileSettingsService.LoadString("profile_rand_2", "Рандом 2"), "avares://funnytext/Assets/menu_random.png", "2"),
        new ItemModel(FileSettingsService.LoadString("profile_rand_3", "Рандом 3"), "avares://funnytext/Assets/menu_random.png", "3"),
        new ItemModel(FileSettingsService.LoadString("profile_rand_4", "Рандом 4"), "avares://funnytext/Assets/menu_random.png", "4"),
        new ItemModel(FileSettingsService.LoadString("profile_rand_5", "Рандом 5"), "avares://funnytext/Assets/menu_random.png", "5"),
    };
    public ItemModel SelectedItemCreate
    {
        get => _selectedItemCreate;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemCreate, value);
        }
    }
    private ItemModel _selectedItemEdit1;
    public ObservableCollection<ItemModel> ItemsEdit1 { get; set; } = new()
    {
        new ItemModel(FileSettingsService.LoadString("profile_change_1", "Изменение 1"), "avares://funnytext/Assets/menu_start.png", "0"),
        new ItemModel(FileSettingsService.LoadString("profile_change_2", "Изменение 2"), "avares://funnytext/Assets/menu_start.png", "1"),
        new ItemModel(FileSettingsService.LoadString("profile_change_3", "Изменение 3"), "avares://funnytext/Assets/menu_start.png", "2"),
        new ItemModel(FileSettingsService.LoadString("profile_change_4", "Изменение 4"), "avares://funnytext/Assets/menu_start.png", "3"),
        new ItemModel(FileSettingsService.LoadString("profile_change_5", "Изменение 5"), "avares://funnytext/Assets/menu_start.png", "4"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_1", "Шифрование 1"), "avares://funnytext/Assets/menu_encryption.png", "5"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_2", "Шифрование 2"), "avares://funnytext/Assets/menu_encryption.png", "6"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_3", "Шифрование 3"), "avares://funnytext/Assets/menu_encryption.png", "7"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_4", "Шифрование 4"), "avares://funnytext/Assets/menu_encryption.png", "8"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_5", "Шифрование 5"), "avares://funnytext/Assets/menu_encryption.png", "9"),
    };
    public ItemModel SelectedItemEdit1
    {
        get => _selectedItemEdit1;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemEdit1, value);
        }
    }
    
    private ItemModel _selectedItemEdit2;
    public ObservableCollection<ItemModel> ItemsEdit2 { get; set; } = new()
    {
        new ItemModel(FileSettingsService.LoadString("profile_change_1", "Изменение 1"), "avares://funnytext/Assets/menu_start.png", "0"),
        new ItemModel(FileSettingsService.LoadString("profile_change_2", "Изменение 2"), "avares://funnytext/Assets/menu_start.png", "1"),
        new ItemModel(FileSettingsService.LoadString("profile_change_3", "Изменение 3"), "avares://funnytext/Assets/menu_start.png", "2"),
        new ItemModel(FileSettingsService.LoadString("profile_change_4", "Изменение 4"), "avares://funnytext/Assets/menu_start.png", "3"),
        new ItemModel(FileSettingsService.LoadString("profile_change_5", "Изменение 5"), "avares://funnytext/Assets/menu_start.png", "4"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_1", "Шифрование 1"), "avares://funnytext/Assets/menu_encryption.png", "5"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_2", "Шифрование 2"), "avares://funnytext/Assets/menu_encryption.png", "6"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_3", "Шифрование 3"), "avares://funnytext/Assets/menu_encryption.png", "7"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_4", "Шифрование 4"), "avares://funnytext/Assets/menu_encryption.png", "8"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_5", "Шифрование 5"), "avares://funnytext/Assets/menu_encryption.png", "9"),
    };
    public ItemModel SelectedItemEdit2
    {
        get => _selectedItemEdit2;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemEdit2, value);
        }
    }
    
    private ItemModel _selectedItemEdit3;
    public ObservableCollection<ItemModel> ItemsEdit3 { get; set; } = new()
    {
        new ItemModel(FileSettingsService.LoadString("profile_change_1", "Изменение 1"), "avares://funnytext/Assets/menu_start.png", "0"),
        new ItemModel(FileSettingsService.LoadString("profile_change_2", "Изменение 2"), "avares://funnytext/Assets/menu_start.png", "1"),
        new ItemModel(FileSettingsService.LoadString("profile_change_3", "Изменение 3"), "avares://funnytext/Assets/menu_start.png", "2"),
        new ItemModel(FileSettingsService.LoadString("profile_change_4", "Изменение 4"), "avares://funnytext/Assets/menu_start.png", "3"),
        new ItemModel(FileSettingsService.LoadString("profile_change_5", "Изменение 5"), "avares://funnytext/Assets/menu_start.png", "4"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_1", "Шифрование 1"), "avares://funnytext/Assets/menu_encryption.png", "5"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_2", "Шифрование 2"), "avares://funnytext/Assets/menu_encryption.png", "6"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_3", "Шифрование 3"), "avares://funnytext/Assets/menu_encryption.png", "7"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_4", "Шифрование 4"), "avares://funnytext/Assets/menu_encryption.png", "8"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_5", "Шифрование 5"), "avares://funnytext/Assets/menu_encryption.png", "9"),
    };
    public ItemModel SelectedItemEdit3
    {
        get => _selectedItemEdit3;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemEdit3, value);
        }
    }
    
    private ItemModel _selectedItemEdit4;
    public ObservableCollection<ItemModel> ItemsEdit4 { get; set; } = new()
    {
        new ItemModel(FileSettingsService.LoadString("profile_change_1", "Изменение 1"), "avares://funnytext/Assets/menu_start.png", "0"),
        new ItemModel(FileSettingsService.LoadString("profile_change_2", "Изменение 2"), "avares://funnytext/Assets/menu_start.png", "1"),
        new ItemModel(FileSettingsService.LoadString("profile_change_3", "Изменение 3"), "avares://funnytext/Assets/menu_start.png", "2"),
        new ItemModel(FileSettingsService.LoadString("profile_change_4", "Изменение 4"), "avares://funnytext/Assets/menu_start.png", "3"),
        new ItemModel(FileSettingsService.LoadString("profile_change_5", "Изменение 5"), "avares://funnytext/Assets/menu_start.png", "4"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_1", "Шифрование 1"), "avares://funnytext/Assets/menu_encryption.png", "5"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_2", "Шифрование 2"), "avares://funnytext/Assets/menu_encryption.png", "6"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_3", "Шифрование 3"), "avares://funnytext/Assets/menu_encryption.png", "7"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_4", "Шифрование 4"), "avares://funnytext/Assets/menu_encryption.png", "8"),
        new ItemModel(FileSettingsService.LoadString("profile_encryption_5", "Шифрование 5"), "avares://funnytext/Assets/menu_encryption.png", "9"),
    };
    public ItemModel SelectedItemEdit4
    {
        get => _selectedItemEdit4;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemEdit4, value);
        }
    }

    private bool is2Visible = false;
    public bool Is2Visible
    {
        get => is2Visible;
        set
        {
            this.RaiseAndSetIfChanged(ref is2Visible, value);
        }
    }
    private bool is3Visible = false;
    public bool Is3Visible
    {
        get => is3Visible;
        set
        {
            this.RaiseAndSetIfChanged(ref is3Visible, value);
        }
    }
    private bool is4Visible = false;
    public bool Is4Visible
    {
        get => is4Visible;
        set
        {
            this.RaiseAndSetIfChanged(ref is4Visible, value);
        }
    }
    public ReactiveCommand<Unit, Unit> MainViewCommand { get; }
    public ReactiveCommand<Unit, Unit> PlusCommand { get; }
    public ReactiveCommand<Unit, Unit> MinusCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowPresetMenuCommand { get; }
    private string _result = "";
    public string result
    {
        get => _result;
        set => this.RaiseAndSetIfChanged(ref _result, value);
    }
    public void Plus()
    {
        c += 1;
        if (c >= 6)
        {
            c = 5;
        }
        UpdateVisible();
    }
    public void Minus()
    {
        c -= 1;
        if (c <= 1)
        {
            c = 2;
        }
        UpdateVisible();
    }

    public void UpdateVisible()
    {
        MinusEnabled = c > 2;
        PlusEnabled = c < 5;
        Is2Visible = c >= 3;
        Is3Visible = c >= 4;
        Is4Visible = c >= 5;
    }

    public async void TestScript()
    {
        result = await UseScript();
    }
    public ReactiveCommand<Unit, Unit> TestScriptCommand { get; }
    public ReactiveCommand<Unit, Unit> SavePresetCommand { get; }
    public ReactiveCommand<Unit, Unit> LoadPresetCommand { get; }
    public ScriptViewModel()
    {
        UpdateVisible();
        _selectedItemCreate = ItemsCreate[0];
        _selectedItemEdit1 = ItemsEdit1[0];
        _selectedItemEdit2 = ItemsEdit2[0];
        _selectedItemEdit3 = ItemsEdit3[0];
        _selectedItemEdit4 = ItemsEdit4[0];
        MainViewCommand = ReactiveCommand.Create(OpenMain);
        PlusCommand = ReactiveCommand.Create(Plus);
        MinusCommand = ReactiveCommand.Create(Minus);
        ShowPresetMenuCommand = ReactiveCommand.Create(ShowMenu);
        SavePresetCommand = ReactiveCommand.Create(StartSaving);
        LoadPresetCommand = ReactiveCommand.Create(StartLoading);
        ConfirmPresetCommand = ReactiveCommand.Create(ConfirmAction);
        TestScriptCommand = ReactiveCommand.Create(TestScript);
        for (int i = 1; i <= 5; i++)
        {
            Profiles.Add(FileSettingsService.LoadString($"script_{i}", $"Скрипт {i}"));
        }
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(ScriptViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(ScriptViewModel)))
        {
            StartTutorial();
        }
    }
    
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "Здесь вы можете совмещать профили из разных разделов программы в один скрипт",
            "Например: изменить текст из буфера обмена, а потом его зашифровать",
            "Или: сгенерировать рандомный текст, и сразу изменить его",
            "Сделанный скрипт можно протестировать с помощью кнопки Тест, сохранить и позже использовать в системном трее",
        };
        
        Tutorial.StartTutorial(steps);
    }

    public async Task<string> UseScript()
    {
        string text;
        if (_selectedItemCreate.Parameter == "0")
        {
            text = await ((App)Application.Current).GetClipboardTextAsyncNonStatic();
        }
        else
        {
            RandomViewModel randomViewModel = new RandomViewModel();
            randomViewModel.LoadProfile2(int.Parse(_selectedItemCreate.Parameter));
            text = randomViewModel.bruh();
        }

        EncryptionViewModel encryptionViewModel = new EncryptionViewModel();
        ChangeViewModel changeViewModel = new ChangeViewModel();
        int[] selected_items = [int.Parse(_selectedItemEdit1.Parameter), int.Parse(_selectedItemEdit2.Parameter), int.Parse(_selectedItemEdit3.Parameter), int.Parse(_selectedItemEdit4.Parameter)];
        
        for (int i = 0; i < c - 1; i++)
        {
            if (selected_items[i] > 4)
            {
                encryptionViewModel.LoadProfile2(selected_items[i] - 4);
                encryptionViewModel.Input = text;
                text = encryptionViewModel.bruh();
            }
            else
            {
                changeViewModel.LoadProfile2(selected_items[i] + 1);
                changeViewModel.Input = text;
                text = changeViewModel.bruh();
            }
        }

        return text;
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
                
                FileSettingsService.Save($"script_{strtoint}", $"{PresetName}_{strtoint}");
                FileSettingsService.Save($"script_c_{strtoint}", c);
                FileSettingsService.Save($"script_1_{strtoint}", int.Parse(_selectedItemCreate.Parameter));
                FileSettingsService.Save($"script_2_{strtoint}", int.Parse(_selectedItemEdit1.Parameter));
                FileSettingsService.Save($"script_3_{strtoint}", int.Parse(_selectedItemEdit2.Parameter));
                FileSettingsService.Save($"script_4_{strtoint}", int.Parse(_selectedItemEdit3.Parameter));
                FileSettingsService.Save($"script_5_{strtoint}", int.Parse(_selectedItemEdit4.Parameter));
                
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
        c = FileSettingsService.LoadInt($"script_c_{strtoint}", 2);
        UpdateVisible();
        SelectedItemCreate = ItemsCreate[FileSettingsService.LoadInt($"script_1_{strtoint}")];
        SelectedItemEdit1 = ItemsEdit1[FileSettingsService.LoadInt($"script_2_{strtoint}")];
        SelectedItemEdit2 = ItemsEdit2[FileSettingsService.LoadInt($"script_3_{strtoint}")];
        SelectedItemEdit3 = ItemsEdit3[FileSettingsService.LoadInt($"script_4_{strtoint}")];
        SelectedItemEdit4 = ItemsEdit4[FileSettingsService.LoadInt($"script_5_{strtoint}")];
    }

    public ObservableCollection<string> Profiles { get; } = new ObservableCollection<string>();

    private bool _isProfileSelectionVisible;
    public bool IsProfileSelectionVisible
    {
        get => _isProfileSelectionVisible;
        set => this.RaiseAndSetIfChanged(ref _isProfileSelectionVisible, value);
    }

    private bool _plusEnabled;
    public bool PlusEnabled
    {
        get => _plusEnabled;
        set => this.RaiseAndSetIfChanged(ref _plusEnabled, value);
    }

    private bool _minusEnabled;
    public bool MinusEnabled
    {
        get => _minusEnabled;
        set => this.RaiseAndSetIfChanged(ref _minusEnabled, value);
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

    private void OnProfileSelected(string? profile)
    {
        if (profile != null)
        {
            SavingIsEnabled = !string.IsNullOrEmpty(SelectedProfile);
        }
    }

    private string _presetName;
    public string PresetName
    {
        get => _presetName;
        set => this.RaiseAndSetIfChanged(ref _presetName, value);
    }
    private void StartSaving()
    {
        IsProfileSelectionVisible = true;
        IsSaving = true;
        IsConfirmVisible = true;
        SavingIsEnabled = !string.IsNullOrEmpty(SelectedProfile);
    }
    public ReactiveCommand<Unit, Unit> ConfirmPresetCommand { get; }

    private void StartLoading()
    {
        IsProfileSelectionVisible = true;
        IsSaving = false;
        IsConfirmVisible = true;
        SavingIsEnabled = !string.IsNullOrEmpty(SelectedProfile);
    }
    private void ShowMenu()
    {
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = desktop.MainWindow;
            var menu = new ContextMenu();
            menu.Items.Add(new MenuItem { Header = "Сохранить скрипт", Command = SavePresetCommand });
            menu.Items.Add(new MenuItem { Header = "Загрузить скрипт", Command = LoadPresetCommand });

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
    
    private void OpenMain() { 
        ((App)Application.Current).OpenScreen<MainView, MainViewModel>();
    }
}