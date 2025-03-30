using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Platform.Storage;


namespace funnytext.Services;

public static class FileSettingsService
{
    private static readonly string SettingsFilePath;

    static FileSettingsService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        SettingsFilePath = Path.Combine(appDataPath, "settings.json");
    }
    
    public static async Task ExportToFolder(bool theme = false)
    {
        if (Application.Current.ApplicationLifetime is ISingleViewApplicationLifetime android)
        {
            try
            {
                var filePicker = await TopLevel.GetTopLevel(android.MainView).StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = theme ? "themes.json" : "profiles.json",
                    SuggestedFileName = theme ? "themes" : "profiles",
                    DefaultExtension = "json"
                });
                if (filePicker == null)
                {
                    return;
                }
                using var writeStream = await filePicker.OpenWriteAsync();
                await JsonSerializer.SerializeAsync(writeStream, GetExportFile(theme));
                await writeStream.FlushAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ошибка: {e}");
            }
        }
        else
        {
            try
            {
                Window parent = App._mainWindow;
                string fileName = theme ? "themes.json" : "profiles.json";
                var folderPicker = await parent.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
                {
                    Title = fileName,
                    AllowMultiple = false
                });
                if (folderPicker.Count == 0) return;
                string folderPath = folderPicker[0].Path.LocalPath;
                string filePath = Path.Combine(folderPath, fileName);
                Dictionary<string, string> exportFile = GetExportFile(theme);
                await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(exportFile));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ошибка: {e}");
            }
        }
    }

    private static Dictionary<string, string> GetExportFile(bool theme)
    {
        var settings = LoadAllSettings();
        Dictionary<string, string> exportFile = new();

        foreach (var key in settings.Keys)
        {
            if (theme ? key.Contains("custom") : (key.Contains("profile") || key.Contains("script")))
            {
                exportFile[key] = settings[key];
            }
        }

        return exportFile;
    }

    public static async Task ImportFromFile()
    {
        if (Application.Current.ApplicationLifetime is ISingleViewApplicationLifetime android)
        {
            try
            {
                var topLevel = TopLevel.GetTopLevel(android.MainView);
                if (topLevel == null) return;
                var filePicker = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Выберите JSON-файл для импорта",
                    AllowMultiple = false,
                    FileTypeFilter = new List<FilePickerFileType>
                    {
                        new FilePickerFileType("JSON файлы") { Patterns = new[] { "*.json" } }
                    },
                    SuggestedStartLocation = await topLevel.StorageProvider.TryGetWellKnownFolderAsync(WellKnownFolder.Videos)
                });
                if (filePicker.Count == 0) return;
                await using var stream = await filePicker[0].OpenReadAsync();
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();
                ImportFile(JsonSerializer.Deserialize<Dictionary<string, string>>(json));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ошибка: {e}");
            }
        }
        else
        {
            try
            {
                Window parent = App._mainWindow;
                var filePicker = await parent.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Выберите JSON-файл для импорта",
                    AllowMultiple = false,
                    FileTypeFilter = new List<FilePickerFileType>
                    {
                        new FilePickerFileType("JSON файлы") { Patterns = new[] { "*.json" } }
                    }
                });
                if (filePicker.Count == 0) return;
                string filePath = filePicker[0].Path.LocalPath;
                string json = await File.ReadAllTextAsync(filePath);
                ImportFile(JsonSerializer.Deserialize<Dictionary<string, string>>(json));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ошибка: {e}");
            }
        }
    }

    public static void ImportFile(Dictionary<string, string>? importedSettings)
    {
        if (importedSettings != null)
        {
            foreach (var item in importedSettings)
            {
                Save(item.Key, item.Value);
            }
        }
    }
    public static void Save(string key, string value)
    {
        var settings = LoadAllSettings();
        settings[key] = value;
        File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(settings));
    }

    public static void Save(string key, int value)
    {
        var settings = LoadAllSettings();
        settings[key] = value.ToString();
        File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(settings));
    }

    public static void Save(string key, bool value)
    {
        var settings = LoadAllSettings();
        settings[key] = value.ToString();
        File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(settings));
    }

    public static string LoadString(string key, string defaultValue = "")
    {
        var settings = LoadAllSettings();
        return settings.TryGetValue(key, out var value) ? value : defaultValue;
    }

    public static int LoadInt(string key, int defaultValue = 0)
    {
        var settings = LoadAllSettings();
        return settings.TryGetValue(key, out var value) ? Int32.Parse(value) : defaultValue;
    }

    public static bool LoadBool(string key, bool defaultValue = false)
    {
        var settings = LoadAllSettings();
        return settings.TryGetValue(key, out var value) ? value.Contains("ru") : defaultValue;
    }
    
    public static void SaveDictionary(string key, Dictionary<string, bool> dictionary)
    {
        var settings = LoadAllSettings();
        settings[key] = JsonSerializer.Serialize(dictionary);
        File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(settings));
    }

    public static Dictionary<string, bool> LoadDictionary(string key)
    {
        var settings = LoadAllSettings();
        if (settings.TryGetValue(key, out var json))
        {
            return JsonSerializer.Deserialize<Dictionary<string, bool>>(json) ?? new Dictionary<string, bool>();
        }
        return new Dictionary<string, bool>();
    }

    private static Dictionary<string, string> LoadAllSettings()
    {
        if (!File.Exists(SettingsFilePath))
            return new Dictionary<string, string>();

        var json = File.ReadAllText(SettingsFilePath);
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
    }
}