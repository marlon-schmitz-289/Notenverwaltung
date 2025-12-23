using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Notenverwaltung.Model;
using Notenverwaltung.Model.Enums;
using Notenverwaltung.Resources;
using Notenverwaltung.Utils;

namespace Notenverwaltung.UI.Pages.Grade;

public partial class ShowGrades : UserControl
{
    private Model.Grade? _selectedGrade;
    private bool _isInitializing = true;

    public ShowGrades()
    {
        InitializeComponent();
        ApplyLocalization();
        Loaded += OnLoaded;
    }

    private void ApplyLocalization()
    {
        ToolTip.SetTip(BtnDelete, Strings.ShowGradesDelete);
        ToolTip.SetTip(BtnClearDate, Strings.FilterClearDate);
        TxtFrom.Text = Strings.FilterFrom;
        TxtTo.Text = Strings.FilterTo;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        FillSubjects();
        FillTypes();
        CbxSubjects.SelectedIndex = 0;
        CbxType.SelectedIndex = 0;
        _isInitializing = false;
        FillGrades();
    }

    private void FillSubjects()
    {
        CbxSubjects.Items.Clear();
        CbxSubjects.Items.Add(Strings.ShowGradesAllSubjects);

        foreach (var s in Subject.Subjects)
            CbxSubjects.Items.Add(s);
    }

    private void FillTypes()
    {
        CbxType.Items.Clear();
        CbxType.Items.Add(new ComboBoxItem { Content = Strings.FilterAllTypes, Tag = null });
        CbxType.Items.Add(new ComboBoxItem { Content = Strings.GradeTypeSimple, Tag = TypeGrade.Simple });
        CbxType.Items.Add(new ComboBoxItem { Content = Strings.GradeTypeDouble, Tag = TypeGrade.Double });
    }

    private void FillGrades()
    {
        if (_isInitializing) return;

        GradesItems.Items.Clear();
        _selectedGrade = null;
        BtnDelete.IsEnabled = false;

        // Get filter values
        Subject? subjectFilter = CbxSubjects.SelectedItem as Subject;
        TypeGrade? typeFilter = null;
        if (CbxType.SelectedItem is ComboBoxItem typeItem && typeItem.Tag is TypeGrade type)
            typeFilter = type;

        DateTime? fromDate = DpFrom.SelectedDate;
        DateTime? toDate = DpTo.SelectedDate;

        foreach (var g in Model.Grade.Grades)
        {
            // Subject filter
            if (subjectFilter != null && !g.Subject.Name.Equals(subjectFilter.Name))
                continue;

            // Type filter
            if (typeFilter != null && g.TypeG != typeFilter)
                continue;

            // Date filters
            if (fromDate != null && g.Creation.Date < fromDate)
                continue;
            if (toDate != null && g.Creation.Date > toDate)
                continue;

            GradesItems.Items.Add(CreateGradeCard(g));
        }
    }

    private Border CreateGradeCard(Model.Grade grade)
    {
        var gradeColor = GetGradeColor(grade.Rating);

        var card = new Border
        {
            CornerRadius = new CornerRadius(6),
            Padding = new Thickness(12, 10),
            BorderThickness = new Thickness(3, 0, 0, 0),
            BorderBrush = gradeColor,
            Cursor = new Cursor(StandardCursorType.Hand),
            Tag = grade
        };

        card.Bind(Border.BackgroundProperty,
            Application.Current!.GetResourceObservable("SystemControlBackgroundChromeMediumLowBrush"));

        card.PointerPressed += GradeCard_PointerPressed;

        var grid = new Grid
        {
            ColumnDefinitions = ColumnDefinitions.Parse("Auto,*,Auto,Auto")
        };

        // Grade number on the left
        var gradeText = new TextBlock
        {
            Text = grade.Rating.ToString(),
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Foreground = gradeColor,
            VerticalAlignment = VerticalAlignment.Center,
            Width = 25
        };
        Grid.SetColumn(gradeText, 0);
        grid.Children.Add(gradeText);

        // Subject name
        var subjectText = new TextBlock
        {
            Text = grade.Subject.Name,
            FontSize = 15,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10, 0, 0, 0)
        };
        Grid.SetColumn(subjectText, 1);
        grid.Children.Add(subjectText);

        // Type
        var typeText = new TextBlock
        {
            Text = grade.TypeG.GetDisplayName(),
            FontSize = 13,
            Opacity = 0.7,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10, 0, 0, 0)
        };
        Grid.SetColumn(typeText, 2);
        grid.Children.Add(typeText);

        // Date
        var dateText = new TextBlock
        {
            Text = grade.Creation.ToString("d"),
            FontSize = 13,
            Opacity = 0.7,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(15, 0, 0, 0),
            Width = 80,
            TextAlignment = TextAlignment.Right
        };
        Grid.SetColumn(dateText, 3);
        grid.Children.Add(dateText);

        card.Child = grid;
        return card;
    }

    private void GradeCard_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border { Tag: Model.Grade grade } card)
        {
            // If clicking the same card, deselect it
            if (_selectedGrade == grade)
            {
                card.BorderThickness = new Thickness(3, 0, 0, 0);
                _selectedGrade = null;
                BtnDelete.IsEnabled = false;
                return;
            }

            // Deselect previous
            foreach (var item in GradesItems.Items)
            {
                if (item is Border b)
                    b.BorderThickness = new Thickness(3, 0, 0, 0);
            }

            // Select current
            card.BorderThickness = new Thickness(3);
            _selectedGrade = grade;
            BtnDelete.IsEnabled = true;
        }
    }

    private static ISolidColorBrush GetGradeColor(int grade)
    {
        return grade switch
        {
            1 => new SolidColorBrush(Color.FromRgb(46, 160, 67)),
            2 => new SolidColorBrush(Color.FromRgb(86, 171, 95)),
            3 => new SolidColorBrush(Color.FromRgb(255, 193, 7)),
            4 => new SolidColorBrush(Color.FromRgb(255, 152, 0)),
            5 => new SolidColorBrush(Color.FromRgb(244, 67, 54)),
            6 => new SolidColorBrush(Color.FromRgb(183, 28, 28)),
            _ => new SolidColorBrush(Color.FromRgb(128, 128, 128))
        };
    }

    private void CbxSubjects_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        FillGrades();
    }

    private void CbxType_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        FillGrades();
    }

    private void DateFilter_Changed(object? sender, SelectionChangedEventArgs e)
    {
        FillGrades();
    }

    private void BtnClearDate_Click(object? sender, RoutedEventArgs e)
    {
        DpFrom.SelectedDate = null;
        DpTo.SelectedDate = null;
        FillGrades();
    }

    private void BtnDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (_selectedGrade != null)
        {
            _selectedGrade.Delete();
            FillGrades();
        }
    }
}
