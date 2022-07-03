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


    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      foreach (Subject s in CSVSubject.Subjects)
      {
        String st = s.CalculateAverage() is not double.NaN ? s.CalculateAverage().ToString() : "keine Einträge vorhanden";
        lbxAvgs.Items.Add(new Label()
        {
          Content = $"{s.Name,15}: {st}",
          FontSize = 18
        });
      }
    }
  }
}
