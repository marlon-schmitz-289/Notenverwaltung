using System;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Styling;

namespace Notenverwaltung.Utils;

public enum ThemeMode
{
    System,
    Light,
    Dark
}

public static class ThemeService
{
    public static ThemeMode CurrentMode { get; private set; } = ThemeMode.System;

    public static bool IsDarkMode => GetEffectiveTheme() == ThemeVariant.Dark;

    public static event Action? ThemeChanged;

    public static void Initialize()
    {
        LoadThemePreference();
        ApplyTheme();
    }

    public static void ToggleTheme()
    {
        CurrentMode = CurrentMode switch
        {
            ThemeMode.System => ThemeMode.Light,
            ThemeMode.Light => ThemeMode.Dark,
            ThemeMode.Dark => ThemeMode.System,
            _ => ThemeMode.System
        };
        ApplyTheme();
        SaveThemePreference();
        ThemeChanged?.Invoke();
    }

    public static void SetTheme(ThemeMode mode)
    {
        if (CurrentMode == mode) return;

        CurrentMode = mode;
        ApplyTheme();
        SaveThemePreference();
        ThemeChanged?.Invoke();
    }

    private static ThemeVariant GetEffectiveTheme()
    {
        if (CurrentMode == ThemeMode.System)
        {
            var platformSettings = Application.Current?.PlatformSettings;
            if (platformSettings != null)
            {
                var colorValues = platformSettings.GetColorValues();
                return colorValues.ThemeVariant == PlatformThemeVariant.Dark
                    ? ThemeVariant.Dark
                    : ThemeVariant.Light;
            }

            return ThemeVariant.Dark;
        }

        return CurrentMode == ThemeMode.Dark ? ThemeVariant.Dark : ThemeVariant.Light;
    }

    private static void ApplyTheme()
    {
        if (Application.Current is null) return;

        Application.Current.RequestedThemeVariant = GetEffectiveTheme();
    }

    private static void LoadThemePreference()
    {
        var value = SettingsService.Settings.Theme.ToLower();
        CurrentMode = value switch
        {
            "light" => ThemeMode.Light,
            "dark" => ThemeMode.Dark,
            _ => ThemeMode.System
        };
    }

    private static void SaveThemePreference()
    {
        SettingsService.Settings.Theme = CurrentMode switch
        {
            ThemeMode.Light => "light",
            ThemeMode.Dark => "dark",
            _ => "system"
        };
        SettingsService.Save();
    }
}
