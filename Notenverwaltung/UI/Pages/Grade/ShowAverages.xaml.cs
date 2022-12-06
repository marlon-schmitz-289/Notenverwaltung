using System;
using System.Windows;
using System.Windows.Controls;

using Notenverwaltung.Persistenz;

namespace Notenverwaltung
{
    public partial class ShowAverages : Page
    {
        public ShowAverages()
        {
            InitializeComponent();
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double number;
            int highest = GetBiggestSubjectLength();

            foreach (Subject s in CSVSubject.Subjects)
            {
                if (s is not null)
                {
                    string st = s.CalculateAverage() is not double.NaN ? s.CalculateAverage().ToString() : "keine Einträge vorhanden";


                    if (double.TryParse(st, out number))
                        st += number % (int)number == 0 ? ",00" : "";


                    Label a = new()
                    {
                        Content = $"{s.Name.PadLeft(highest)} : {st}",
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        FontSize = 18
                    };

                    if (number is not 0)
                    {
                        a.Content += $"(Zeugnisnote: {(int)Math.Round(number, 0)})";
                    }

                    lbxAvgs.Items.Add(a);
                }
            }
        }



        public static int GetBiggestSubjectLength()
        {
            int biggest = 0;


            foreach (Subject s in Subject.Subjects)
                if (s is not null)
                    biggest = s.Name.Length > biggest ? s.Name.Length : biggest;


            return biggest;
        }



        private void LbxAvgs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] curr = (lbxAvgs.SelectedItem as Label)?.Content.ToString().Split(':');

            if (curr is not null && curr.Length > 0)
                DiscordClient.UpdateClient($"{curr[0]}", $"{curr[1]}");
        }
    }
}
