using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaction logic for FirstTimeSubjectsDialog.xaml
  /// </summary>
  public partial class FirstTimeSubjectsDialog : Window
  {
    public FirstTimeSubjectsDialog()
    {
      InitializeComponent();
      WindowStartupLocation = WindowStartupLocation.CenterOwner;
    }


    private void brdSchliessen_MouseDown(object sender, MouseButtonEventArgs e)
    {
      foreach (Subject s in lbxGrades.Items)
        Subject.Subjects.Add(s);

      Subject.SaveAll();
      this.Close();
    }


    private void brdSchliessen_MouseEnter(object sender, MouseEventArgs e)
    {
      brdSchliessen.Background = Brushes.Red;
    }


    private void brdSchliessen_MouseLeave(object sender, MouseEventArgs e)
    {
      brdSchliessen.Background = new SolidColorBrush() { Color = Color.FromArgb(255, 197, 37, 37) };
    }


    private void btnSav_Click(object sender, RoutedEventArgs e)
    {
      lbxGrades.Items.Add(new Subject(tbxName.Text, true));
      tbxName.Text = "";
    }


    private void btnDel_Click(object sender, RoutedEventArgs e)
    {
      lbxGrades.Items.Remove(lbxGrades.SelectedItem);
    }


    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
      try
      {
        this.DragMove();
      }
      catch
      {

      }
    }
  }
}
