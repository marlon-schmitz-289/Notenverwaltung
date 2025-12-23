using Avalonia.Controls;
using Avalonia.Interactivity;
using Notenverwaltung.Model;
using Notenverwaltung.Model.Enums;
using Notenverwaltung.Resources;
using Notenverwaltung.UI.Windows;

namespace Notenverwaltung.UI.Pages.Grade;

public partial class EditGrades : UserControl
{
    public EditGrades()
    {
        InitializeComponent();
        ApplyLocalization();
        Loaded += OnLoaded;
    }

    private void ApplyLocalization()
    {
        TxtTitle.Text = Strings.EditGradesTitle;
        TxtSubject.Text = Strings.EditGradesSubject;
        TxtRating.Text = Strings.EditGradesRating;
        TxtType.Text = Strings.EditGradesType;
        TxtDate.Text = Strings.EditGradesDate;
        ToolTip.SetTip(BtnSave, Strings.EditGradesSave);
        ToolTip.SetTip(BtnAdd, Strings.EditGradesAddNew);
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        FillListBoxes();
    }

    private Model.Grade? GetSelectedValues()
    {
        if (LbxGrades.SelectedItem is not Model.Grade tmp) return null;

        if (LbxSubject.SelectedItem is Subject subject)
            tmp.Subject = subject;
        tmp.Rating = LbxRating.SelectedItem is int rating ? rating : 0;

        // Extract TypeGrade from ListBoxItem's Tag
        if (LbxType.SelectedItem is ListBoxItem typeItem && typeItem.Tag is TypeGrade type)
            tmp.TypeG = type;

        tmp.Creation = DpDate.SelectedDate ?? tmp.Creation;

        return tmp;
    }

    private void SetSelectedValues()
    {
        if (LbxGrades.SelectedItem is Model.Grade tmp)
        {
            LbxSubject.SelectedItem = tmp.Subject;
            LbxRating.SelectedItem = tmp.Rating;

            // Find the ListBoxItem with matching TypeGrade Tag
            foreach (var item in LbxType.Items)
            {
                if (item is ListBoxItem lbi && lbi.Tag is TypeGrade type && type == tmp.TypeG)
                {
                    LbxType.SelectedItem = lbi;
                    break;
                }
            }

            DpDate.SelectedDate = tmp.Creation;
            SetEditControlsEnabled(true);
            return;
        }

        // Reset to placeholder and disable controls
        LbxSubject.SelectedIndex = 0;
        LbxRating.SelectedIndex = 0;
        LbxType.SelectedIndex = 0;
        DpDate.SelectedDate = null;
        SetEditControlsEnabled(false);
    }

    private void SetEditControlsEnabled(bool enabled)
    {
        EditPanel.IsEnabled = enabled;
        BtnSave.IsEnabled = enabled;
    }

    private void FillListBoxes()
    {
        FillGrades();
        FillRating();
        FillSubjects();
        FillTypes();
    }

    private void FillTypes()
    {
        LbxType.Items.Clear();
        LbxType.Items.Add(new ListBoxItem { Content = Strings.CommonPleaseSelect, Tag = null });
        LbxType.Items.Add(new ListBoxItem { Content = Strings.GradeTypeSimple, Tag = TypeGrade.Simple });
        LbxType.Items.Add(new ListBoxItem { Content = Strings.GradeTypeDouble, Tag = TypeGrade.Double });
    }

    private void FillSubjects()
    {
        LbxSubject.Items.Clear();
        LbxSubject.Items.Add(Strings.CommonPleaseSelect);
        foreach (var s in Subject.Subjects)
            LbxSubject.Items.Add(s);
    }

    private void FillRating()
    {
        LbxRating.Items.Clear();
        LbxRating.Items.Add(Strings.CommonPleaseSelect);
        for (var i = 1; i < 7; i++)
            LbxRating.Items.Add(i);
    }

    private void FillGrades()
    {
        LbxGrades.Items.Clear();
        LbxGrades.Items.Add(Strings.CommonPleaseSelect);
        foreach (var g in Model.Grade.Grades)
            LbxGrades.Items.Add(g);
        LbxGrades.SelectedIndex = 0;
    }

    private void BtnSave_Click(object? sender, RoutedEventArgs e)
    {
        if (LbxGrades.SelectedIndex != -1)
        {
            var tmp = GetSelectedValues();
            tmp?.Update();

            Model.Grade.ReadAll();
            FillGrades();
        }
    }

    private void LbxGrades_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        SetSelectedValues();
    }

    private void BtnAdd_Click(object? sender, RoutedEventArgs e)
    {
        // Navigate to AddGrade page via MainWindow
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel is MainWindow mainWindow)
        {
            mainWindow.NavigateToPage(CurrentPage.NewEntry);
        }
    }
}