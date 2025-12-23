using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using Notenverwaltung.Model;
using Notenverwaltung.Model.Enums;
using Notenverwaltung.Resources;
using Notenverwaltung.UI.Pages.Grade;
using Notenverwaltung.UI.Pages.Settings;
using Notenverwaltung.UI.Pages.Subs;
using Notenverwaltung.Utils;

namespace Notenverwaltung.UI.Windows;

public enum NotificationType
{
    Success,
    Error,
    Info
}

public partial class MainWindow : Window
{
    private CurrentPage _currPage = CurrentPage.ShowAll;
    private DispatcherTimer? _notificationTimer;
    private bool _sidebarCollapsed;

    public MainWindow()
    {
        InitializeComponent();
        LanguageService.Initialize();
        ApplyLocalization();
        Loaded += Window_Loaded;
        Closing += Window_Closing;
        LanguageService.LanguageChanged += ApplyLocalization;

        _notificationTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(4)
        };
        _notificationTimer.Tick += (_, _) => HideNotification();

        // Restore sidebar state
        if (SettingsService.Settings.SidebarCollapsed)
        {
            _sidebarCollapsed = true;
            ApplySidebarState();
        }
    }

    private void ApplyLocalization()
    {
        Title = Strings.AppTitle;
        TxtTitle.Text = Strings.AppTitle;
        TxtAddGrade.Text = Strings.MenuAddGrade;
        TxtEditGrade.Text = Strings.MenuManageGrades;
        TxtListAvgs.Text = Strings.MenuShowAverages;
        TxtListAll.Text = Strings.MenuShowAllGrades;
        TxtEditSubs.Text = Strings.MenuEditSubjects;
        TxtSettingsLabel.Text = Strings.MenuSettings;
    }

    private void BtnSettings_Click(object? sender, RoutedEventArgs e)
    {
        _currPage = CurrentPage.Settings;
        SwitchPage();
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        Subject.ReadAll();
        Grade.ReadAll();

        // If no subjects exist, navigate to EditSubs page
        if (Subject.Subjects.Count == 0)
        {
            _currPage = CurrentPage.EditSubs;
        }

        DiscordClient.Initialize();
        SwitchPage();
    }

    private void Window_Closing(object? sender, WindowClosingEventArgs e)
    {
        LanguageService.LanguageChanged -= ApplyLocalization;
        DiscordClient.UpdateClient(Strings.DiscordClosingApp, Strings.DiscordClosingAppState);

        Grade.SaveAll();
        Subject.SaveAll();

        DiscordClient.Deinitialize();
    }

    private void SwitchPage()
    {
        switch (_currPage)
        {
            case CurrentPage.ShowAll:
                MainContent.Content = new ShowGrades();
                DiscordClient.UpdateClient(Strings.DiscordGradesOverview, Strings.DiscordViewingGrades);
                break;
            case CurrentPage.ShowAvgs:
                MainContent.Content = new ShowAverages();
                DiscordClient.UpdateClient(Strings.DiscordAveragesOverview, Strings.DiscordViewingBadAverages);
                break;
            case CurrentPage.NewEntry:
                MainContent.Content = new AddGrade();
                DiscordClient.UpdateClient(Strings.DiscordAddingGrade, Strings.DiscordAddingBadGrade);
                break;
            case CurrentPage.EditEntry:
                MainContent.Content = new EditGrades();
                DiscordClient.UpdateClient(Strings.DiscordEditingGrades, Strings.DiscordFalsifyingGrades);
                break;
            case CurrentPage.EditSubs:
                MainContent.Content = new EditSubs();
                DiscordClient.UpdateClient(Strings.DiscordEditingSubjects, Strings.DiscordMessingWithSchedule);
                break;
            case CurrentPage.Settings:
                MainContent.Content = new Settings();
                DiscordClient.UpdateClient(Strings.DiscordSettings, Strings.DiscordChangingSettings);
                break;
        }
    }

    private void BtnAddGrade_Click(object? sender, RoutedEventArgs e)
    {
        _currPage = CurrentPage.NewEntry;
        SwitchPage();
    }

    private void BtnEditGrade_Click(object? sender, RoutedEventArgs e)
    {
        _currPage = CurrentPage.EditEntry;
        SwitchPage();
    }

    private void BtnListAvgs_Click(object? sender, RoutedEventArgs e)
    {
        _currPage = CurrentPage.ShowAvgs;
        SwitchPage();
    }

    private void BtnListAll_Click(object? sender, RoutedEventArgs e)
    {
        if (Grade.Grades != null)
        {
            _currPage = CurrentPage.ShowAll;
            SwitchPage();
            return;
        }

        ShowNotification(Strings.MessageWaitForData);
    }

    private void BtnEditSubs_Click(object? sender, RoutedEventArgs e)
    {
        _currPage = CurrentPage.EditSubs;
        SwitchPage();
    }

    private void BtnToggleSidebar_Click(object? sender, RoutedEventArgs e)
    {
        _sidebarCollapsed = !_sidebarCollapsed;
        ApplySidebarState();

        // Save state
        SettingsService.Settings.SidebarCollapsed = _sidebarCollapsed;
        SettingsService.Save();
    }

    private void ApplySidebarState()
    {
        if (_sidebarCollapsed)
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(60);
            TxtTitle.IsVisible = false;
            TxtAddGrade.IsVisible = false;
            TxtEditGrade.IsVisible = false;
            TxtListAvgs.IsVisible = false;
            TxtListAll.IsVisible = false;
            TxtEditSubs.IsVisible = false;
            TxtSettingsLabel.IsVisible = false;
            ToggleIcon.Data = Geometry.Parse("M8.59,16.58L13.17,12L8.59,7.41L10,6L16,12L10,18L8.59,16.58Z");
        }
        else
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(200);
            TxtTitle.IsVisible = true;
            TxtAddGrade.IsVisible = true;
            TxtEditGrade.IsVisible = true;
            TxtListAvgs.IsVisible = true;
            TxtListAll.IsVisible = true;
            TxtEditSubs.IsVisible = true;
            TxtSettingsLabel.IsVisible = true;
            ToggleIcon.Data = Geometry.Parse("M15.41,16.58L10.83,12L15.41,7.41L14,6L8,12L14,18L15.41,16.58Z");
        }
    }

    public void ShowNotification(string text, NotificationType type = NotificationType.Info)
    {
        _notificationTimer?.Stop();

        NotificationText.Text = text;

        switch (type)
        {
            case NotificationType.Success:
                NotificationBanner.Background = new SolidColorBrush(Color.Parse("#4CAF50"));
                NotificationIcon.Text = "✓";
                break;
            case NotificationType.Error:
                NotificationBanner.Background = new SolidColorBrush(Color.Parse("#F44336"));
                NotificationIcon.Text = "✕";
                break;
            default:
                NotificationBanner.Background = new SolidColorBrush(Color.Parse("#2196F3"));
                NotificationIcon.Text = "ℹ";
                break;
        }

        NotificationText.Foreground = Brushes.White;
        NotificationIcon.Foreground = Brushes.White;

        NotificationBanner.IsVisible = true;
        _notificationTimer?.Start();
    }

    private void HideNotification()
    {
        _notificationTimer?.Stop();
        NotificationBanner.IsVisible = false;
    }

    private void NotificationClose_Click(object? sender, RoutedEventArgs e)
    {
        HideNotification();
    }

    public void NavigateToPage(CurrentPage page)
    {
        _currPage = page;
        SwitchPage();
    }
}