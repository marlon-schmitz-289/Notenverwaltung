using System.Windows;
using System.Windows.Input;

namespace Notenverwaltung
{
  public partial class SubEditDlg : Window
  {
    private Subject CurrSub { get; set; } = null;
    private Subject ChangedSub { get; set; } = null;


    public SubEditDlg(Subject sub)
    {
      InitializeComponent();

      CurrSub = new(sub?.Name, true);

      WindowStartupLocation = WindowStartupLocation.CenterOwner;

      MainWindow.UpdateClient(
        sub is not null ? "Bearbeitung der Fächerliste" : "Hinzufügen eines Fachs",
        sub is not null ? "Pfuscht am Stundenplan mies rum" : "Kek hat ein Fach vergessen KEKW"
      );
    }


    private void SaveSub(object sender, MouseButtonEventArgs e)
    {
      ChangedSub = new(tbxSubName.Text, true);

      if (!Subject.Subjects.Contains(CurrSub!))
        Subject.Subjects.Add(ChangedSub);
      else
        Subject.Subjects[Subject.Subjects.IndexOf(CurrSub!)] = ChangedSub!;

      this.Close();
    }
  }
}
