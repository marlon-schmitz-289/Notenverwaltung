using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notenverwaltung
{
    public partial class AddGrade : Page
  {
    public AddGrade()
    {
      InitializeComponent();

      brdClear.MouseLeftButtonDown += (sender, e) => ClearComboBoxes();
      brdDone.MouseLeftButtonDown += DoneButton;

      cbxSubject.Loaded += (sender, e) => cbxSubject.ItemsSource = CSVSubject.Subjects;
      cbxType.Loaded += (sender, e) => cbxType.ItemsSource = Enum.GetValues(typeof(Type));
    }


    private void DoneButton(object sender, MouseButtonEventArgs e)
    {
      Grade g = new();
      try
      {
        g.Id = Grade.GetNextID();
        g.Subject = cbxSubject.SelectedItem as Subject;
        g.Rating = Int32.Parse((cbxRating.SelectedItem as ComboBoxItem).Content.ToString());
        g.TypeG = (Type)cbxType.SelectedItem;
        g.Save();

        ClearComboBoxes();
        MessageDialog md = new("Note erfolgreich gespeichert", Application.Current.MainWindow);
        md.ShowDialog();
      }
      catch (InvalidOperationException)
      {
        MessageDialog dlg = new("Beim Speichern ist ein Fehler Aufgetreten!", Application.Current.MainWindow);
        dlg.ShowDialog();
      }
      catch (Exception)
      {
        MessageDialog dlg = new("Ein Fehler im Programm ist aufgetreten!", Application.Current.MainWindow);
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
