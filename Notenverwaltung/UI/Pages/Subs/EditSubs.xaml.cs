using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notenverwaltung
{
  public partial class EditSubs : Page
  {
    public EditSubs()
    {
      InitializeComponent();
    }


    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      FillListBox();
    }


    private void LbxSubs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      new SubEditDlg(lbxSubs.SelectedItem as Subject) { Owner = Application.Current.MainWindow }.ShowDialog();
      FillListBox();
    }


    private void FillListBox()
    {
      lbxSubs.Items.Clear();

      foreach(var s in Subject.Subjects)
        lbxSubs.Items.Add(s);
    }
  }
}
