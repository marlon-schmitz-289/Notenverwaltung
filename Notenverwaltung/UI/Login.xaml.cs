using System;
using System.Windows;
using System.Windows.Input;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaktionslogik für Login.xaml
  /// </summary>
  public partial class Login : Window
  {
    public User CurrentUser { get; set; }


    public Login(User cur)
    {
      InitializeComponent();
      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      CurrentUser = cur;
    }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      this.LostFocus += (sender, e) => this.Focus();

      brdClose.MouseLeftButtonDown += (sender, e) => Application.Current.Shutdown();
      brdLogin.MouseLeftButtonDown += BrdLogin_MouseLeftButtonDown;

      tbxUsername.GotFocus += (sender, e) => tbxUsername.SelectAll();
      tbxUsername.MouseEnter += (sender, e) => tbxUsername.SelectAll();
      tbxUsername.GotKeyboardFocus += (sender, r) => tbxUsername.SelectAll();
      
      pwbPassword.GotFocus += (sender, e) => pwbPassword.SelectAll();
      pwbPassword.MouseEnter += (sender, e) => pwbPassword.SelectAll();
      pwbPassword.GotKeyboardFocus += (sender, r) => pwbPassword.SelectAll();
    }


    private void BrdLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      UserLogin();
    }


    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        UserLogin();
      }
    }


    private void UserLogin()
    {
      try
      {
        User u = new User(tbxUsername.Text.ToLower());

        if ((u.Password != null || !u.Password.Equals("")) && u.Password.Equals(pwbPassword.Password))
        {
          CurrentUser.Username = u.Username;
          CurrentUser.Password = u.Password;

          this.Close();
        }
      }
      catch (Exception)
      {
        MessageBox.Show("Falsche Login-Daten!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}
