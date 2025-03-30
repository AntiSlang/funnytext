using System;
using Avalonia.Markup.Xaml;
using funnytext.Services;

namespace funnytext.Extensions
{
    public class LocalizationExtension : MarkupExtension
    {
        public string Key { get; set; }

        public LocalizationExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return LocalizationService.GetString(Key);
        }
    }
}