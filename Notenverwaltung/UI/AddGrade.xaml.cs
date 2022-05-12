using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaktionslogik für AddGrade.xaml
  /// </summary>
  public partial class AddGrade : Page
  {
    public User CurUser { get; set; }

    public AddGrade(User u)
    {
      InitializeComponent();
      CurUser = u;

      brdClear.MouseLeftButtonDown += (sender, e) => ClearComboBoxes();
      brdDone.MouseLeftButtonDown += DoneButton;

      cbxSubject.Loaded += (sender, e) => cbxSubject.ItemsSource = Subject.ReadAll();
      cbxType.Loaded += (sender, e) => cbxType.ItemsSource = Enum.GetValues(typeof(Type));
    }


    private void DoneButton(object sender, MouseButtonEventArgs e)
    {
      Grade g = new Grade();
      try
      {
        g.Subject = cbxSubject.SelectedItem as Subject;
        g.Rating = Int32.Parse((cbxRating.SelectedItem as ComboBoxItem).Content.ToString());
        g.TypeG = (Type)cbxType.SelectedItem;
        g.User = CurUser;
        g.Save();
      }
      catch(InvalidOperationException)
      {
        MessageDialog dlg = new MessageDialog("Beim Speichern ist ein Fehler Aufgetreten!");
        dlg.Owner = Application.Current.MainWindow;
        dlg.ShowDialog();
      }
      catch(Exception)
      {
        MessageDialog dlg = new MessageDialog("Ein Fehler im Programm ist aufgetreten!");
        dlg.Owner = Application.Current.MainWindow;
        dlg.ShowDialog();
      }
    }


    private void ClearComboBoxes()
    {
      cbxSubject.SelectedIndex = -1;
      cbxRating.SelectedIndex = -1;
      cbxType.SelectedIndex = -1;
    }
  }
}
