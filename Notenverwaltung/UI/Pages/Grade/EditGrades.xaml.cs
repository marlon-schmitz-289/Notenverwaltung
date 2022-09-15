using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaction logic for EditGrades.xaml
  /// </summary>
  public partial class EditGrades : Page
  {
    public EditGrades()
    {
      InitializeComponent();
    }


    public void Page_Loaded(Object sender, RoutedEventArgs e)
    {
      FillListBoxes();
    }


    private Grade GetSelectedValues()
    {
      Grade tmp = lbxGrades.SelectedItem as Grade;

      tmp.Subject = lbxSubject.SelectedItem as Subject;
      tmp.Rating = (Int32)lbxRating.SelectedItem;
      tmp.TypeG = (Type)((Int32)lbxType.SelectedItem);

      return tmp;
    }


    private void SetSelectedValues()
    {
      var tmp = lbxGrades.SelectedItem as Grade;
      if (tmp is not null)
      {
        lbxSubject.SelectedItem = tmp.Subject;
        lbxRating.SelectedItem = tmp.Rating;
        lbxType.SelectedItem = tmp.TypeG;
        return;
      }
      lbxSubject.SelectedItem = null;
      lbxRating.SelectedItem = null;
      lbxType.SelectedItem = null;
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
      lbxType.Items.Clear();

      foreach (Type t in Enum.GetValues(typeof(Type)))
        lbxType.Items.Add(t);
    }


    private void FillSubjects()
    {
      lbxSubject.Items.Clear();

      foreach (Subject s in CSVSubject.Subjects)
        lbxSubject.Items.Add(s);
    }


    private void FillRating()
    {
      lbxRating.Items.Clear();

      for (int i = 1; i < 7; i++)
        lbxRating.Items.Add(i);
    }


    private void FillGrades()
    {
      lbxGrades.Items.Clear();

      foreach (Grade g in CSVGrade.Grades)
        lbxGrades.Items.Add(g);
    }


    private void btnSafe_Click(object sender, RoutedEventArgs e)
    {
      if (lbxGrades.SelectedIndex is not -1)
      {
        Grade tmp = GetSelectedValues();
        tmp.Update();

        Grade.ReadAll();
        FillGrades();
      }
    }


    private void lbxGrades_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      SetSelectedValues();
    }
  }
}
