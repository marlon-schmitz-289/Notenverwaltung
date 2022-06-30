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
        lbxAvgs.Items.Add(new Label()
        {
          Content = $"{s.Name}: {s.CalculateAverage()}",
        });
      }
    }
  }
}
