using System;
using System.Collections.Generic;

namespace funnytext.Services;

public static class TutorialService
{
    private static Dictionary<string, bool> _completedTutorials = new();
    public static string CurrentTutorialName;

    static TutorialService()
    {
        LoadTutorials();
    }

    private static void LoadTutorials()
    {
        _completedTutorials = FileSettingsService.LoadDictionary("tutorials");
    }

    public static bool ShouldShowTutorial(string viewModelName)
    {
        return !_completedTutorials.ContainsKey(viewModelName) || !_completedTutorials[viewModelName];
    }

    public static void CompleteTutorial()
    {
        _completedTutorials[CurrentTutorialName] = true;
        FileSettingsService.SaveDictionary("tutorials", _completedTutorials);
    }

    public static void ResetTutorials()
    {
        _completedTutorials.Clear();
        FileSettingsService.SaveDictionary("tutorials", _completedTutorials);
    }
}
