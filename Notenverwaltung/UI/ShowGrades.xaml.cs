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
  /// Interaction logic for ShowGrades.xaml
  /// </summary>
  public partial class ShowGrades : Page
  {
    public User CurrUser { get; set; }

    
    public ShowGrades(User curr)
    {
      InitializeComponent();
      CurrUser = curr;
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
      List<Subject> subjects = Subject.ReadAll();

      foreach (Subject s in subjects) cbxSubjects.Items.Add(s);
    }


    private void FillGrades()
    {
      lbxGrades.Items.Clear();
      List<Grade> grades = Grade.ReadAll(CurrUser.Username);

      foreach(Grade g in grades)
        if (cbxSubjects.SelectedItem != null && g.Subject.Id == (cbxSubjects.SelectedItem as Grade).Id)
          lbxGrades.Items.Add(g);
    }

    
    private void cbxSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      FillGrades();
    }
  }
}
