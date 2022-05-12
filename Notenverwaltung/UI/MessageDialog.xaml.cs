using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaction logic for MessageDialog.xaml
  /// </summary>
  public partial class MessageDialog : Window
  {
    public String Text
    {
      get
      {
        return tbxText.Text;
      }
      set
      {
        tbxText.Text = value;
      }
    }


    public MessageDialog(String text)
    {
      InitializeComponent();

      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      Text = text;

      brdOK.MouseLeftButtonDown += (sender, e) => this.Close();
    }
  }
}
