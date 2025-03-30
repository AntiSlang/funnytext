using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Avalonia;
using Avalonia.Threading;
using funnytext.Views;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.TextInput;
using Avalonia.Layout;
using DynamicData;
using funnytext.Models;

namespace funnytext.ViewModels;
using Services;


public class EncryptionViewModel : ViewModelBase
{
    public string resulttext;
    public int global_copied;
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
    private bool _leftEncryption;
    public bool LeftEncryption
    {
        get => _leftEncryption;
        set => this.RaiseAndSetIfChanged(ref _leftEncryption, value);
    }
    private bool _leftDecryption;
    public bool LeftDecryption
    {
        get => _leftDecryption;
        set => this.RaiseAndSetIfChanged(ref _leftDecryption, value);
    }
    private bool _rightASCII;
    public bool RightASCII
    {
        get => _rightASCII;
        set => this.RaiseAndSetIfChanged(ref _rightASCII, value);
    }
    private bool _rightUnicode;
    public bool RightUnicode
    {
        get => _rightUnicode;
        set => this.RaiseAndSetIfChanged(ref _rightUnicode, value);
    }
    private bool _rightAlphabet;
    public bool RightAlphabet
    {
        get => _rightAlphabet;
        set => this.RaiseAndSetIfChanged(ref _rightAlphabet, value);
    }
    private bool _rightCaesar;
    public bool RightCaesar
    {
        get => _rightCaesar;
        set => this.RaiseAndSetIfChanged(ref _rightCaesar, value);
    }
    private bool _rightMorse;
    public bool RightMorse
    {
        get => _rightMorse;
        set => this.RaiseAndSetIfChanged(ref _rightMorse, value);
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
    private bool _isDropdownMorse;
    public bool IsDropdownMorse
    {
        get => _isDropdownMorse;
        set => this.RaiseAndSetIfChanged(ref _isDropdownMorse, value);
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
            SavingIsEnabled = !string.IsNullOrEmpty(SelectedProfile);
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
    
    private bool _buttonKolvoInteractable;
    public bool ButtonKolvoInteractable
    {
        get => _buttonKolvoInteractable;
        set => this.RaiseAndSetIfChanged(ref _buttonKolvoInteractable, value);
    }
    
    private bool _buttonKolvo2Interactable = true;
    public bool ButtonKolvo2Interactable
    {
        get => _buttonKolvo2Interactable;
        set => this.RaiseAndSetIfChanged(ref _buttonKolvo2Interactable, value);
    }

    private string _inputKolvo = "1";
    public string InputKolvo
    {
        get => _inputKolvo;
        set => this.RaiseAndSetIfChanged(ref _inputKolvo, value);
    }
    public ObservableCollection<EmptyItemModel> ItemsMorse { get; set; } = new()
    {
        new EmptyItemModel("Морзе -> ru -> en", 0),
        new EmptyItemModel("Морзе -> en -> ru", 1)
    };
    private EmptyItemModel _selectedItemMorse;
    public EmptyItemModel SelectedItemMorse
    {
        get => _selectedItemMorse;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItemMorse, value);
        }
    }
    public ReactiveCommand<Unit, Unit> ShowPresetMenuCommand { get; }
    public ReactiveCommand<Unit, Unit> SavePresetCommand { get; }
    public ReactiveCommand<Unit, Unit> LoadPresetCommand { get; }
    public ReactiveCommand<Unit, Unit> ConfirmPresetCommand { get; }
    public ReactiveCommand<Unit, Unit> MakeCommand { get; }
    public ReactiveCommand<Unit, Unit> EncryptionViewCommand { get; }
    public ReactiveCommand<Unit, Unit> MainViewCommand { get; }
    public ReactiveCommand<Unit, Unit> RandomSettingsCommand { get; }
    public ReactiveCommand<Unit, Unit> ShareCommand { get; }
    public ReactiveCommand<int, Unit> ChangeCaesarCoefCommand { get; }
    public EncryptionViewModel()
    {
        ((App)Application.Current).SetThemeShort();
        MakeCommand = ReactiveCommand.Create(make);
        RandomSettingsCommand = ReactiveCommand.Create(RandomSettings);
        EncryptionViewCommand = ReactiveCommand.Create(OpenEncryption);
        MainViewCommand = ReactiveCommand.Create(OpenMain);
        ShareCommand = ReactiveCommand.Create(() => App.ShareService.ShareText(result));
        ShowPresetMenuCommand = ReactiveCommand.Create(ShowMenu);
        SavePresetCommand = ReactiveCommand.Create(StartSaving);
        LoadPresetCommand = ReactiveCommand.Create(StartLoading);
        ConfirmPresetCommand = ReactiveCommand.Create(ConfirmAction);
        ChangeCaesarCoefCommand = ReactiveCommand.Create<int>(changeznach);
        LeftEncryption = true;
        RightASCII = true;
        foreach (var pair in MorseCodeRuFirst)
        {
            ReverseMorseCodeRuFirst[pair.Value] = pair.Key;
        }
        foreach (var pair in MorseCodeEnFirst)
        {
            ReverseMorseCodeEnFirst[pair.Value] = pair.Key;
        }

        for (int i = 1; i <= 5; i++)
        {
            Profiles.Add(FileSettingsService.LoadString($"profile_encryption_{i}", $"Профиль {i}"));
        }
        if (FileSettingsService.LoadInt("start") != 0)
        {
            LoadProfile2(FileSettingsService.LoadInt("start"));
        }
        _selectedItemMorse = ItemsMorse[0];
        Tutorial = new TutorialViewModel();
        TutorialService.CurrentTutorialName = nameof(EncryptionViewModel);
        if (TutorialService.ShouldShowTutorial(nameof(EncryptionViewModel)))
        {
            StartTutorial();
        }
    }
    
    public TutorialViewModel Tutorial { get; }
    
    private void StartTutorial()
    {
        var steps = new List<string>
        {
            "Раздел шифрования текста. Текст из поля ввода будет зашифрован или расшифрован одним из представленных шифров",
            "Также вы можете сохранить / загрузить профиль настроек, а позже использовать его в трее или в скриптах"
        };
        
        Tutorial.StartTutorial(steps);
    }
    public void OnChangeSomething()
    {
        IsDropdownMorse = LeftDecryption && RightMorse;
        KolvoUpdate();
        ButtonStartInteractable = Input != "";
        SavingIsEnabled = !string.IsNullOrEmpty(SelectedProfile);
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
                    FileSettingsService.Save("changedtextencryption", FileSettingsService.LoadInt("changedtextencryption") + 1);
                }
            }
        }
    }
    public void changeznach(int z) {
        var zz = FileSettingsService.LoadInt("plusminus", 1);
        if (z < 0) {
            zz = -zz;
        }
        int q = (Int32.Parse(InputKolvo) + zz);
        if (q <= 0) {q = 1;}
        if (q >= 33) {q = 32;}
        InputKolvo = q.ToString();
        OnChangeSomething();
    }

    public void KolvoUpdate()
    {
        ButtonKolvoInteractable = Int32.Parse(InputKolvo) > 1;
        ButtonKolvo2Interactable = Int32.Parse(InputKolvo) < 32;
    }
    public async void make() {
        if ((FileSettingsService.LoadInt("music") == 1) && ((FileSettingsService.LoadInt("theme") == 2) || (FileSettingsService.LoadInt("theme") == 3))) {
            AudioPlayer.PlayMp3Async("avares://funnytext/Assets/bruh.mp3");
        }
        resulttext = bruh();
        result = resulttext;
        if (resulttext != "") {
            CopyText(resulttext, 3);
            sharebtn = true;
            FileSettingsService.Save("changedtextencryption", FileSettingsService.LoadInt("changedtextencryption") + 1);
        }
    }
    public string bruh()
    {
        string res = Input;
        if (string.IsNullOrEmpty(res))
        {
            return "";
        }
        if (RightCaesar)
        {
            res = CaesarCipher(res, LeftEncryption, Int32.Parse(InputKolvo));
        } else if (RightAlphabet)
        {
            res = AlphabetTransform(res, LeftEncryption);
        } else if (RightMorse)
        {
            res = MorseCodeTransform(res, LeftEncryption, en_to_ru: _selectedItemMorse.Number == 0);
        } else if (RightASCII)
        {
            res = AsciiReplace(res, LeftEncryption);
        } else if (RightUnicode)
        {
            res = UnicodeTransform(res, LeftEncryption);
        }

        return res.Trim();
    }
    private void OpenEncryption()
    {
        ((App)Application.Current).OpenScreen<EncryptionView, EncryptionViewModel>();
    }
    private void OpenMain() { 
        ((App)Application.Current).OpenScreen<MainView, MainViewModel>();
    }

    public void RandomSettings()
    {
        if (new Random().Next(0, 2) == 1)
        {
            LeftEncryption = true;
        }
        else
        {
            LeftDecryption = true;
        }
        int random = new Random().Next(0, 5);
        if (random == 0)
        {
            RightCaesar = true;
        } else if (random == 1)
        {
            RightAlphabet = true;
        } else if (random == 2)
        {
            RightMorse = true;
        } else if (random == 3)
        {
            RightASCII = true;
        } else if (random == 4)
        {
            RightUnicode = true;
        }
        OnChangeSomething();
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
            SavingIsEnabled = !string.IsNullOrEmpty(SelectedProfile);
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
                    FileSettingsService.Save($"profile_encryption_{strtoint}", $"{PresetName}_{strtoint}");
                    int one, two;
                    if (LeftEncryption)
                    {
                        one = 0;
                    }
                    else
                    {
                        one = 1;
                    }
                    if (RightASCII)
                    {
                        two = 0;
                    } else if (RightUnicode)
                    {
                        two = 1;
                    } else if (RightAlphabet)
                    {
                        two = 2;
                    } else if (RightCaesar)
                    {
                        two = 3;
                    } else
                    {
                        two = 4;
                    }
                    FileSettingsService.Save($"profile_encryption_{strtoint}_one", one);
                    FileSettingsService.Save($"profile_encryption_{strtoint}_two", two);
                    FileSettingsService.Save($"profile_encryption_{strtoint}_input", InputKolvo);
                    FileSettingsService.Save($"profile_encryption_{strtoint}_morse", _selectedItemMorse.Number);
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
            int one = FileSettingsService.LoadInt($"profile_encryption_{strtoint}_one");
            int two = FileSettingsService.LoadInt($"profile_encryption_{strtoint}_two");
            InputKolvo = FileSettingsService.LoadString($"profile_encryption_{strtoint}_input", "1");
            LeftEncryption = one == 0;
            LeftDecryption = one == 1;
            RightASCII = two == 0;
            RightUnicode = two == 1;
            RightAlphabet = two == 2;
            RightCaesar = two == 3;
            RightMorse = two == 4;
            SelectedItemMorse = ItemsMorse[FileSettingsService.LoadInt($"profile_encryption_{strtoint}_morse")];
            OnChangeSomething();
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
    
    public static string AlphabetTransform(string input, bool encode)
    {
        if (encode)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    int number = GetLetterNumber(c);
                    if (number != -1)
                    {
                        if (char.IsUpper(c))
                            result.Append($"|{number}-]");
                        else
                            result.Append($"|{number}_]");
                    }
                    else
                    {
                        result.Append(c);
                    }
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
        else
        {
            StringBuilder result = new StringBuilder();
            int i = 0;
            while (i < input.Length)
            {
                if (input[i] == '|' && i + 3 < input.Length)
                {
                    int endIndex = input.IndexOf(']', i);
                    if (endIndex != -1)
                    {
                        string code = input.Substring(i, endIndex - i + 1);
                        if (code.Length >= 4 && (code[code.Length - 2] == '-' || code[code.Length - 2] == '_'))
                        {
                            string numberStr = code.Substring(1, code.Length - 3);
                            if (int.TryParse(numberStr, out int number))
                            {
                                char decodedChar = GetNumberLetter(number, code[code.Length - 2] == '-');
                                if (decodedChar != '\0')
                                {
                                    result.Append(decodedChar);
                                    i = endIndex + 1;
                                    continue;
                                }
                            }
                        }
                    }
                }
                result.Append(input[i]);
                i++;
            }
            return result.ToString();
        }
    }

    private static int GetLetterNumber(char c)
    {
        if (c >= 'A' && c <= 'Z') return c - 'A' + 1;
        if (c >= 'a' && c <= 'z') return c - 'a' + 1;
        if (c >= 'А' && c <= 'Я') return c - 'А' + 27;
        if (c >= 'а' && c <= 'я') return c - 'а' + 27;
        if (c == 'Ё') return 33;
        if (c == 'ё') return 33;
        return -1;
    }

    private static char GetNumberLetter(int number, bool isUpper)
    {
        if (number >= 1 && number <= 26)
        {
            return isUpper ? (char)('A' + number - 1) : (char)('a' + number - 1);
        }
        if (number >= 27 && number <= 59)
        {
            if (number == 33) return isUpper ? 'Ё' : 'ё';
            return isUpper ? (char)('А' + number - 27) : (char)('а' + number - 27);
        }
        return '\0';
    }
    
    public string CaesarCipher(string input, bool encode, int shift)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                char[][] alphabet = { 
                    new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' },
                    new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' },
                    new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' },
                    new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' }
                };
                char[] array = alphabet[((alphabet[2].Contains(c) || alphabet[3].Contains(c)) ? 2 : 0) + (char.IsUpper(c) ? 1 : 0)];
                result.Append(array[(array.Length * 2 + array.IndexOf(c) + (encode ? shift : -shift)) % array.Length]);
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
    private static readonly Dictionary<char, string> MorseCodeRuFirst = new Dictionary<char, string>()
    {
        {'А', ".-"}, {'Б', "-..."}, {'В', ".--"}, {'Г', "--."}, {'Д', "-.."}, {'Е', "."},
        {'Ж', "...-"}, {'З', "--.."}, {'И', ".."}, {'Й', ".---"}, {'К', "-.-"}, {'Л', ".-.."},
        {'М', "--"}, {'Н', "-."}, {'О', "---"}, {'П', ".--."}, {'Р', ".-."}, {'С', "..."},
        {'Т', "-"}, {'У', "..-"}, {'Ф', "..-."}, {'Х', "...."}, {'Ц', "-.-."}, {'Ч', "---."},
        {'Ш', "----"}, {'Щ', "--.-"}, {'Ъ', "--.--"}, {'Ы', "-.--"}, {'Ь', "-..-"}, {'Э', "..-.."},
        {'Ю', "..--"}, {'Я', ".-.-"},
        
        {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."}, {'F', "..-."},
        {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"}, {'K', "-.-"}, {'L', ".-.."},
        {'M', "--"}, {'N', "-."}, {'O', "---"}, {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."},
        {'S', "..."}, {'T', "-"}, {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"},
        {'Y', "-.--"}, {'Z', "--.."},

        {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"},
        {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."},

        {' ', "/"}, {'.', ".-.-.-"}, {',', "--..--"}, {'?', "..--.."}, {'!', "-.-.--"}
    };
    
    private static readonly Dictionary<char, string> MorseCodeEnFirst = new Dictionary<char, string>()
    {
        {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."}, {'F', "..-."},
        {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"}, {'K', "-.-"}, {'L', ".-.."},
        {'M', "--"}, {'N', "-."}, {'O', "---"}, {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."},
        {'S', "..."}, {'T', "-"}, {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"},
        {'Y', "-.--"}, {'Z', "--.."},

        {'А', ".-"}, {'Б', "-..."}, {'В', ".--"}, {'Г', "--."}, {'Д', "-.."}, {'Е', "."},
        {'Ж', "...-"}, {'З', "--.."}, {'И', ".."}, {'Й', ".---"}, {'К', "-.-"}, {'Л', ".-.."},
        {'М', "--"}, {'Н', "-."}, {'О', "---"}, {'П', ".--."}, {'Р', ".-."}, {'С', "..."},
        {'Т', "-"}, {'У', "..-"}, {'Ф', "..-."}, {'Х', "...."}, {'Ц', "-.-."}, {'Ч', "---."},
        {'Ш', "----"}, {'Щ', "--.-"}, {'Ъ', "--.--"}, {'Ы', "-.--"}, {'Ь', "-..-"}, {'Э', "..-.."},
        {'Ю', "..--"}, {'Я', ".-.-"},

        {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"},
        {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."},

        {' ', "/"}, {'.', ".-.-.-"}, {',', "--..--"}, {'?', "..--.."}, {'!', "-.-.--"}
    };

    private static readonly Dictionary<string, char> ReverseMorseCodeRuFirst = new Dictionary<string, char>();
    private static readonly Dictionary<string, char> ReverseMorseCodeEnFirst = new Dictionary<string, char>();


    public string MorseCodeTransform(string input, bool encode, bool en_to_ru)
    {
        StringBuilder result = new StringBuilder();

        if (encode)
        {
            foreach (char c in input.ToUpper())
            {
                if (MorseCodeRuFirst.ContainsKey(c))
                {
                    result.Append(MorseCodeRuFirst[c]).Append(' ');
                }
            }
        }
        else
        {
            string[] codes = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var reverseMorseCode = en_to_ru ? ReverseMorseCodeEnFirst : ReverseMorseCodeRuFirst;
            foreach (string code in codes)
            {
                if (reverseMorseCode.ContainsKey(code))
                {
                    result.Append(reverseMorseCode[code]);
                }
            }
        }

        return result.ToString();
    }
    
    public static string AsciiReplace(string input, bool encode)
    {
        StringBuilder result = new StringBuilder();

        if (encode)
        {
            foreach (char c in input)
            {
                result.Append((int)c).Append(' ');
            }
        }
        else
        {
            string[] codes = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string code in codes)
            {
                if (int.TryParse(code, out int asciiCode))
                {
                    result.Append((char)asciiCode);
                }
            }
        }

        return result.ToString();
    }
    
    public string UnicodeTransform(string input, bool encode)
    {
        if (encode)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in input)
            {
                result.AppendFormat("\\u{0:X4}", (int)c);
            }
            return result.ToString();
        }
        else
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\' && i + 1 < input.Length && input[i + 1] == 'u')
                {
                    if (i + 5 < input.Length)
                    {
                        string hexCode = input.Substring(i + 2, 4);
                        try
                        {
                            int unicodeValue = Convert.ToInt32(hexCode, 16);
                            result.Append((char)unicodeValue);
                            i += 5;
                        }
                        catch
                        {
                            result.Append("\\u" + hexCode);
                            i += 5;
                        }
                    }
                    else
                    {
                        result.Append(input[i]);
                    }
                }
                else
                {
                    result.Append(input[i]);
                }
            }
            return result.ToString();
        }
    }
}