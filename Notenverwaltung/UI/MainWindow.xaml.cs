using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private CurrentPage _currPage = CurrentPage.showAll;
    public List<Grade> grades { get; private set; }
    public User CurrentUser { get; set; }

    public MainWindow()
    {
      InitializeComponent();
      WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }


    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      CurrentUser = new();
      this.IsEnabled = false;
      Login loginDlg = new(CurrentUser);
      loginDlg.Owner = this;
      loginDlg.ShowDialog();

      grades = Grade.ReadAll(CurrentUser.Username);

      MessageDialog dlg = new($"Willkommen {CurrentUser.Username}");
      dlg.Owner = this;
      dlg.ShowDialog();
      this.IsEnabled = true;

      brdSchliessen.MouseLeftButtonDown += (sender, e) => Application.Current.Shutdown();
      brdMinimieren.MouseLeftButtonDown += (sender, e) => this.WindowState = WindowState.Minimized;
    }


    private void SwitchPage()
    {
      switch (_currPage)
      {
        case CurrentPage.showAll:
          this.frame.Content = new ShowGrades(CurrentUser);
          break;
        case CurrentPage.showAvgs:
          this.frame.Content = new ShowAverages(CurrentUser);
          break;
        case CurrentPage.newEntry:
          this.frame.Content = new AddGrade(CurrentUser);
          break;
        case CurrentPage.editEntry:
          this.frame.Content = new Label() { Content = "Aktuell ausgewählten bearbeiten" };
          break;
      }
    }


    private void brdAddGrade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.newEntry;
      SwitchPage();
    }


    private void brdEditGrade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.editEntry;
      SwitchPage();
    }


    private void brdListAvgs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.showAvgs;
      SwitchPage();
    }


    private void brdListAll_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.showAll;
      SwitchPage();
    }
  }
}
