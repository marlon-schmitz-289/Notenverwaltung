using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Google.Protobuf;

namespace Notenverwaltung
{
  public partial class MainWindow : Window
  {
    private CurrentPage _currPage = CurrentPage.showAll;

    public MainWindow()
    {
      InitializeComponent();
      WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }


    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton.Equals(MouseButton.Left))
        this.DragMove();
    }


    private void brdSchliessen_MouseEnter(object sender, MouseEventArgs e)
    {
      brdSchliessen.Background = Brushes.Red;
    }


    private void brdSchliessen_MouseLeave(object sender, MouseEventArgs e)
    {
      brdSchliessen.Background = new SolidColorBrush() { Color = Color.FromArgb(255, 197, 37, 37) };
    }


    private void brdMinimieren_MouseEnter(object sender, MouseEventArgs e)
    {
      brdMinimieren.Background = new SolidColorBrush() { Color = Color.FromArgb(255, 169, 150, 150) };
    }


    private void brdMinimieren_MouseLeave(object sender, MouseEventArgs e)
    {
      brdMinimieren.Background = new SolidColorBrush() { Color = Color.FromArgb(255, 134, 124, 124) };
    }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      brdSchliessen.MouseLeftButtonDown += (sender, e) => Application.Current.Shutdown();
      brdMinimieren.MouseLeftButtonDown += (sender, e) => this.WindowState = WindowState.Minimized;

#if DEBUG
      //new MessageDialog(text:$@"Test", owner:this).ShowDialog();
      this.Topmost = true;
#endif

      Subject.ReadAll();
      Grade.ReadAll();

      if (CSVSubject.Subjects.Count == 0)
        new FirstTimeSubjectsDialog() { Owner = this }.ShowDialog();
    }


    private void SwitchPage()
    {
      switch (_currPage)
      {
        case CurrentPage.showAll:
        {
          this.frame.Content = new ShowGrades();
          break;
        }
        case CurrentPage.showAvgs:
        {
          this.frame.Content = new ShowAverages();
          break;
        }
        case CurrentPage.newEntry:
        {
          this.frame.Content = new AddGrade();
          break;
        }
        case CurrentPage.editEntry:
        {
          this.frame.Content = new EditGrades();
          break;
        }
        case CurrentPage.editSubs:
        {
          this.frame.Content = new EditSubs();
          break;
        }
      }
    }


    private void BrdAddGrade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.newEntry;
      SwitchPage();
    }


    private void BrdEditGrade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.editEntry;
      SwitchPage();
    }


    private void BrdListAvgs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.showAvgs;
      SwitchPage();
    }


    private void BrdListAll_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (CSVGrade.Grades != null)
      {
        _currPage = CurrentPage.showAll;
        SwitchPage();
        return;
      }

      new MessageDialog(text:"Bitte warten, bis die Daten geladen wurden", owner:this).ShowDialog();
    }


    private void BrdEditSubs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.editSubs;
      SwitchPage();
      return;
    }


    private void Window_Closing(object sender, CancelEventArgs e)
    {
      Grade.SaveAll();
    }
  }
}
