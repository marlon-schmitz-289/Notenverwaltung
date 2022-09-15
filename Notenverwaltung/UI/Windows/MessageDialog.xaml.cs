using System;
using System.Windows;
using System.Windows.Input;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaction logic for MessageDialog.xaml
  /// </summary>
  public partial class MessageDialog : Window
  {
    public String Text
    {
      get => tbxText.Text;
      set => tbxText.Text = value;
    }


    public MessageDialog(String text, Window owner)
    {
      InitializeComponent();

      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      Owner = owner;
      Text = text;

      this.MouseLeftButtonDown += (sender, e) => DragMove();
      brdOK.MouseLeftButtonDown += (sender, e) => this.Close();
    }


    private void Window_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) this.Close(); }
  }
}
