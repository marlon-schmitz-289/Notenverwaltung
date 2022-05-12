using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private CurrentPage _currPage = CurrentPage.showAll;
    public User CurrentUser { get; set; }
#if DEBUG
    public String User
    {
      get => lblUserName.Content.ToString();
      set => lblUserName.Content = value;
    }
#endif

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
#if DEBUG
      CurrentUser = new User("admin");
      User = CurrentUser.Username;
#elif RELEASE
      CurrentUser = new User();
      this.IsEnabled = false;
      Login loginDlg = new Login(CurrentUser);
      loginDlg.Owner = this;
      loginDlg.ShowDialog();

      try
      {
        MessageDialog dlg = new MessageDialog($"Willkommen {CurrentUser.Username}");
        dlg.Owner = this;
        dlg.ShowDialog();
        this.IsEnabled = true;
      }
      catch
      {

      }
#endif

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
          this.frame.Content = new Label() { Content = "Durchschnitte anzeigen" };
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
