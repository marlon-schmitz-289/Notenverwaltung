using System.Windows;
using System.Windows.Controls;

namespace Notenverwaltung
{
  public partial class ShowGrades : Page
  {
    public ShowGrades()
    {
      InitializeComponent();
    }


    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      FillListBox();
    }


    private void FillListBox()
    {
      FillSubjects();
      FillGrades();
    }


    private void FillSubjects()
    {
      cbxSubjects.Items.Clear();

      foreach (Subject s in CSVSubject.Subjects)
        cbxSubjects.Items.Add(s);
    }


    private void FillGrades()
    {
      lbxGrades.Items.Clear();

      if (cbxSubjects.SelectedItem is not null)
      {
        Subject s = cbxSubjects.SelectedItem as Subject;

        foreach (Grade g in CSVGrade.Grades)
          if (g.Subject.ToSaveableString().Equals(s.ToSaveableString()))
            lbxGrades.Items.Add(g);
      }
    }


    private void CbxSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      FillGrades();
    }
  }
}
