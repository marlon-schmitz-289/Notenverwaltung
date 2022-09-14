using System;
using System.Windows;
using System.Windows.Controls;

namespace Notenverwaltung
{
  public partial class ShowAverages : Page
  {
    public ShowAverages()
    {
      InitializeComponent();
    }


    private int GetBiggestSubjectLength()
    {
      int biggest = 0;

      foreach (Subject s in Subject.Subjects)
      {

      }

      return biggest;
    }


    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      int highest = GetBiggestSubjectLength();
      foreach (Subject s in CSVSubject.Subjects)
      {
        String st = s.CalculateAverage() is not double.NaN ? s.CalculateAverage().ToString() : "keine Einträge vorhanden";
        lbxAvgs.Items.Add(new Label()
        {
          Content = $"{s.Name.PadLeft(highest)}: {st}",
          FontSize = 18
        });
      }
    }
  }
}
