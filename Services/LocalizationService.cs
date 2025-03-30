using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Avalonia.Platform;
using Newtonsoft.Json;

namespace funnytext.Services
{
    public static class LocalizationService
    {
        private static Dictionary<string, Dictionary<string, string>> _translations = new();
        public static string CurrentLanguage { get; private set; } = "en";

        static LocalizationService()
        {
            if (!FileSettingsService.LoadBool("firsttimelanguage", false))
            {
                string systemLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                string[] supportedLanguages = { "en", "ru" };
                FileSettingsService.Save("language", supportedLanguages.Contains(systemLanguage) ? systemLanguage : "en");
                FileSettingsService.Save("firsttimelanguage", true);
            }

            CurrentLanguage = FileSettingsService.LoadString("language", "en");
            LoadTranslations();
        }

        private static void LoadTranslations()
        {
            var uri = new Uri("avares://funnytext/Assets/Localization.json");
            using var stream = AssetLoader.Open(uri);
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            _translations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
        }

        public static string GetString(string key)
        {
            if (_translations.TryGetValue(CurrentLanguage, out var langDict) && langDict.TryGetValue(key, out var value))
                return value;
            return key;
        }
    }
}