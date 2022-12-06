using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using Notenverwaltung.Persistenz;

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
                DragMove();
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
            brdSchliessen.MouseLeftButtonDown += (sender, e) =>
            {
                DiscordClient.UpdateClient("Beendet das Programm", "Hat wohl doch keinen Bock mehr");

                Grade.SaveAll();
                Subject.SaveAll();

                DiscordClient.Deinitialize();

                Application.Current.Shutdown();
            };


            brdMinimieren.MouseLeftButtonDown += (sender, e) =>
              WindowState = WindowState.Minimized;


            Subject.ReadAll();
            Grade.ReadAll();


            if (CSVSubject.Subjects.Count == 0)
                new FirstTimeSubjectsDialog() { Owner = this }.ShowDialog();


            DiscordClient.Initialize();


            System.Timers.Timer timer = new(150);
            timer.Elapsed += (sender, args) => { DiscordClient.Client.Invoke(); };
            timer.Start();


            DiscordClient.UpdateClient("Hauptmenü", "Macht gerade absolut nichts");


#if DEBUG
            //new MessageDialog(text:$@"Test", owner:this).ShowDialog();

            //var s = new Subject("test", false);
            //new SubEditDlg(s) { Owner = this }.ShowDialog();
#endif
        }


        private void SwitchPage()
        {
            switch (_currPage)
            {
                case CurrentPage.showAll:
                {
                    frame.Content = new ShowGrades();
                    DiscordClient.UpdateClient("Übersicht der Noten aller Fächer", "schaut Noteneinträge an");
                    break;
                }
                case CurrentPage.showAvgs:
                {
                    frame.Content = new ShowAverages();
                    DiscordClient.UpdateClient("Übersicht der Notendurchschnitte", "schaut schlechte Durchschnitte an");
                    break;
                }
                case CurrentPage.newEntry:
                {
                    frame.Content = new AddGrade();
                    DiscordClient.UpdateClient("Hinzufügen eines Noteneintrags", "fügt neue schlechte Note hinzu");
                    break;
                }
                case CurrentPage.editEntry:
                {
                    frame.Content = new EditGrades();
                    DiscordClient.UpdateClient("Bearbeiten der Noteneinträge", "verfälscht Noteneinträge");
                    break;
                }
                case CurrentPage.editSubs:
                {
                    frame.Content = new EditSubs();
                    DiscordClient.UpdateClient("Bearbeiten der eingetragenen Fächer", "pfuscht am Stundenplan");
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

            new MessageDialog(text: "Bitte warten, bis die Daten geladen wurden", owner: this).ShowDialog();
        }


        private void BrdEditSubs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _currPage = CurrentPage.editSubs;
            SwitchPage();
        }
    }
}
