using System;
using System.Collections.Generic;
using System.IO;

namespace Notenverwaltung.Persistenz
{
    public static class CSVGrade
    {
        private static readonly string PATH = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Notenverwaltung";
        private static readonly string PATH_GRADES = $@"{PATH}\noten.csv";
        private static readonly string PATH_LAST_ID = $@"{PATH}\last.txt";

        public static List<Grade> Grades = new();


        public static Grade Read (int id)
        {
            foreach (Grade g in Grades)
                if (g.Id == id) return g;

            return null;
        }


        public static void ReadAll ()
        {
            if (!File.Exists(PATH_GRADES))
            {
                File.Create(PATH_GRADES).Close();
            }

            Grades = new();
            List<string> lines = new();
            StreamReader sr = new(PATH_GRADES);

            while (!sr.EndOfStream)
                lines.Add(sr.ReadLine());

            sr.Close();

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                // Possibility 1:
                //Grades.Add(new Grade(
                //  Int32.Parse(parts[0]),
                //  new(parts[1], false),
                //  Int32.Parse(parts[2]),
                //  (Type)Int32.Parse(parts[3])),
                //  DateTime.Parse(parts[4])
                //);

                // Possibility 2:
                Grades.Add(new Grade()
                {
                    Id = int.Parse(parts[0]),
                    Subject = new(parts[1], true),
                    Rating = int.Parse(parts[2]),
                    TypeG = (TypeGrade)int.Parse(parts[3]),
                    Creation = DateTime.Parse(parts[4])
                });
            }
        }


        public static void Save (Grade g)
        {
            if (!Grades.Contains(g)) Grades.Add(g);
            SaveAll();
            SetLastId(g.Id);
        }


        public static void SaveAll ()
        {
            StreamWriter sw = new(PATH_GRADES, false);
            foreach (Grade g in Grades) sw.WriteLine(g.ToSaveableString());
            sw.Close();
        }


        public static void Update (Grade @new)
        {
            Grade old = Read(@new.Id);
            if (old is not null) Grades[Grades.IndexOf(old)] = @new;
            SaveAll();
        }


        public static void Delete (Grade g)
        {
            if (Grades.Contains(g)) Grades.Remove(g);
            SaveAll();
        }


        public static int GetNextId ()
        {
            try
            {
                StreamReader sr = new(PATH_LAST_ID);
                int id = int.Parse(sr.ReadLine()) + 1;
                sr.Close();
                return id;
            }
            catch
            {
                return 0;
            }
        }


        public static void SetLastId (int id)
        {
            StreamWriter sr = new(PATH_LAST_ID, false);
            sr.WriteLine(id);
            sr.Close();
        }
    }
}
