using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Notenverwaltung
{
  public partial class ShowAverages : Page
  {
    private List<Grade> Grades { get; set; }

    
    public ShowAverages(List<Grade> grades)
    {
      InitializeComponent();
      Grades = grades;
    }

    
    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      foreach(Subject s in Subject.ReadAll())
      {
        lbxAvgs.Items.Add(new Label() {
          Content = $"{s.Name}: {s.CalculateAverage(Grades)}",
        });
      }
    }
  }
}
