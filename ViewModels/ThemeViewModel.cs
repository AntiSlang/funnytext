using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Eremex.AvaloniaUI.Controls.Editors;
using funnytext.Models;
using funnytext.Services;
using funnytext.Views;
using Application = Avalonia.Application;
using Debug = System.Diagnostics.Debug;
#if ANDROID
using Android.OS;
#endif

namespace funnytext.ViewModels;

public class ThemeViewModel : ViewModelBase
{
    private string filePath = "";
    private string filePath2 = "";
    private IBrush _backgroundColor = Brushes.White;
    private IBrush _elementColor = Brushes.Gray;
    private IBrush _elementAccentColor = Brushes.Gray;
    private IBrush _textColor = Brushes.Black;
    private IBrush _borderColor = Brushes.Black;
    private IBrush _backgroundImageBrush = Brushes.White;
    private IBrush _backgroundImage2Brush = Brushes.White;
    private ItemModel _selectedItemLoad;
    private ItemModel _selectedItemBase;
    private ItemModel _selectedItemSave;
    private bool _isDropdownOpenLoad;
    private bool _isDropdownOpenBase;
    private bool _isDropdownOpenSave;
    public ObservableCollection<ItemModel> ItemsLoad { get; set; } = new()
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
    public ObservableCollection<ItemModel> ItemsBase { get; set; } = new()
    {
        new ItemModel("Светлая", "avares://funnytext/Assets/day.png", "0"),
        new ItemModel("Тёмная", "avares://funnytext/Assets/night.png", "1")
    };
    public ObservableCollection<ItemModel> ItemsSave { get; set; } = new()
    {
        new ItemModel(FileSettingsService.LoadString("custom_name_1", "Кастом 1"), "avares://funnytext/Assets/1.png", "1"),
        new ItemModel(FileSettingsService.LoadString("custom_name_2", "Кастом 2"), "avares://funnytext/Assets/2.png", "2"),
        new ItemModel(FileSettingsService.LoadString("custom_name_3", "Кастом 3"), "avares://funnytext/Assets/3.png", "3"),
        new ItemModel(FileSettingsService.LoadString("custom_name_4", "Кастом 4"), "avares://funnytext/Assets/4.png", "4"),
        new ItemModel(FileSettingsService.LoadString("custom_name_5", "Кастом 5"), "avares://funnytext/Assets/5.png", "5")
    };
    public ItemModel SelectedItemLoad
    {
        get => _selectedItemLoad;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemLoad, value);
            try
            {
                filePath = CustomThemes.Colors[int.Parse(value.Parameter)][3];
                if (!string.IsNullOrEmpty(filePath))
                {
                    BackgroundImageBrush = ((App)Application.Current).GetImageBrush(filePath);
                }
                else
                {
                    ClearImage();
                }
                filePath2 = CustomThemes.Colors[int.Parse(value.Parameter)][4];
                if (!string.IsNullOrEmpty(filePath2)) {
                    BackgroundImage2Brush = ((App)Application.Current).GetImageBrush(filePath2);
                }
                else
                {
                    ClearImage2();
                }
            }
            catch (Exception)
            {
                filePath = "";
                filePath2 = "";
            }
            BackgroundColor = new SolidColorBrush(Color.Parse(CustomThemes.Colors[int.Parse(value.Parameter)][0]));
            ElementColor = new SolidColorBrush(Color.Parse(CustomThemes.Colors[int.Parse(value.Parameter)][1]));
            TextColor = new SolidColorBrush(Color.Parse(CustomThemes.Colors[int.Parse(value.Parameter)][2]));
            ElementAccentColor = new SolidColorBrush(Color.Parse(CustomThemes.Colors[int.Parse(value.Parameter)][7]));
            BorderColor = new SolidColorBrush(Color.Parse(CustomThemes.Colors[int.Parse(value.Parameter)][8]));
            InputText = CustomThemes.Colors[int.Parse(value.Parameter)][5];
            SelectedItemBase = ItemsBase[CustomThemes.Colors[int.Parse(value.Parameter)][6] == "Dark" ? 1 : 0];
            if (int.Parse(_selectedItemLoad.Parameter) >= 4)
            {
                SelectedItemSave = ItemsSave[int.Parse(_selectedItemLoad.Parameter) - 4];
            }
        }
    }
    public ItemModel SelectedItemBase
    {
        get => _selectedItemBase;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemBase, value);
        }
    }
    public ItemModel SelectedItemSave
    {
        get => _selectedItemSave;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemSave, value);
        }
    }
    public bool IsDropdownOpenLoad
    {
        get => _isDropdownOpenLoad;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenLoad, value);
    }
    public bool IsDropdownOpenBase
    {
        get => _isDropdownOpenBase;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenBase, value);
    }
    public bool IsDropdownOpenSave
    {
        get => _isDropdownOpenSave;
        set => this.RaiseAndSetIfChanged(ref _isDropdownOpenSave, value);
    }
    public ReactiveCommand<Unit, bool> ToggleDropdownLoadCommand { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownBaseCommand { get; }
    public ReactiveCommand<Unit, bool> ToggleDropdownSaveCommand { get; }

    private Color _editorColor;
    public Color EditorColor
    {
        get => _editorColor;
        set => this.RaiseAndSetIfChanged(ref _editorColor, value);
    }

    public IBrush BackgroundColor
    {
        get => _backgroundColor;
        set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
    }
    public IBrush ElementColor
    {
        get => _elementColor;
        set => this.RaiseAndSetIfChanged(ref _elementColor, value);
    }
    public IBrush ElementAccentColor
    {
        get => _elementAccentColor;
        set => this.RaiseAndSetIfChanged(ref _elementAccentColor, value);
    }
    public IBrush TextColor
    {
        get => _textColor;
        set => this.RaiseAndSetIfChanged(ref _textColor, value);
    }
    public IBrush BorderColor
    {
        get => _borderColor;
        set => this.RaiseAndSetIfChanged(ref _borderColor, value);
    }

    public IBrush BackgroundImageBrush
    {
        get => _backgroundImageBrush;
        set => this.RaiseAndSetIfChanged(ref _backgroundImageBrush, value);
    }
    public IBrush BackgroundImage2Brush
    {
        get => _backgroundImage2Brush;
        set => this.RaiseAndSetIfChanged(ref _backgroundImage2Brush, value);
    }

    public ReactiveCommand<Unit, Unit> SelectBackgroundColorCommand { get; }
    public ReactiveCommand<Unit, Unit> SelectElementColorCommand { get; }
    public ReactiveCommand<Unit, Unit> SelectElementAccentColorCommand { get; }
    public ReactiveCommand<Unit, Unit> SelectTextColorCommand { get; }
    public ReactiveCommand<Unit, Unit> SelectBorderColorCommand { get; }
    public ReactiveCommand<string, Unit> SelectBackgroundImageCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearImageCommand { get; }
    public ReactiveCommand<Unit, Unit> SelectBackgroundImage2Command { get; }
    public ReactiveCommand<Unit, Unit> ClearImage2Command { get; }

    private async Task PickBackgroundImage(string number)
    {
        string tempFilePath = "";
        ImageBrush tempBackgroundImageBrush = new();
        if (Application.Current.ApplicationLifetime is ISingleViewApplicationLifetime android)
        {
            try
            {
                var topLevel = TopLevel.GetTopLevel(android.MainView);
                if (topLevel == null) return;
                var filePicker = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Выберите изображение",
                    AllowMultiple = false,
                    FileTypeFilter = new[] { FilePickerFileTypes.ImageAll },
                    SuggestedStartLocation = await topLevel.StorageProvider.TryGetWellKnownFolderAsync(WellKnownFolder.Videos)
                });
                if (filePicker.Count == 0) return;
                /*await using var stream = await filePicker[0].OpenReadAsync();
                using var reader = new StreamReader(stream);
                tempFilePath = filePicker[0].Path.ToString(); 
                //tempFilePath = await SaveToInternalStorageAsync(filePicker[0]);
                tempBackgroundImageBrush = new ImageBrush{Source = new Bitmap(stream), Stretch = Stretch.Fill};*/
                string appDataPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "FunnyText");
                Directory.CreateDirectory(appDataPath);
                tempFilePath = Path.Combine(appDataPath, $"background_{number}{Path.GetExtension(filePicker[0].Name)}");
                await using (var input = await filePicker[0].OpenReadAsync())
                await using (var output = File.Create(tempFilePath))
                {
                    await input.CopyToAsync(output);
                }
                await using var stream = await filePicker[0].OpenReadAsync();
                tempBackgroundImageBrush = new ImageBrush{Source = new Bitmap(stream), Stretch = Stretch.Fill};
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ошибка: {e}");
            }
        }
        else
        {
            try
            {
                var topLevel = TopLevel.GetTopLevel(App._mainWindow);
                if (topLevel == null) return;

                var storageProvider = topLevel.StorageProvider;
                var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Выберите изображение",
                    AllowMultiple = false
                });

                if (files.Count == 0) return;
                
                tempFilePath = files[0].Path.LocalPath;
                tempBackgroundImageBrush = new ImageBrush{Source = new Bitmap(tempFilePath), Stretch = Stretch.Fill};
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ошибка: {e}");
            }
        }

        ((App)Application.Current).RequestPermission(tempFilePath);
        
        if (number == "1")
        {
            filePath = tempFilePath;
            BackgroundImageBrush = tempBackgroundImageBrush;
        } else
        {
            filePath2 = tempFilePath;
            BackgroundImage2Brush = tempBackgroundImageBrush;
        }
    }
    public void ClearImage()
    {
        filePath = "";
        BackgroundImageBrush = new SolidColorBrush(Colors.White);
    }
    public void ClearImage2()
    {
        filePath2 = "";
        BackgroundImage2Brush = new SolidColorBrush(Colors.White);
    }
    public ReactiveCommand<Unit, Unit> SettingsViewCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    public void SaveTheme()
    {
        FileSettingsService.Save($"custom_back_{_selectedItemSave.Parameter}", filePath);
        FileSettingsService.Save($"custom_back_2_{_selectedItemSave.Parameter}", filePath2);
        FileSettingsService.Save($"custom_color_1_{_selectedItemSave.Parameter}", ShortenColor(BackgroundColor));
        FileSettingsService.Save($"custom_color_2_{_selectedItemSave.Parameter}", ShortenColor(ElementColor));
        FileSettingsService.Save($"custom_color_3_{_selectedItemSave.Parameter}", ShortenColor(TextColor));
        FileSettingsService.Save($"custom_color_4_{_selectedItemSave.Parameter}", ShortenColor(ElementAccentColor));
        FileSettingsService.Save($"custom_color_5_{_selectedItemSave.Parameter}", ShortenColor(BorderColor));
        FileSettingsService.Save($"custom_name_{_selectedItemSave.Parameter}", $"{InputText}_{_selectedItemSave.Parameter}");
        FileSettingsService.Save($"custom_base_{_selectedItemSave.Parameter}", _selectedItemBase.Parameter == "0" ? "Light" : "Dark");

        CustomThemes.Reload();
    }
    private string _inputText = "";

    public string InputText
    {
        get => _inputText;
        set => this.RaiseAndSetIfChanged(ref _inputText, value);
    }

    public string ShortenColor(IBrush? ibrush)
    {
        if (ibrush is null)
        {
            return "#FFFFFF";
        }
        string str = ibrush.ToString();
        return str.Length == 9 ? str.Replace("#ff", "#").Replace("#FF", "#") : str;
    }

    public ThemeViewModel()
    {
        ((App)Application.Current).InTheme = true;
        ToggleDropdownLoadCommand = ReactiveCommand.Create(() => IsDropdownOpenLoad = !IsDropdownOpenLoad);
        ToggleDropdownBaseCommand = ReactiveCommand.Create(() => IsDropdownOpenBase = !IsDropdownOpenBase);
        ToggleDropdownSaveCommand = ReactiveCommand.Create(() => IsDropdownOpenSave = !IsDropdownOpenSave);
        EditorColor = Colors.White;
        SettingsViewCommand = ReactiveCommand.Create(OpenSettings);
        SaveCommand = ReactiveCommand.Create(SaveTheme);
        SelectBackgroundColorCommand = ReactiveCommand.Create(BackgroundColorSet);
        SelectElementColorCommand = ReactiveCommand.Create(ElementColorSet);
        SelectElementAccentColorCommand = ReactiveCommand.Create(ElementAccentColorSet);
        SelectTextColorCommand = ReactiveCommand.Create(TextColorSet);
        SelectBorderColorCommand = ReactiveCommand.Create(BorderColorSet);
        SelectBackgroundImageCommand = ReactiveCommand.CreateFromTask<string>(PickBackgroundImage);
        ClearImageCommand = ReactiveCommand.Create(ClearImage);
        ClearImage2Command = ReactiveCommand.Create(ClearImage2);
        
        _selectedItemLoad = ItemsLoad[0];
        _selectedItemBase = ItemsBase[0];
        _selectedItemSave = ItemsSave[0];
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(ThemeViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(ThemeViewModel)))
        {
            StartTutorial();
        }
    }
    
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "Здесь можно создать кастомную тему и сохранить её в один из пяти доступных слотов",
            "Для кастомизации доступны 5 цветов, возможно загрузка картинок для фона",
            "Полученная тема будет применена во всей программе и в трее, а также в Настройках её возможно будет экспортировать"
        };
        
        Tutorial.StartTutorial(steps);
    }

    private void BackgroundColorSet()
    {
        BackgroundColor = new SolidColorBrush(EditorColor);
    }
    private void ElementAccentColorSet()
    {
        ElementAccentColor = new SolidColorBrush(EditorColor);
    }
    private void ElementColorSet()
    {
        ElementColor = new SolidColorBrush(EditorColor);
    }
    private void TextColorSet()
    {
        TextColor = new SolidColorBrush(EditorColor);
    }
    private void BorderColorSet()
    {
        BorderColor = new SolidColorBrush(EditorColor);
    }
    private void OpenSettings() {
        ((App)Application.Current).OpenScreen<SettingsView, SettingsViewModel>(intheme: true);
    }
}