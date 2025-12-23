using System.Globalization;
using System.Resources;

namespace Notenverwaltung.Resources;

/// <summary>
///     Provides access to localized strings from resource files.
/// </summary>
public static class Strings
{
    private static readonly ResourceManager ResourceManager =
        new("Notenverwaltung.Resources.Strings", typeof(Strings).Assembly);

    // App Title
    public static string AppTitle => Get("AppTitle");

    // Common
    public static string CommonPleaseSelect => Get("Common_PleaseSelect");

    // Main Window - Menu Buttons
    public static string MenuAddGrade => Get("Menu_AddGrade");
    public static string MenuManageGrades => Get("Menu_ManageGrades");
    public static string MenuShowAverages => Get("Menu_ShowAverages");
    public static string MenuShowAllGrades => Get("Menu_ShowAllGrades");
    public static string MenuEditSubjects => Get("Menu_EditSubjects");

    // AddGrade Page
    public static string AddGradeTitle => Get("AddGrade_Title");
    public static string AddGradeSubject => Get("AddGrade_Subject");
    public static string AddGradeRating => Get("AddGrade_Rating");
    public static string AddGradeType => Get("AddGrade_Type");
    public static string AddGradeDate => Get("AddGrade_Date");
    public static string AddGradeReset => Get("AddGrade_Reset");
    public static string AddGradeSave => Get("AddGrade_Save");
    public static string AddGradeFillAllFields => Get("AddGrade_FillAllFields");
    public static string AddGradeSavedSuccessfully => Get("AddGrade_SavedSuccessfully");

    // EditGrades Page
    public static string EditGradesTitle => Get("EditGrades_Title");
    public static string EditGradesSubject => Get("EditGrades_Subject");
    public static string EditGradesRating => Get("EditGrades_Rating");
    public static string EditGradesType => Get("EditGrades_Type");
    public static string EditGradesDate => Get("EditGrades_Date");
    public static string EditGradesSave => Get("EditGrades_Save");
    public static string EditGradesAddNew => Get("EditGrades_AddNew");

    // ShowGrades Page
    public static string ShowGradesAllSubjects => Get("ShowGrades_AllSubjects");
    public static string ShowGradesDelete => Get("ShowGrades_Delete");

    // ShowAverages Page
    public static string ShowAveragesTitle => Get("ShowAverages_Title");
    public static string ShowAveragesNoEntries => Get("ShowAverages_NoEntries");

    // EditSubs Page
    public static string EditSubsTitle => Get("EditSubs_Title");
    public static string EditSubsAdd => Get("EditSubs_Add");
    public static string EditSubsDelete => Get("EditSubs_Delete");

    // SubEditDlg
    public static string SubEditDlgTitle => Get("SubEditDlg_Title");
    public static string SubEditDlgSave => Get("SubEditDlg_Save");

    // FirstTimeSubjectsDialog
    public static string FirstTimeTitle => Get("FirstTime_Title");
    public static string FirstTimeAutoSaveHint => Get("FirstTime_AutoSaveHint");
    public static string FirstTimeSubjectNamePlaceholder => Get("FirstTime_SubjectNamePlaceholder");
    public static string FirstTimeSaveToList => Get("FirstTime_SaveToList");
    public static string FirstTimeRemoveFromList => Get("FirstTime_RemoveFromList");
    public static string FirstTimeDone => Get("FirstTime_Done");

    // MessageDialog
    public static string MessageDialogTitle => Get("MessageDialog_Title");
    public static string MessageDialogOk => Get("MessageDialog_OK");

    // Messages
    public static string MessageWaitForData => Get("Message_WaitForData");

    // Discord Status Messages
    public static string DiscordMainMenu => Get("Discord_MainMenu");
    public static string DiscordDoingNothing => Get("Discord_DoingNothing");
    public static string DiscordClosingApp => Get("Discord_ClosingApp");
    public static string DiscordClosingAppState => Get("Discord_ClosingAppState");
    public static string DiscordGradesOverview => Get("Discord_GradesOverview");
    public static string DiscordViewingGrades => Get("Discord_ViewingGrades");
    public static string DiscordAveragesOverview => Get("Discord_AveragesOverview");
    public static string DiscordViewingBadAverages => Get("Discord_ViewingBadAverages");
    public static string DiscordAddingGrade => Get("Discord_AddingGrade");
    public static string DiscordAddingBadGrade => Get("Discord_AddingBadGrade");
    public static string DiscordEditingGrades => Get("Discord_EditingGrades");
    public static string DiscordFalsifyingGrades => Get("Discord_FalsifyingGrades");
    public static string DiscordEditingSubjects => Get("Discord_EditingSubjects");
    public static string DiscordMessingWithSchedule => Get("Discord_MessingWithSchedule");
    public static string DiscordEditingSubjectList => Get("Discord_EditingSubjectList");
    public static string DiscordEditingSubjectListState => Get("Discord_EditingSubjectListState");
    public static string DiscordAddingSubject => Get("Discord_AddingSubject");
    public static string DiscordForgotSubject => Get("Discord_ForgotSubject");

    // Theme
    public static string ThemeLightMode => Get("Theme_LightMode");
    public static string ThemeDarkMode => Get("Theme_DarkMode");

    // Settings Page
    public static string MenuSettings => Get("Menu_Settings");
    public static string SettingsTitle => Get("Settings_Title");
    public static string SettingsLanguage => Get("Settings_Language");
    public static string SettingsLanguageSystem => Get("Settings_Language_System");
    public static string SettingsTheme => Get("Settings_Theme");
    public static string SettingsThemeSystem => Get("Settings_Theme_System");
    public static string SettingsThemeLight => Get("Settings_Theme_Light");
    public static string SettingsThemeDark => Get("Settings_Theme_Dark");
    public static string SettingsRestartHint => Get("Settings_RestartHint");
    public static string DiscordSettings => Get("Discord_Settings");
    public static string DiscordChangingSettings => Get("Discord_ChangingSettings");

    // Grade Types
    public static string GradeTypeSimple => Get("GradeType_Simple");
    public static string GradeTypeDouble => Get("GradeType_Double");

    // Sort Options
    public static string SortByName => Get("Sort_ByName");
    public static string SortByGradeAsc => Get("Sort_ByGradeAsc");
    public static string SortByGradeDesc => Get("Sort_ByGradeDesc");

    // Filter Options
    public static string FilterAllTypes => Get("Filter_AllTypes");
    public static string FilterFrom => Get("Filter_From");
    public static string FilterTo => Get("Filter_To");
    public static string FilterClearDate => Get("Filter_ClearDate");

    /// <summary>
    ///     Gets a localized string by key using the current UI culture.
    /// </summary>
    public static string Get(string key)
    {
        return ResourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;
    }

    /// <summary>
    ///     Gets a localized string by key and formats it with arguments.
    /// </summary>
    public static string Format(string key, params object[] args)
    {
        var format = Get(key);
        return string.Format(format, args);
    }
}