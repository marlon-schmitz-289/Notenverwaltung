using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
      cbxSubjects.SelectedIndex = 0;
    }


    private void FillListBox()
    {
      FillSubjects();
      FillGrades();
    }


    private void FillSubjects()
    {
      cbxSubjects.Items.Clear();

      cbxSubjects.Items.Add("alle Fächer");

      foreach (Subject s in CSVSubject.Subjects)
        cbxSubjects.Items.Add(s);
    }


    private void FillGrades()
    {
      lbxGrades.Items.Clear();

      if (cbxSubjects.SelectedItem is not null)
      {
        if (cbxSubjects.SelectedItem.GetType() == typeof(Subject))
        {
          Subject s = cbxSubjects.SelectedItem as Subject;

          foreach (Grade g in CSVGrade.Grades)
            if (g.Subject.ToSaveableString().Equals(s.ToSaveableString()))
              lbxGrades.Items.Add(g);

          goto end;
        }

        foreach (Grade g in CSVGrade.Grades)
            lbxGrades.Items.Add(g);

        goto end;
      }

      end:;
    }


    private void CbxSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      FillGrades();
    }


    private void BrdTrash_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (lbxGrades.SelectedItem is not null)
      {
        ((Grade)lbxGrades.SelectedItem).Delete();
        lbxGrades.Items.Remove(lbxGrades.SelectedItem);
      }
    }


    private void BrdTrash_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
      brdTrash.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF5454");
    }


    private void BrdTrash_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
      brdTrash.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
    }


    private void LbxGrades_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var g = lbxGrades.SelectedItem as Grade;
      MainWindow.UpdateClient($"{g.Subject}, {g.TypeG}",$"{g.Rating}");
    }
  }
}
