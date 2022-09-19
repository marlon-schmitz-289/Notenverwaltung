using System.Collections.Generic;
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


    private void BrdDel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (lbxSubs.SelectedItem is not Subject tmp || !Subject.Subjects.Contains(tmp)) return;

      var tmps = new List<Grade>();

      foreach (var g in Grade.Grades)
        if (g.Subject.Equals(tmp))
          tmps.Add(g);

      foreach (var g in tmps)
        g.Delete();

      tmp.Delete();

      FillListBox();
    }


    private void BrdAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      new SubEditDlg(null) { Owner = Application.Current.MainWindow }.ShowDialog();
      FillListBox();
    }
  }
}
