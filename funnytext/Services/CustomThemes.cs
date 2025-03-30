using System;
using Avalonia.Styling;

namespace funnytext.Services;

public static class CustomThemes
{
    public static readonly ThemeVariant ArabLight = new("ArabLight", ThemeVariant.Light);
    public static readonly ThemeVariant ArabDark = new("ArabDark", ThemeVariant.Dark);
    public static ThemeVariant Custom1 = new("Custom1", ThemeVariant.Light);
    public static ThemeVariant Custom2 = new("Custom2", ThemeVariant.Light);
    public static ThemeVariant Custom3 = new("Custom3", ThemeVariant.Light);
    public static ThemeVariant Custom4 = new("Custom4", ThemeVariant.Light);
    public static ThemeVariant Custom5 = new("Custom5", ThemeVariant.Light);

    public static string[][] Colors =
    [
        ["#B6F4FF", "#63D1F4", "#000000", "", "", "Светлая", "Light", "#63D1F4", "#000000"],
        ["#006D80", "#0097B2", "#FFFFFF", "", "", "Тёмная", "Dark", "#0097B2", "#FFFFFF"],
        ["#FFF9F9", "#FFE2E2", "#000000", "avares://funnytext/Assets/fon.png", "avares://funnytext/Assets/fonfon.png", "Арабская светлая", "Light", "#FFE2E2", "#000000"],
        ["#1A0000", "#AB0000", "#FFFFFF", "avares://funnytext/Assets/fond.png", "avares://funnytext/Assets/fonfond.png", "Арабская тёмная", "Dark", "#AB0000", "#FFFFFF"],
        ["#B6F4FF", "#63D1F4", "#000000", "", "", "Кастом 1", "Light", "#63D1F4", "#000000"],
        ["#B6F4FF", "#63D1F4", "#000000", "", "", "Кастом 2", "Light", "#63D1F4", "#000000"],
        ["#B6F4FF", "#63D1F4", "#000000", "", "", "Кастом 3", "Light", "#63D1F4", "#000000"],
        ["#B6F4FF", "#63D1F4", "#000000", "", "", "Кастом 4", "Light", "#63D1F4", "#000000"],
        ["#B6F4FF", "#63D1F4", "#000000", "", "", "Кастом 5", "Light", "#63D1F4", "#000000"],
        ["#B6F4FF", "#63D1F4", "#000000", "", "", "Расписание", "Light", "#63D1F4", "#000000"]
    ];

    public static void Reload()
    {
        for (int i = 4; i <= 8; i++)
        {
            Colors[i] =
            [
                FileSettingsService.LoadString($"custom_color_1_{i - 3}", "#B6F4FF"),
                FileSettingsService.LoadString($"custom_color_2_{i - 3}", "#ADE7FF"),
                FileSettingsService.LoadString($"custom_color_3_{i - 3}", "#000000"),
                FileSettingsService.LoadString($"custom_back_{i - 3}"),
                FileSettingsService.LoadString($"custom_back_2_{i - 3}"),
                FileSettingsService.LoadString($"custom_name_{i - 3}", $"Кастом {i - 3}"),
                FileSettingsService.LoadString($"custom_base_{i - 3}", "Light"),
                FileSettingsService.LoadString($"custom_color_4_{i - 3}", "#ADE7FF"),
                FileSettingsService.LoadString($"custom_color_5_{i - 3}", "#000000")
            ];
            switch (i)
            {
                case 4:
                    Custom1 = new("Custom1", Colors[i][6] == "Dark" ? ThemeVariant.Dark : ThemeVariant.Light);
                    break;
                case 5:
                    Custom2 = new("Custom2", Colors[i][6] == "Dark" ? ThemeVariant.Dark : ThemeVariant.Light);
                    break;
                case 6:
                    Custom3 = new("Custom3", Colors[i][6] == "Dark" ? ThemeVariant.Dark : ThemeVariant.Light);
                    break;
                case 7:
                    Custom4 = new("Custom4", Colors[i][6] == "Dark" ? ThemeVariant.Dark : ThemeVariant.Light);
                    break;
                default:
                    Custom5 = new("Custom5", Colors[i][6] == "Dark" ? ThemeVariant.Dark : ThemeVariant.Light);
                    break;
            }
        }
    }
}