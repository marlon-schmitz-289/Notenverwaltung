using System.Windows;
using System.Windows.Input;

namespace Notenverwaltung
{
  public partial class GradeEditDlg : Window
  {
    private Subject CurrSub { get; set; }
    private Subject ChangedSub { get; set; } = null;


    public GradeEditDlg(Subject sub)
    {
      InitializeComponent();

      CurrSub = new(sub?.Name, true);
      ChangedSub = new(CurrSub?.Name, true);

      WindowStartupLocation = WindowStartupLocation.CenterOwner;

      MainWindow.UpdateClient(
        sub is not null ? "Bearbeitung der Fächerliste" : "Hinzufügen eines Fachs",
        sub is not null ? "Pfuscht am Stundenplan mies rum" : "Kek hat ein Fach vergessen KEKW"
      );
    }


    private void SaveSub(object sender, MouseButtonEventArgs e)
    {

      if (!Subject.Subjects.Contains(CurrSub!))
      {
        Subject.Subjects.Add(ChangedSub);
      }
      else
      {
        ChangedSub = new(tbxSubName.Text, true);
        Subject.Subjects[Subject.Subjects.IndexOf(CurrSub!)] = ChangedSub!;
      }

      this.Close();
    }
  }
}
