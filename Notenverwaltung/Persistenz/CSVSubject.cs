using System;
using System.Collections.Generic;
using System.IO;

namespace Notenverwaltung.Persistenz
{
    public class CSVSubject
    {
        private static readonly string PATH = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Notenverwaltung\fach.csv";

        public static List<Subject> Subjects { get; set; } = new();


        public static Subject Read(string name)
        {
            foreach (Subject s in Subjects)
                if (s is not null && s.Name is not null && s.Name.Equals(name)) return s;

            return null;
        }


        public static void Delete(Subject s)
        {
            Subjects.Remove(s);
            SaveAll();
        }


        public static void SaveAll()
        {
            StreamWriter sw = new(PATH, false);
            foreach (Subject g in Subjects) sw.WriteLine(g.ToSaveableString());
            sw.Close();
        }


        public static void ReadAll()
        {
            if (File.Exists(PATH))
            {
                List<string> lines = new();
                StreamReader sr = new(File.Open(PATH, FileMode.Open));

                while (!sr.EndOfStream)
                    lines.Add(sr.ReadLine());

                sr.Close();

                foreach (string line in lines)
                    Subjects.Add(new Subject(line, true));

                return;
            }


            File.Create(PATH).Close();
        }
    }
}
