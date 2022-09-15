using System.Windows;
using System.Windows.Input;

namespace Notenverwaltung
{
  public partial class GradeEditDlg : Window
  {
    private Subject CurrSub { get; set; }
    private Subject ChangedSub { get; set; }


    public GradeEditDlg(Subject sub)
    {
      InitializeComponent();
      CurrSub = sub;
      ChangedSub = new(sub.Name, true);
    }


    private void SaveSub(object sender, MouseButtonEventArgs e)
    {
      if (!Subject.Subjects.Contains(CurrSub!))
        Subject.Subjects.Add(CurrSub);
      else
        Subject.Subjects[Subject.Subjects.IndexOf(CurrSub!)] = ChangedSub!;
    }
  }
}
