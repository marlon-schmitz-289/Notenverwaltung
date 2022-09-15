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
      int highest = GetBiggestSubjectLength();
      foreach (Subject s in CSVSubject.Subjects)
      {
        if (s is not null)
        {
          var st = s.CalculateAverage() is not double.NaN ? s.CalculateAverage().ToString() : "keine Einträge vorhanden";
          lbxAvgs.Items.Add(new Label()
          {
            Content = $"{s.Name.PadLeft(highest)} : {st}",
            HorizontalContentAlignment = HorizontalAlignment.Center,
            FontSize = 18
          });
        }
      }
    }


    public static int GetBiggestSubjectLength()
    {
      int biggest = 0;

      foreach (var s in Subject.Subjects)
        if (s is not null)
          biggest = s.Name.Length > biggest ? s.Name.Length : biggest;

      return biggest;
    }


    private void LbxAvgs_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      String[] curr = (lbxAvgs.SelectedItem as Label).Content.ToString().Split(':');
      MainWindow.UpdateClient($"{curr[0]}",$"{curr[1]}");
    }
  }
}
