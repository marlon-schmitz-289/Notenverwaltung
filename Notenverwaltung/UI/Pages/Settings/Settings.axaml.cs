using Avalonia.Controls;
using Avalonia.Threading;
using Notenverwaltung.Resources;
using Notenverwaltung.Utils;

namespace Notenverwaltung.UI.Pages.Settings;

public partial class Settings : UserControl
{
    private bool _isInitializing = true;

    public Settings()
    {
        InitializeComponent();
        PopulateLanguageComboBox();
        PopulateThemeComboBox();
        ApplyLocalization();
        _isInitializing = false;
    }

    private void ApplyLocalization()
    {
        TxtTitle.Text = Strings.SettingsTitle;
        TxtLanguageLabel.Text = Strings.SettingsLanguage;
        TxtThemeLabel.Text = Strings.SettingsTheme;
        TxtRestartHint.Text = Strings.SettingsRestartHint;
    }

    private void PopulateLanguageComboBox()
    {
        CbxLanguage.Items.Clear();
        CbxLanguage.Items.Add(
            new ComboBoxItem { Content = Strings.SettingsLanguageSystem, Tag = Language.System });
        CbxLanguage.Items.Add(new ComboBoxItem { Content = "Deutsch", Tag = Language.German });
        CbxLanguage.Items.Add(new ComboBoxItem { Content = "English", Tag = Language.English });

        foreach (var obj in CbxLanguage.Items)
        {
            if (obj is ComboBoxItem item && item.Tag is Language lang && lang == LanguageService.CurrentLanguage)
            {
                CbxLanguage.SelectedItem = item;
                break;
            }
        }
    }

    private void PopulateThemeComboBox()
    {
        CbxTheme.Items.Clear();
        CbxTheme.Items.Add(new ComboBoxItem { Content = Strings.SettingsThemeSystem, Tag = ThemeMode.System });
        CbxTheme.Items.Add(new ComboBoxItem { Content = Strings.SettingsThemeLight, Tag = ThemeMode.Light });
        CbxTheme.Items.Add(new ComboBoxItem { Content = Strings.SettingsThemeDark, Tag = ThemeMode.Dark });

        foreach (var obj in CbxTheme.Items)
        {
            if (obj is ComboBoxItem item && item.Tag is ThemeMode mode && mode == ThemeService.CurrentMode)
            {
                CbxTheme.SelectedItem = item;
                break;
            }
        }
    }

    private void CbxLanguage_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing) return;

        if (CbxLanguage.SelectedItem is ComboBoxItem item && item.Tag is Language language)
        {
            LanguageService.SetLanguage(language);

            // Defer UI refresh to after current event completes
            Dispatcher.UIThread.Post(() =>
            {
                _isInitializing = true;
                ApplyLocalization();
                PopulateLanguageComboBox();
                PopulateThemeComboBox();
                _isInitializing = false;
            });
        }
    }

    private void CbxTheme_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing) return;

        if (CbxTheme.SelectedItem is ComboBoxItem item && item.Tag is ThemeMode mode)
        {
            ThemeService.SetTheme(mode);
        }
    }
}