using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Threading;
using funnytext.Views;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;

namespace funnytext.ViewModels;
using Services;


public class RandomViewModel : ViewModelBase
{
    public string resulttext, strall = "";
    public int ll = 0, global_copied;
    public char[] all= {}, temp= {}, temp_simarr, oil;
    public char[][] alls= { 
        new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' },
        new char[] { '+', '-', '*', '=', '<', '>', '(', ')', '[', ']', '{', '}', '/', '|', '\\', '.', ',', ':', ';', '?', '!', '@', '#', '&', '№', '$', '%', '^', '_', '~', '\'', '\"', '`' },
        new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' },
        new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' },
        new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' },
        new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' },
        new char[] {}
    };
    public int[] coefs;
    private void OpenRandom()
    {
        ((App)Application.Current).OpenScreen<RandomView, RandomViewModel>();
    }
    private void OpenMain() { 
        ((App)Application.Current).OpenScreen<MainView, MainViewModel>();
    }
    

    
    private bool _copied;
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
    
    private string _inputKolvo = "1";
    public string InputKolvo
    {
        get => _inputKolvo;
        set => this.RaiseAndSetIfChanged(ref _inputKolvo, value);
    }
    
    private string _result = "";
    public string result
    {
        get => _result;
        set => this.RaiseAndSetIfChanged(ref _result, value);
    }
    
    private string _alltext = "";
    public string alltext
    {
        get => _alltext;
        set => this.RaiseAndSetIfChanged(ref _alltext, value);
    }
    private string _inputsvoi = "";

    public string inputsvoi
    {
        get => _inputsvoi;
        set => this.RaiseAndSetIfChanged(ref _inputsvoi, value);
    }
    
    private bool _buttonKolvoInteractable;
    public bool ButtonKolvoInteractable
    {
        get => _buttonKolvoInteractable;
        set => this.RaiseAndSetIfChanged(ref _buttonKolvoInteractable, value);
    }
    
    private bool _buttonStartInteractable;
    public bool ButtonStartInteractable
    {
        get => _buttonStartInteractable;
        set => this.RaiseAndSetIfChanged(ref _buttonStartInteractable, value);
    }
    
    private bool _savingIsEnabled;
    public bool SavingIsEnabled
    {
        get => _savingIsEnabled;
        set => this.RaiseAndSetIfChanged(ref _savingIsEnabled, value);
    }
    private ObservableCollection<bool> _toggles = new ObservableCollection<bool>();
    public ObservableCollection<bool> toggles
    {
        get => _toggles;
        set => this.RaiseAndSetIfChanged(ref _toggles, value);
    }
    private ObservableCollection<bool> _buttoncoef = new ObservableCollection<bool>();

    public ObservableCollection<bool> buttoncoef
    {
        get => _buttoncoef;
        set => this.RaiseAndSetIfChanged(ref _buttoncoef, value);
    }

    private string[] _coefstext = new string[7];

    public string[] coefstext
    {
        get => _coefstext;
        set => this.RaiseAndSetIfChanged(ref _coefstext, value);
    }

    private string[] _countnumbers = new string[7];

    public string[] countnumbers
    {
        get => _countnumbers;
        set => this.RaiseAndSetIfChanged(ref _countnumbers, value);
    }


    private bool _similar;

    public bool similar
    {
        get => _similar;
        set => this.RaiseAndSetIfChanged(ref _similar, value);
    }

    private bool _unique;

    public bool unique
    {
        get => _unique;
        set => this.RaiseAndSetIfChanged(ref _unique, value);
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
    
    private void OnProfileSelected(string? profile)
    {
        if (profile != null)
        {
            SavingIsEnabled = true;
            if (IsSaving) {
                SavingIsEnabled = ButtonStartInteractable;
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

    public ReactiveCommand<Unit, Unit> ShowPresetMenuCommand { get; }
    public ReactiveCommand<Unit, Unit> SavePresetCommand { get; }
    public ReactiveCommand<Unit, Unit> LoadPresetCommand { get; }
    public ReactiveCommand<Unit, Unit> ConfirmPresetCommand { get; }
    public ReactiveCommand<int, Unit> ChangeZnachCommand { get; }
    public ReactiveCommand<Unit, Unit> MakeCommand { get; }
    public ReactiveCommand<Unit, Unit> RandomViewCommand { get; }
    public ReactiveCommand<Unit, Unit> MainViewCommand { get; }
    public ReactiveCommand<Unit, Unit> RandomSettingsCommand { get; }
    public ReactiveCommand<int, Unit> ChangeZnachCoefCommand { get; }
    public ReactiveCommand<Unit, Unit> SteamCommand { get; }
    public ReactiveCommand<Unit, Unit> ShareCommand { get; }
    private void UpdateArray<T>(string propertyName, int index, T newValue)
    {
        var property = GetType().GetProperty(propertyName);
        if (property == null)
        {
            return;
        }
        var array = (T[])property.GetValue(this);
        if (index < 0 || index >= array.Length)
        {
            return;
        }
        var newArray = array.ToArray();
        newArray[index] = newValue;
        property.SetValue(this, newArray);
        this.RaisePropertyChanged(propertyName);
    }
    
    public RandomViewModel()
    {
        ((App)Application.Current).SetThemeShort();
        MakeCommand = ReactiveCommand.Create(make);
        RandomSettingsCommand = ReactiveCommand.Create(RandomSettings);
        ChangeZnachCommand = ReactiveCommand.Create<int>(changeznach);
        ChangeZnachCoefCommand = ReactiveCommand.Create<int>(changeznachcoef);
        RandomViewCommand = ReactiveCommand.Create(OpenRandom);
        MainViewCommand = ReactiveCommand.Create(OpenMain);
        SteamCommand = ReactiveCommand.Create(Steam);
        ShareCommand = ReactiveCommand.Create(() => App.ShareService.ShareText(result));
        ShowPresetMenuCommand = ReactiveCommand.Create(ShowMenu);
        SavePresetCommand = ReactiveCommand.Create(StartSaving);
        LoadPresetCommand = ReactiveCommand.Create(StartLoading);
        ConfirmPresetCommand = ReactiveCommand.Create(ConfirmAction);
        coefs = new int[coefstext.Length];
        for (int i = 0; i < coefstext.Length; i++)
        {
            UpdateArray("coefstext", i, "1");
            coefs[i] = Int32.Parse(coefstext[i]);
        }
        for (int i = 0; i < 7; i++)
        {
            _toggles.Add(false);
            _buttoncoef.Add(false);
        }

        for (int i = 1; i <= 5; i++)
        {
            Profiles.Add(FileSettingsService.LoadString($"profile_rand_{i}", $"Профиль {i}"));
        }

        SetCountNumbers();
        coefchecker();
        if (FileSettingsService.LoadInt("start") != 0)
        {
            LoadProfile2(FileSettingsService.LoadInt("start"));
        }
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(RandomViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(RandomViewModel)))
        {
            StartTutorial();
        }
    }
    
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "Раздел создания случайного текста. Будет создан текст длиной в указанное количество символов из указанного набора символов",
            "Также вы можете сохранить / загрузить профиль настроек, а позже использовать его в трее или в скриптах"
        };
        
        Tutorial.StartTutorial(steps);
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
            SavingIsEnabled = ButtonStartInteractable && !string.IsNullOrEmpty(SelectedProfile);
        }

        private void StartLoading()
        {
            IsProfileSelectionVisible = true;
            IsSaving = false;
            IsConfirmVisible = true;
            SavingIsEnabled = !string.IsNullOrEmpty(SelectedProfile);
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
                    FileSettingsService.Save($"profile_rand_{strtoint}", $"{PresetName}_{strtoint}");
                    FileSettingsService.Save($"profile_rand_{strtoint}_inputkolvo", InputKolvo);
                    FileSettingsService.Save($"profile_rand_{strtoint}_inputsvoi", inputsvoi);
                    for (int i = 0; i < 7; i++)
                    {
                        FileSettingsService.Save($"profile_rand_{strtoint}_toggles{i}", toggles[i]);
                        FileSettingsService.Save($"profile_rand_{strtoint}_coefstext{i}", coefstext[i]);
                    }
                    FileSettingsService.Save($"profile_rand_{strtoint}_similar", similar);
                    FileSettingsService.Save($"profile_rand_{strtoint}_unique", unique);
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
            InputKolvo = FileSettingsService.LoadString($"profile_rand_{strtoint}_inputkolvo", "1");
            inputsvoi = FileSettingsService.LoadString($"profile_rand_{strtoint}_inputsvoi", inputsvoi);
            for (int i = 0; i < 7; i++)
            {
                _toggles[i] = FileSettingsService.LoadBool($"profile_rand_{strtoint}_toggles{i}", toggles[i]);
                UpdateArray("coefstext", i, FileSettingsService.LoadString($"profile_rand_{strtoint}_coefstext{i}", coefstext[i]));
            }
            similar = FileSettingsService.LoadBool($"profile_rand_{strtoint}_similar", similar);
            unique = FileSettingsService.LoadBool($"profile_rand_{strtoint}_unique", unique);
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

    void copied_off(int local_copied)
    {
        if (local_copied == global_copied)
        {
            copied = false;
        }
    }

    public void SetCountNumbers() {
        for (int i = 0; i < alls.Length; i++) {
            UpdateArray("countnumbers", i, (alls[i].Length * Convert.ToInt32(coefstext[i])).ToString());
        }
    }

    public string GetSteam()
    {
        all = new char[]{};
        string b = "";
        for (int n = 0; n < alls[0].Length; n++) {
            all = all.Append(alls[0][n]).ToArray();
        }
        for (int n = 0; n < alls[3].Length; n++) {
            all = all.Append(alls[3][n]).ToArray();
        }
        for (int i = 0; i < 15; i++) {
            var rrange = new Random().Next(0, all.Length);
            b += all[rrange];
            if ((i + 1) % 5 == 0 && i != 14)
            {
                b += '-';
            }
        }

        return b;
    }

    public void Steam()
    {
        string b = GetSteam();
        result = b;
        CopyText(b, 3);
        sharebtn = true;
        FileSettingsService.Save("changedtextrand", FileSettingsService.LoadInt("changedtextrand") + 1);
    }

    public int StrToInt(string str)
    {
        return Int32.Parse(str.Last().ToString());
    }

    public void OnChangeSomething()
    {
        oil = new char[]{};
        for (int i = 0; i < coefstext.Length; i++) {
            UpdateArray("coefstext", i, Regex.Replace(coefstext[i], "[^0-9]", ""));
            if (coefstext[i].Length > 4)
            {
                UpdateArray("coefstext", i, coefstext[i][..4]);
            }
            if (string.IsNullOrEmpty(coefstext[i]) || coefstext[i] == "0")
            {
                UpdateArray("coefstext", i, "1");
            }
            coefs[i] = Int32.Parse(coefstext[i]);
        }
        strall = "";
        alls[6] = inputsvoi.ToCharArray();
        for (int i = 0; i < alls.Length; i++) {
            for (int k = 0; k < coefs[i]; k++) {
                for (int j = 0; j < alls[i].Length; j++) {
                    if (toggles[i]) {
                        oil = oil.Append(alls[i][j]).ToArray();
                    }
                }
            }
        }

        oil = return_similar(oil);

        for (int i = 0; i < oil.Length; i++) {
            strall += oil[i];
        }

        alltext = strall.Length < 500 ? strall : strall[..499] + "...";

        coefchecker();
        if ((FileSettingsService.LoadInt("make") == 1) && (ButtonStartInteractable)) {
            if ((FileSettingsService.LoadInt("music") == 1) && ((FileSettingsService.LoadInt("theme") == 2) || (FileSettingsService.LoadInt("theme") == 3))) {
                AudioPlayer.PlayMp3Async("avares://funnytext/Assets/bruh.mp3");
            }
            resulttext = bruh();
            if (resulttext != result) {
                result = resulttext;
                if (resulttext != "") {
                    CopyText(resulttext, 3);
                    sharebtn = true;
                    FileSettingsService.Save("changedtextrand", FileSettingsService.LoadInt("changedtextrand") + 1);
                }
            }
        }

        SetCountNumbers();
        
        ButtonStartInteractable = !((unique && (Int32.Parse(InputKolvo) > oil.Length)) ||
                                    (!toggles[0] && !toggles[1] && !toggles[2] && !toggles[3] &&
                                     !toggles[4] && !toggles[5] && !toggles[6]) ||
                                    ((toggles[6]) && (inputsvoi == "")) ||
                                    (Int32.Parse(InputKolvo) <= 0) || (InputKolvo == "") ||
                                    (Int32.Parse(coefstext[0]) <= 0) || (Int32.Parse(coefstext[1]) <= 0) ||
                                    (Int32.Parse(coefstext[2]) <= 0) || (Int32.Parse(coefstext[3]) <= 0) ||
                                    (Int32.Parse(coefstext[4]) <= 0) || (Int32.Parse(coefstext[5]) <= 0) ||
                                    (Int32.Parse(coefstext[6]) <= 0) || (coefstext[0] == "") ||
                                    (coefstext[1] == "") || (coefstext[2] == "") ||
                                    (coefstext[3] == "") || (coefstext[4] == "") ||
                                    (coefstext[5] == "") || (coefstext[6] == ""));
        if (IsSaving) {
            SavingIsEnabled = ButtonStartInteractable;
        }
        SavingIsEnabled = SavingIsEnabled && !string.IsNullOrEmpty(SelectedProfile);
    }

    (char[], int) similar_couple(char[] simarr_s, char[] temp_simarr_s, int ll_s, char l1, char l2) {
        if ((Array.IndexOf(simarr_s, l1) > -1) && (Array.IndexOf(simarr_s, l2) > -1)) {
            for (int i = 0; i < simarr_s.Length; i++) {
                if ((simarr_s[i] != l1) && (simarr_s[i] != l2)) {
                    temp_simarr_s = temp_simarr_s.Append(simarr_s[i]).ToArray();
                } else if (simarr_s[i] == l1) {
                    temp_simarr_s = temp_simarr_s.Append(l1).ToArray();
                } else {
                    ll_s--;
                }
            }
        } else {
            for (int i = 0; i < simarr_s.Length; i++) {
                temp_simarr_s = temp_simarr_s.Append(simarr_s[i]).ToArray();
            }
        }
        return (temp_simarr_s, ll_s);
    }

    public char[] return_similar(char[] simarr) {
        temp_simarr = new char[]{};
        if (!similar) {
            return simarr;
        }
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, '0', 'O');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, '0', 'О');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'l', 'I');

        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'a', 'а');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'c', 'с');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'e', 'е');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'o', 'о');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'p', 'р');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'x', 'х');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'y', 'у');

        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'A', 'А');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'B', 'В');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'C', 'С');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'E', 'Е');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'H', 'Н');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'K', 'К');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'M', 'М');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'P', 'Р');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'T', 'Т');
        (simarr, ll) = similar_couple(simarr, temp_simarr, ll, 'X', 'Х');
        return simarr;
    }

    public void loadprofile() {
        for (int i = 0; i < 7; i++)
        {
            _toggles[i] = FileSettingsService.LoadInt($"toggle{i}R") == 1;
            UpdateArray("coefstext", i, FileSettingsService.LoadString($"pole{i + 3}R", "1"));
        }
        InputKolvo = FileSettingsService.LoadString("pole1R", "1");
        inputsvoi = FileSettingsService.LoadString("pole2R");
        similar = FileSettingsService.LoadInt("toggle7R") == 1;
        coefchecker();
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

    public void RandomSettingsOne()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            _toggles[i] = new Random().Next(0, 2) == 1;
        }
        for (int i = 0; i < coefstext.Length; i++)
        {
            UpdateArray("coefstext", i, new Random().Next(1, 4).ToString());
        }
        similar = new Random().Next(0, 2) == 1;
        unique = new Random().Next(0, 2) == 1;
        InputKolvo = new Random().Next(1, 11).ToString();
        inputsvoi = GenerateRandom(new Random().Next(1, 11));
        result = "";
        copied = false;
        OnChangeSomething();
    }

    public void RandomSettings()
    {
        RandomSettingsOne();
        while (!ButtonStartInteractable)
        {
            RandomSettingsOne();
        }
    }

    public void make() {
        if ((FileSettingsService.LoadInt("music") == 1) && ((FileSettingsService.LoadInt("theme") == 2) || (FileSettingsService.LoadInt("theme") == 3))) {
            AudioPlayer.PlayMp3Async("avares://funnytext/Assets/bruh.mp3");
        }
        resulttext = bruh();
        result = resulttext;
        if (resulttext != "") {
            CopyText(resulttext, 3);
            sharebtn = true;
            FileSettingsService.Save("changedtextrand", FileSettingsService.LoadInt("changedtextrand") + 1);
        }
    }

    public string bruh() {
        int kolvo = Int32.Parse(InputKolvo);
        all = new char[]{};
        ll = 0;
        alls[6] = inputsvoi.ToCharArray();
        for (int i = 0; i < coefstext.Length; i++) {
            coefs[i] = Int32.Parse(coefstext[i]);
        }
        string b = "";
        for (int i = 0; i < alls.Length; i++) {
            if (toggles[i]) {
                for (int j = 0; j < coefs[i]; j++) {
                    for (int n = 0; n < alls[i].Length; n++) {
                        all = all.Append(alls[i][n]).ToArray();
                    }
                    ll += alls[i].Length;
                }
            }
        }

        temp = return_similar(all);
        
        /* Debug.Log(ll); */
        
        if (ll > 0) {
            for (int i = 0; i < kolvo; i++) {
                var rrange = new Random().Next(0, ll);
                b += temp[rrange];
                if (unique) {
                    string str = new string(temp);
                    str = str.Remove(rrange, 1);
                    temp = str.ToCharArray();
                    ll -= 1;
                }
            }
        }
        return b;
    }

    public void changeznach(int z) {
        var zz = FileSettingsService.LoadInt("plusminus", 1);
        if (z < 0) {
            zz = -zz;
        }
        int q = (Int32.Parse(InputKolvo) + zz);
        if (q <= 0) {q = 1;}
        InputKolvo = q.ToString();
        ButtonKolvoInteractable = q > 1;
        OnChangeSomething();
    }

    public void changeznachcoef(int z)
    {
        int plumi = FileSettingsService.LoadInt("plusminus", 1);
        if (z < 0) {plumi = -plumi; z *= -1;}
        int q = Int32.Parse(coefstext[z-1]) + plumi;
        if (q <= 0) {q = 1;}
        UpdateArray("coefstext", z - 1, q.ToString());
        _buttoncoef[z - 1] = q > 1;
        SetCountNumbers();
        OnChangeSomething();
    }

    public void coefchecker() {
        if ((InputKolvo != "") && (InputKolvo != "-"))
        {
            ButtonKolvoInteractable = Int32.Parse(InputKolvo) > 1;
        }
        
        for (int i = 0; i < 7; i++) {
            if ((coefstext[i] != "") && (coefstext[i] != "-")) {
                _buttoncoef[i] = Int32.Parse(coefstext[i]) > 1;
            }
        }
    }
}