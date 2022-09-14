using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using DiscordRPC.Logging;
using DiscordRPC;

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


    private void BrdSchliessen_MouseEnter(object sender, MouseEventArgs e)
    {
      brdSchliessen.Background = Brushes.Red;
    }


    private void BrdSchliessen_MouseLeave(object sender, MouseEventArgs e)
    {
      brdSchliessen.Background = new SolidColorBrush() { Color = Color.FromArgb(255, 197, 37, 37) };
    }


    private void BrdMinimieren_MouseEnter(object sender, MouseEventArgs e)
    {
      brdMinimieren.Background = new SolidColorBrush() { Color = Color.FromArgb(255, 169, 150, 150) };
    }


    private void BrdMinimieren_MouseLeave(object sender, MouseEventArgs e)
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

      Initialize();

      var timer = new System.Timers.Timer(150);
      timer.Elapsed += (sender, args) => { Client.Invoke(); };
      timer.Start();
    }


    private void SwitchPage()
    {
      switch (_currPage)
      {
        case CurrentPage.showAll:
        {
          this.frame.Content = new ShowGrades();
          UpdateClient("Übersicht der Noten aller Fächer", "schaut Noteneinträge an");
          break;
        }
        case CurrentPage.showAvgs:
        {
          this.frame.Content = new ShowAverages();
          UpdateClient("Übersicht der Notendurchschnitte", "schaut schlechte Durchschnitte an");
          break;
        }
        case CurrentPage.newEntry:
        {
          this.frame.Content = new AddGrade();
          UpdateClient("Hinzufügen eines Noteneintrags", "fügt neue schlechte Note hinzu");
          break;
        }
        case CurrentPage.editEntry:
        {
          this.frame.Content = new EditGrades();
          UpdateClient("Bearbeiten der Noteneinträge", "verfälscht Noteneinträge");
          break;
        }
        case CurrentPage.editSubs:
        {
          this.frame.Content = new EditSubs();
          UpdateClient("Bearbeiten der eingetragenen Fächer", "pfuscht am Stundenplan");
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
      Deinitialize();
    }


    #region Discord Rich Presence
    public static DiscordRpcClient Client { get; set; }

    //Called when your application first starts.
    //For example, just before your main loop, on OnEnable for unity.
    private void Initialize()
    {
      /*
      Create a Discord client
      NOTE: 	If you are using Unity3D, you must use the full constructor and define
           the pipe connection.
      */
      Client = new("1019555860135026758");

      //Set the logger
      Client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

      //Subscribe to events
      Client.OnReady += (sender, e) =>
      {
        Console.WriteLine("Received Ready from user {0}", e.User.Username);
      };

      Client.OnPresenceUpdate += (sender, e) =>
      {
        Console.WriteLine("Received Update! {0}", e.Presence);
      };

      //Connect to the RPC
      Client.Initialize();

      //Set the rich presence
      //Call this as many times as you want and anywhere in your code.
      Client.SetPresence(new RichPresence()
      {
        Details = "Hauptmenü",
        State = "macht gerade nichts",
        Assets = new Assets()
        {
          LargeImageKey = "image_large",
          LargeImageText = "Penis",
          SmallImageKey = "image_small"
        }
      });
    }


    public static void UpdateClient(String details, String state)
    {
      Client.SetPresence(new RichPresence()
      {
        Details = details,
        State = state,
        Assets = new Assets()
        {
          LargeImageKey = "image_large",
          LargeImageText = "sapnu puas",
          SmallImageKey = "image_small"
        }
      });
    }


    //Called when your application terminates.
    //For example, just after your main loop, on OnDisable for unity.
    void Deinitialize()
    {
      Client.Dispose();
    }
    #endregion
  }
}
