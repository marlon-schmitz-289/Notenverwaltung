using System;
using System.Windows;
using System.Windows.Input;

namespace Notenverwaltung
{
  public partial class SubEditDlg : Window
  {
    private Subject CurrSub { get; set; } = null;
    private Subject ChangedSub { get; set; } = null;
    private Boolean _editSub = false;


    public SubEditDlg(Subject sub)
    {
      InitializeComponent();

      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      CurrSub = sub;
    }


    private void SaveSub(object sender, MouseButtonEventArgs e)
    {
      ChangedSub = new(tbxSubName.Text, true);

      if (ChangedSub.Name.Equals("")) return;

      if (!Subject.Subjects.Contains(CurrSub!))
        Subject.Subjects.Add(ChangedSub);
      else
        Subject.Subjects[Subject.Subjects.IndexOf(CurrSub!)] = ChangedSub!;

      ChangeAllGradesSub();

      this.Close();
    }


    private void ChangeAllGradesSub()
    {
      if (_editSub)
        foreach(var g in Grade.Grades)
          g.Subject = g.Subject.Name.Equals(CurrSub.Name) ? ChangedSub : g.Subject;
    }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      _editSub = CurrSub is not null;

      DiscordClient.UpdateClient(
        _editSub ? "Bearbeitung der Fächerliste" : "Hinzufügen eines Fachs",
        _editSub ? "Pfuscht am Stundenplan mies rum" : "Kek hat ein Fach vergessen KEKW"
      );

      tbxSubName.Text = _editSub ? CurrSub.Name : "";
    }
  }
}
