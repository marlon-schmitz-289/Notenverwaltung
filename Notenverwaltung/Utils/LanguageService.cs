using System;
using System.Globalization;
using System.Threading;

namespace Notenverwaltung.Utils;

public enum Language
{
    System,
    German,
    English
}

public static class LanguageService
{
    private static readonly CultureInfo SystemCulture = CultureInfo.CurrentUICulture;

    public static Language CurrentLanguage { get; private set; } = Language.System;

    public static event Action? LanguageChanged;

    public static void Initialize()
    {
        LoadLanguagePreference();
        ApplyLanguage();
    }

    public static void SetLanguage(Language language)
    {
        if (CurrentLanguage == language) return;

        CurrentLanguage = language;
        ApplyLanguage();
        SaveLanguagePreference();
        LanguageChanged?.Invoke();
    }

    public static CultureInfo GetEffectiveCulture()
    {
        return CurrentLanguage switch
        {
            Language.German => new CultureInfo("de-DE"),
            Language.English => new CultureInfo("en-US"),
            _ => SystemCulture
        };
    }

    private static void ApplyLanguage()
    {
        var culture = GetEffectiveCulture();
        Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.CurrentUICulture = culture;
    }

    private static void LoadLanguagePreference()
    {
        var value = SettingsService.Settings.Language.ToLower();
        CurrentLanguage = value switch
        {
            "de" or "german" => Language.German,
            "en" or "english" => Language.English,
            _ => Language.System
        };
    }

    private static void SaveLanguagePreference()
    {
        SettingsService.Settings.Language = CurrentLanguage switch
        {
            Language.German => "de",
            Language.English => "en",
            _ => "system"
        };
        SettingsService.Save();
    }
}
