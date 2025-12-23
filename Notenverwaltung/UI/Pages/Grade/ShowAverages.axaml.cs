using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Notenverwaltung.Model;
using Notenverwaltung.Resources;
using Notenverwaltung.Utils;

namespace Notenverwaltung.UI.Pages.Grade;

public partial class ShowAverages : UserControl
{
    private bool _isInitializing = true;

    public ShowAverages()
    {
        InitializeComponent();
        ApplyLocalization();
        PopulateSortComboBox();
        Loaded += OnLoaded;
    }

    private void ApplyLocalization()
    {
        TxtTitle.Text = Strings.ShowAveragesTitle;
    }

    private void PopulateSortComboBox()
    {
        CbxSort.Items.Clear();
        CbxSort.Items.Add(new ComboBoxItem { Content = Strings.SortByName, Tag = 0 });
        CbxSort.Items.Add(new ComboBoxItem { Content = Strings.SortByGradeAsc, Tag = 1 });
        CbxSort.Items.Add(new ComboBoxItem { Content = Strings.SortByGradeDesc, Tag = 2 });

        var savedSort = SettingsService.Settings.AveragesSortMode;
        if (savedSort >= 0 && savedSort < CbxSort.Items.Count)
            CbxSort.SelectedIndex = savedSort;
        else
            CbxSort.SelectedIndex = 0;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _isInitializing = false;
        RefreshAverages();
    }

    private void CbxSort_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing) return;

        if (CbxSort.SelectedItem is ComboBoxItem item && item.Tag is int sortMode)
        {
            SettingsService.Settings.AveragesSortMode = sortMode;
            SettingsService.Save();
            RefreshAverages();
        }
    }

    private void RefreshAverages()
    {
        ItemsAvgs.Items.Clear();

        // Build list of subjects with their averages
        var subjectAverages = new List<(Subject subject, double average, bool hasGrades, int finalGrade)>();

        foreach (var s in Subject.Subjects)
        {
            if (s is null) continue;

            var avg = s.CalculateAverage();
            var hasGrades = !double.IsNaN(avg);
            var finalGrade = hasGrades ? (int)Math.Round(avg, 0) : 0;

            subjectAverages.Add((s, avg, hasGrades, finalGrade));
        }

        // Sort based on selected mode
        var sortMode = CbxSort.SelectedItem is ComboBoxItem item && item.Tag is int mode ? mode : 0;

        var sorted = sortMode switch
        {
            1 => subjectAverages.OrderBy(x => x.hasGrades ? x.average : double.MaxValue).ToList(),
            2 => subjectAverages.OrderByDescending(x => x.hasGrades ? x.average : double.MinValue).ToList(),
            _ => subjectAverages.OrderBy(x => x.subject.Name).ToList()
        };

        foreach (var (subject, average, hasGrades, finalGrade) in sorted)
        {
            var card = CreateSubjectCard(subject.Name, average, hasGrades, finalGrade);
            ItemsAvgs.Items.Add(card);
        }
    }

    private Border CreateSubjectCard(string subjectName, double average, bool hasGrades, int finalGrade)
    {
        // Determine color based on grade (German grading: 1=best, 6=worst)
        var gradeColor = hasGrades ? GetGradeColor(finalGrade) : Brushes.Gray;

        // Main card container
        var card = new Border
        {
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(15),
            BorderThickness = new Thickness(0, 0, 0, 3),
            BorderBrush = gradeColor
        };

        // Use dynamic resource binding for theme-aware background
        card.Bind(Border.BackgroundProperty,
            Application.Current!.GetResourceObservable("SystemControlBackgroundChromeMediumLowBrush"));

        // Content grid
        var grid = new Grid
        {
            ColumnDefinitions = ColumnDefinitions.Parse("*,Auto")
        };

        // Left side: Subject name and average
        var leftStack = new StackPanel
        {
            Spacing = 4,
            VerticalAlignment = VerticalAlignment.Center
        };

        leftStack.Children.Add(new TextBlock
        {
            Text = subjectName,
            FontSize = 18,
            FontWeight = FontWeight.SemiBold
        });

        leftStack.Children.Add(new TextBlock
        {
            Text = hasGrades
                ? $"{Strings.Get("ShowAverages_Average")}: {average:F2}"
                : Strings.ShowAveragesNoEntries,
            FontSize = 14,
            Opacity = 0.7
        });

        Grid.SetColumn(leftStack, 0);
        grid.Children.Add(leftStack);

        // Right side: Final grade badge (always circular)
        if (hasGrades)
        {
            var gradeBadge = new Border
            {
                Background = gradeColor,
                CornerRadius = new CornerRadius(20),
                Width = 40,
                Height = 40,
                VerticalAlignment = VerticalAlignment.Center
            };

            gradeBadge.Child = new TextBlock
            {
                Text = finalGrade.ToString(),
                FontSize = 20,
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Grid.SetColumn(gradeBadge, 1);
            grid.Children.Add(gradeBadge);
        }

        card.Child = grid;
        return card;
    }

    private static ISolidColorBrush GetGradeColor(int grade)
    {
        return grade switch
        {
            1 => new SolidColorBrush(Color.FromRgb(46, 160, 67)),  // Green
            2 => new SolidColorBrush(Color.FromRgb(86, 171, 95)),  // Light green
            3 => new SolidColorBrush(Color.FromRgb(255, 193, 7)),  // Yellow
            4 => new SolidColorBrush(Color.FromRgb(255, 152, 0)),  // Orange
            5 => new SolidColorBrush(Color.FromRgb(244, 67, 54)),  // Red
            6 => new SolidColorBrush(Color.FromRgb(183, 28, 28)),  // Dark red
            _ => new SolidColorBrush(Color.FromRgb(128, 128, 128)) // Gray
        };
    }
}
