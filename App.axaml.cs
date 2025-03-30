using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using funnytext.ViewModels;
using funnytext.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using funnytext.Services;
using ReactiveUI;
#if ANDROID
using Avalonia.Android;
#endif

namespace funnytext;

public partial class App : Application
{
    public bool InMain = true;
    public bool InTheme = false;
    private DateTime _lastClickTime = DateTime.MinValue;
    public static IShareService ShareService { get; set; } = new ShareServiceDefault();
    public static IAppCloser AppCloser { get; set; }
    public static Window? _mainWindow;
    #if ANDROID
    public static AvaloniaMainActivity mactivity;
#endif
    public static UserControl? _mainView;
    public static TrayIcon? _trayIcon;
    public ReactiveCommand<Unit, Unit> ShowWindowCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ExitCommand { get; private set; }
    public ReactiveCommand<int, Unit> RandomCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> SteamCommand { get; private set; }
    public ReactiveCommand<int, Unit> EncryptionCommand { get; private set; }
    public ReactiveCommand<int, Unit> ChangeCommand { get; private set; }
    public ReactiveCommand<int, Unit> ScriptCommand { get; private set; }

    public ThemeVariant GetThemeVariant(string theme = "Light")
    {
        return theme == "Light" ? ThemeVariant.Light :
            theme == "Dark" ? ThemeVariant.Dark :
            theme == "ArabLight" ? CustomThemes.ArabLight :
            theme == "ArabDark" ? CustomThemes.ArabDark :
            theme == "Custom1" ? CustomThemes.Custom1 :
            theme == "Custom2" ? CustomThemes.Custom2 :
            theme == "Custom3" ? CustomThemes.Custom3 :
            theme == "Custom4" ? CustomThemes.Custom4 :
            CustomThemes.Custom5;
    }

    public void RequestPermission(string tempFilePath)
    {
#if ANDROID
        try
        {
            var uri = Android.Net.Uri.Parse(tempFilePath);
            var context = Android.App.Application.Context;
            var contentResolver = context.ContentResolver;
            var activity = funnytext.App.mactivity;
            var flags = Android.Content.ActivityFlags.GrantReadUriPermission
                        | Android.Content.ActivityFlags.GrantWriteUriPermission;
            activity.ContentResolver.TakePersistableUriPermission(uri, flags);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"ошибка: {e}");
        }
#endif
    }

    public IBrush GetImageBrush(string background_path)
    {
        Debug.WriteLine($"ошибка GetImageBrush path: {background_path}");
        if (Current.ApplicationLifetime is ISingleViewApplicationLifetime && !string.IsNullOrEmpty(background_path))
        {
            try
            {
#if ANDROID
                return new ImageBrush 
                {
                    Source = new Bitmap(background_path),
                    Stretch = Stretch.Fill
                };
#endif
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ошибка GetImageBrush android: {e}");
            }
        }
        if (Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime && !string.IsNullOrEmpty(background_path) && !(!background_path.Contains("avares") && !File.Exists(background_path))) {
            try
            {
                return new ImageBrush
                {
                    Source = background_path.Contains("avares")
                        ? new Bitmap(AssetLoader.Open(new Uri(background_path)))
                        : new Bitmap(background_path),
                    Stretch = Stretch.Fill
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"ошибка: {e}");
            }
        }
        return new SolidColorBrush(Color.Parse(CustomThemes.Colors[FileSettingsService.LoadInt("theme")][0]));
    }
    
    public void SetTheme(string theme = "Light", bool main = false)
    {
        CustomThemes.Reload();
        string[] themes =
        [
            "Light", "Dark", "ArabLight", "ArabDark", "Custom1", "Custom2", "Custom3", "Custom4", "Custom5", "Schedule"
        ];
        int themeInt = Array.FindIndex(themes, x => x == theme);
        var resources = Current.Resources;
        resources["BackgroundColorResource"] = Color.Parse(CustomThemes.Colors[themeInt][0]);
        resources["ElementColorResource"] = Color.Parse(CustomThemes.Colors[themeInt][1]);
        resources["TextColorResource"] = Color.Parse(CustomThemes.Colors[themeInt][2]);
        resources["ElementAccentColorResource"] = Color.Parse(CustomThemes.Colors[themeInt][7]);
        resources["BorderColorResource"] = Color.Parse(CustomThemes.Colors[themeInt][8]);
        int time1 = FileSettingsService.LoadInt("schedule_time_1", 7);
        int time2 = FileSettingsService.LoadInt("schedule_time_2", 21);
        int theme1 = FileSettingsService.LoadInt("schedule_theme_1");
        int theme2 = FileSettingsService.LoadInt("schedule_theme_2", 1);
        CustomThemes.Colors[^1] = CustomThemes.Colors[DateTime.Now.Hour >= time1 && DateTime.Now.Hour <= time2 ? theme1 : theme2];
        RequestedThemeVariant = theme != "Schedule" ? GetThemeVariant(theme) : GetThemeVariant(themes[DateTime.Now.Hour >= time1 && DateTime.Now.Hour <= time2 ? theme1 : theme2]);
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && _mainWindow is not null) {
            string background_path = CustomThemes.Colors[FileSettingsService.LoadInt("theme")][main ? 3 : 4];
            _mainWindow.Background = GetImageBrush(background_path);
            _mainWindow.InvalidateVisual();
        } else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform && _mainView is not null)
        {
            ApplyBackground(_mainView);
        }
        RebuildTrayMenu();
    }

    public string FontToAndroid(string savedFont)
    {
        if (((App)Current).ApplicationLifetime is ISingleViewApplicationLifetime)
        {
            savedFont = savedFont == "Courier New" ? "Monospace" : savedFont;
        }
        return savedFont;
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public static async Task<string> GetClipboardTextAsync()
    {
        var clipboard = Clipboard.Get();
        return await clipboard.GetTextAsync();
    }
    public async Task<string> GetClipboardTextAsyncNonStatic()
    {
        var clipboard = Clipboard.Get();
        return await clipboard.GetTextAsync();
    }

    public void SetThemeShort(bool main = false)
    {
        string[] themes =
        [
            "Light", "Dark", "ArabLight", "ArabDark", "Custom1", "Custom2", "Custom3", "Custom4", "Custom5", "Schedule"
        ];
        SetTheme(themes[FileSettingsService.LoadInt("theme")], main);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        new SettingsViewModel().ChangeTheme(FileSettingsService.LoadInt("theme"));
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            AppCloser = new AppCloserDesktop();
            ShowWindowCommand = ReactiveCommand.Create(ShowWindow);
            ExitCommand = ReactiveCommand.Create(CloseApp);
            RandomCommand = ReactiveCommand.Create<int>(MakeRandom);
            SteamCommand = ReactiveCommand.Create(MakeSteam);
            EncryptionCommand = ReactiveCommand.Create<int>(MakeEncryption);
            ChangeCommand = ReactiveCommand.Create<int>(MakeChange);
            ScriptCommand = ReactiveCommand.Create<int>(MakeScript);
            _mainWindow = new MainWindow
            {
                DataContext = new MainViewModel(),
                Title = "Cool Text"
            };
            _mainWindow.KeyDown += OnGlobalKeyDown;
            _trayIcon = new TrayIcon
            {
                ToolTipText = "Cool Text",
                Icon = new WindowIcon(new Bitmap(AssetLoader.Open(new Uri("avares://funnytext/Assets/32logo_v2.ico")))),
                Menu = new NativeMenu()
            };
            _trayIcon.Clicked += OnTrayIconClicked;
            RebuildTrayMenu();

            _mainWindow.Closing += OnMainWindowClosing;

            desktop.MainWindow = _mainWindow;
            _trayIcon.IsVisible = false;
        } else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            _mainView = new MainView
            {
                DataContext = new MainViewModel()
            };
            singleViewPlatform.MainView = _mainView;
        }

        base.OnFrameworkInitializationCompleted();

        SetThemeShort(true);
        if (FileSettingsService.LoadInt("tray") == 1)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                _mainWindow.Close();
            }, Avalonia.Threading.DispatcherPriority.Loaded);
        }
    }
    
    private void OnTrayIconClicked(object? sender, EventArgs e)
    {
        var currentTime = DateTime.Now;
        var elapsedTime = currentTime - _lastClickTime;

        if (elapsedTime.TotalMilliseconds < 500)
        {
            ShowWindow();
        }

        _lastClickTime = currentTime;
    }
    
    public void RebuildTrayMenu()
    {
        if (_trayIcon == null) return;
        _trayIcon.Menu = new NativeMenu();
        var randomSubMenu = new NativeMenu();
        var changeSubMenu = new NativeMenu();
        var encryptionSubMenu = new NativeMenu();
        var scriptsSubMenu = new NativeMenu();
        for (int i = 1; i <= 5; i++)
        {
            randomSubMenu.Items.Add(new NativeMenuItem(FileSettingsService.LoadString($"profile_rand_{i}", $"Профиль {i}")) { Command = RandomCommand, CommandParameter = i});
            changeSubMenu.Items.Add(new NativeMenuItem(FileSettingsService.LoadString($"profile_change_{i}", $"Профиль {i}")) { Command = ChangeCommand, CommandParameter = i});
            encryptionSubMenu.Items.Add(new NativeMenuItem(FileSettingsService.LoadString($"profile_encryption_{i}", $"Профиль {i}")) { Command = EncryptionCommand, CommandParameter = i});
            scriptsSubMenu.Items.Add(new NativeMenuItem(FileSettingsService.LoadString($"script_{i}", $"Скрипт {i}")) { Command = ScriptCommand, CommandParameter = i});
        }
        randomSubMenu.Items.Add(new NativeMenuItemSeparator());
        randomSubMenu.Items.Add(new NativeMenuItem("Ключ") { Command = SteamCommand });
        var randomMenuItem = new NativeMenuItem("Рандом") { Menu = randomSubMenu };
        var changeMenuItem = new NativeMenuItem("Изменение") { Menu = changeSubMenu };
        var encryptionMenuItem = new NativeMenuItem("Шифрование") { Menu = encryptionSubMenu };
        var scriptsMenuItem = new NativeMenuItem("Скрипты") { Menu = scriptsSubMenu };
        _trayIcon.Menu.Items.Add(randomMenuItem);
        _trayIcon.Menu.Items.Add(changeMenuItem);
        _trayIcon.Menu.Items.Add(encryptionMenuItem);
        _trayIcon.Menu.Items.Add(new NativeMenuItemSeparator());
        _trayIcon.Menu.Items.Add(scriptsMenuItem);
        _trayIcon.Menu.Items.Add(new NativeMenuItemSeparator());
        _trayIcon.Menu.Items.Add(new NativeMenuItem("Открыть") { Command = ShowWindowCommand });
        _trayIcon.Menu.Items.Add(new NativeMenuItem("Выход") { Command = ExitCommand });
    }
    
    public void OnGlobalKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            if (_mainWindow?.DataContext is MainViewModel mainViewModel)
            {
                if (InMain)
                {
                    mainViewModel.MinimizeToTray();
                }
                else if (InTheme)
                {
                    OpenSettings();
                } else
                {
                    OpenMain();
                }
            }
        }
    }
    
    public void OpenScreen<TView, TViewModel>(bool inmain = false, bool intheme = false)
        where TView : UserControl, new()
        where TViewModel : ViewModelBase, new()
    {
        if (inmain)
        {
            InMain = false;
        }
        if (intheme)
        {
            InTheme = false;
        }
        var view = new TView();
        var viewModel = new TViewModel();
    
        switch (Current?.ApplicationLifetime)
        {
            case ISingleViewApplicationLifetime android:
            {
                android.MainView = view;
                _mainView = view;
                view.DataContext = viewModel;
                ApplyBackground(view);
                break;
            }
            case IClassicDesktopStyleApplicationLifetime desktop:
            {
                var mainWindow = desktop.MainWindow as MainWindow;
                mainWindow?.ShowView(view);
                view.DataContext = viewModel;
                break;
            }
        }
    }
    
    private void ApplyBackground(UserControl view)
    {
        string background_path = CustomThemes.Colors[FileSettingsService.LoadInt("theme")][InMain ? 3 : 4];
        view.Background = GetImageBrush(background_path);
    }


    private void OpenMain()
    {
        OpenScreen<MainView, MainViewModel>();
    }
    private void OpenSettings()
    {
        OpenScreen<SettingsView, SettingsViewModel>();
    }
    
    public static void OnMainWindowClosing(object? sender, WindowClosingEventArgs e)
    {
        e.Cancel = true;
        _mainWindow.Hide();
        _trayIcon.IsVisible = true;
    }
    
    private void MakeRandom(int i)
    {
        RandomViewModel randomViewModel = new RandomViewModel();
        randomViewModel.LoadProfile2(i);
        randomViewModel.CopyText(randomViewModel.bruh(), 0);
    }
    private void MakeSteam()
    {
        RandomViewModel randomViewModel = new RandomViewModel();
        randomViewModel.CopyText(randomViewModel.GetSteam(), 0);
    }
    private async void MakeChange(int i)
    {
        ChangeViewModel changeViewModel = new ChangeViewModel();
        changeViewModel.LoadProfile2(i);
        changeViewModel.Input = await GetClipboardTextAsync();
        changeViewModel.CopyText(changeViewModel.bruh(), 0);
    }
    private async void MakeEncryption(int i)
    {
        EncryptionViewModel encryptionViewModel = new EncryptionViewModel();
        encryptionViewModel.LoadProfile2(i);
        encryptionViewModel.Input = await GetClipboardTextAsync();
        encryptionViewModel.CopyText(encryptionViewModel.bruh(), 0);
    }
    private async void MakeScript(int i)
    {
        ScriptViewModel scriptViewModel = new ScriptViewModel();
        scriptViewModel.LoadProfile2(i);
        var clipboard = Clipboard.Get();
        await clipboard.SetTextAsync(await scriptViewModel.UseScript());
    }
    
    private void ShowWindow()
    {
        if (Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow != null)
            {
                desktop.MainWindow.Show();
                desktop.MainWindow.WindowState = WindowState.Normal;
                desktop.MainWindow.Activate();
                _trayIcon.IsVisible = false;
            }
        }
    }

    private void CloseApp()
    {
        if (Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            if (_trayIcon != null)
            {
                _mainWindow.Closing -= OnMainWindowClosing;
                _trayIcon.IsVisible = false;
            }

        AppCloser.Close();
    }

    private static void ToggleWindow()
    {
        if (_mainWindow != null)
        {
            if (_mainWindow.IsVisible)
                _mainWindow.Hide();
            else
            {
                _mainWindow.Show();
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.Activate();
            }
        }
    }
}