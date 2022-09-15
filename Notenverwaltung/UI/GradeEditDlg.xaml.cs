using System.Windows;
using System.Windows.Input;

namespace Notenverwaltung
{
  public partial class GradeEditDlg : Window
  {
    public Subject CurrSub { get; set; }


    public GradeEditDlg(Subject sub)
    {
      InitializeComponent();
      CurrSub = sub;
    }


    private void SaveSub(object sender, MouseButtonEventArgs e)
    {

    }
  }
}
