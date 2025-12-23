using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Notenverwaltung.Model;
using Notenverwaltung.Model.Enums;
using Notenverwaltung.Resources;
using Notenverwaltung.UI.Windows;

namespace Notenverwaltung.UI.Pages.Grade;

public partial class AddGrade : UserControl
{
    private Windows.MainWindow? _mainWindow;

    public AddGrade()
    {
        InitializeComponent();
        ApplyLocalization();
        Loaded += OnLoaded;
    }

    private void ApplyLocalization()
    {
        TxtTitle.Text = Strings.AddGradeTitle;
        TxtSubject.Text = Strings.AddGradeSubject;
        TxtRating.Text = Strings.AddGradeRating;
        TxtType.Text = Strings.AddGradeType;
        TxtDate.Text = Strings.AddGradeDate;
        ToolTip.SetTip(BtnClear, Strings.AddGradeReset);
        ToolTip.SetTip(BtnSave, Strings.AddGradeSave);
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _mainWindow = TopLevel.GetTopLevel(this) as MainWindow;
        FillComboBoxes();
    }

    private void FillComboBoxes()
    {
        var placeholder = Strings.CommonPleaseSelect;

        // Fill subjects
        CbxSubject.Items.Clear();
        CbxSubject.Items.Add(placeholder);
        foreach (var s in Subject.Subjects)
            CbxSubject.Items.Add(s);

        // Fill ratings 1-6
        CbxRating.Items.Clear();
        CbxRating.Items.Add(placeholder);
        for (var i = 1; i <= 6; i++)
            CbxRating.Items.Add(i);

        // Fill grade types
        CbxType.Items.Clear();
        CbxType.Items.Add(new ComboBoxItem { Content = placeholder, Tag = (TypeGrade?)null });
        CbxType.Items.Add(new ComboBoxItem { Content = Strings.GradeTypeSimple, Tag = TypeGrade.Simple });
        CbxType.Items.Add(new ComboBoxItem { Content = Strings.GradeTypeDouble, Tag = TypeGrade.Double });

        // Set placeholder as default selection
        CbxSubject.SelectedIndex = 0;
        CbxRating.SelectedIndex = 0;
        CbxType.SelectedIndex = 0;

        // Set default date to today
        DpDate.SelectedDate = DateTime.Today;
    }

    private void BtnSave_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            // Extract TypeGrade from ComboBoxItem's Tag
            TypeGrade? type = null;
            if (CbxType.SelectedItem is ComboBoxItem typeItem && typeItem.Tag is TypeGrade t)
                type = t;

            if (CbxSubject.SelectedItem is not Subject subject ||
                CbxRating.SelectedItem is not int rating ||
                type is null ||
                DpDate.SelectedDate is not DateTime selectedDate)
            {
                _mainWindow?.ShowNotification(Strings.AddGradeFillAllFields, NotificationType.Error);
                return;
            }

            var g = new Model.Grade
            {
                Id = Model.Grade.GetNextId(),
                Subject = subject,
                Rating = rating,
                TypeG = type.Value,
                Creation = selectedDate
            };

            g.Save();
            ClearComboBoxes();
            _mainWindow?.ShowNotification(Strings.AddGradeSavedSuccessfully, NotificationType.Success);
        }
        catch (Exception ex)
        {
            _mainWindow?.ShowNotification(Strings.Format("AddGrade_Error", ex.Message), NotificationType.Error);
        }
    }

    private void BtnClear_Click(object? sender, RoutedEventArgs e)
    {
        ClearComboBoxes();
    }

    private void ClearComboBoxes()
    {
        // Reset to placeholder
        CbxSubject.SelectedIndex = 0;
        CbxRating.SelectedIndex = 0;
        CbxType.SelectedIndex = 0;
        DpDate.SelectedDate = DateTime.Today;
    }

    private void ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        var subjectSelected = CbxSubject.SelectedIndex > 0;
        var ratingSelected = CbxRating.SelectedIndex > 0;
        var typeSelected = CbxType.SelectedIndex > 0;

        // Reset enabled if at least one is not default
        BtnClear.IsEnabled = subjectSelected || ratingSelected || typeSelected;

        // Save enabled only if all are selected
        BtnSave.IsEnabled = subjectSelected && ratingSelected && typeSelected;
    }
}