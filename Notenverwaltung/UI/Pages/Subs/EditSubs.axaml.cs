using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Notenverwaltung.Model;
using Notenverwaltung.Resources;

namespace Notenverwaltung.UI.Pages.Subs;

public partial class EditSubs : UserControl
{
    private Subject? _selectedSubject;
    private Subject? _editingSubject; // null = adding new, non-null = editing existing

    public EditSubs()
    {
        InitializeComponent();
        ApplyLocalization();
        Loaded += OnLoaded;
    }

    private void ApplyLocalization()
    {
        TxtTitle.Text = Strings.EditSubsTitle;
        ToolTip.SetTip(BtnAdd, Strings.EditSubsAdd);
        ToolTip.SetTip(BtnDelete, Strings.EditSubsDelete);
        ToolTip.SetTip(BtnSave, Strings.SubEditDlgSave);
        TbxSubjectName.Watermark = Strings.FirstTimeSubjectNamePlaceholder;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        FillSubjects();
    }

    private void FillSubjects()
    {
        SubjectsItems.Items.Clear();
        _selectedSubject = null;
        BtnDelete.IsEnabled = false;

        foreach (var s in Subject.Subjects)
            SubjectsItems.Items.Add(CreateSubjectCard(s));
    }

    private Border CreateSubjectCard(Subject subject)
    {
        var card = new Border
        {
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(15),
            BorderThickness = new Thickness(0, 0, 0, 3),
            BorderBrush = new SolidColorBrush(Color.FromRgb(100, 100, 100)),
            Cursor = new Cursor(StandardCursorType.Hand),
            Tag = subject
        };

        card.Bind(Border.BackgroundProperty,
            Application.Current!.GetResourceObservable("SystemControlBackgroundChromeMediumLowBrush"));

        card.PointerPressed += SubjectCard_PointerPressed;
        card.DoubleTapped += SubjectCard_DoubleTapped;

        var textBlock = new TextBlock
        {
            Text = subject.Name,
            FontSize = 16,
            FontWeight = FontWeight.SemiBold,
            VerticalAlignment = VerticalAlignment.Center
        };

        card.Child = textBlock;
        return card;
    }

    private void SubjectCard_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border { Tag: Subject subject } card)
        {
            // If clicking the same card, deselect it
            if (_selectedSubject == subject)
            {
                card.BorderThickness = new Thickness(0, 0, 0, 3);
                _selectedSubject = null;
                BtnDelete.IsEnabled = false;
                return;
            }

            // Deselect previous
            foreach (var item in SubjectsItems.Items)
            {
                if (item is Border b)
                    b.BorderThickness = new Thickness(0, 0, 0, 3);
            }

            // Select current
            card.BorderThickness = new Thickness(3);
            _selectedSubject = subject;
            BtnDelete.IsEnabled = true;
        }
    }

    private void SubjectCard_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (sender is Border { Tag: Subject subject })
        {
            // Show inline edit panel for editing
            _editingSubject = subject;
            TbxSubjectName.Text = subject.Name;
            EditPanel.IsVisible = true;
            UpdateSaveButtonState();
            TbxSubjectName.Focus();
        }
    }

    private void BtnDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (_selectedSubject == null || !Subject.Subjects.Contains(_selectedSubject)) return;

        var tmps = new List<Model.Grade>();

        foreach (var g in Model.Grade.Grades)
            if (g.Subject.Equals(_selectedSubject))
                tmps.Add(g);

        foreach (var g in tmps)
            g.Delete();

        _selectedSubject.Delete();
        FillSubjects();
    }

    private void BtnAdd_Click(object? sender, RoutedEventArgs e)
    {
        // Show inline edit panel for adding new
        _editingSubject = null;
        TbxSubjectName.Text = "";
        EditPanel.IsVisible = true;
        UpdateSaveButtonState();
        TbxSubjectName.Focus();
    }

    private void TbxSubjectName_TextChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateSaveButtonState();
    }

    private void UpdateSaveButtonState()
    {
        BtnSave.IsEnabled = !string.IsNullOrWhiteSpace(TbxSubjectName.Text);
    }

    private void BtnSave_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TbxSubjectName.Text))
        {
            EditPanel.IsVisible = false;
            return;
        }

        var newSubject = new Subject(TbxSubjectName.Text, true);

        if (_editingSubject == null)
        {
            // Adding new subject
            Subject.Subjects.Add(newSubject);
        }
        else
        {
            // Editing existing subject
            var index = Subject.Subjects.IndexOf(_editingSubject);
            if (index >= 0)
            {
                Subject.Subjects[index] = newSubject;

                // Update all grades with this subject
                foreach (var g in Model.Grade.Grades)
                {
                    if (g.Subject.Name.Equals(_editingSubject.Name))
                        g.Subject = newSubject;
                }
            }
        }

        Subject.SaveAll();
        EditPanel.IsVisible = false;
        _editingSubject = null;
        FillSubjects();
    }

    private void BtnCancel_Click(object? sender, RoutedEventArgs e)
    {
        EditPanel.IsVisible = false;
        _editingSubject = null;
    }
}
