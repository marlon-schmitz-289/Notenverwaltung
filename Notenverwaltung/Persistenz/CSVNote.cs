using System;
using System.Collections.Generic;
using System.IO;

namespace Notenverwaltung
{
  public static class CSVGrade
  {
    private static readonly String PATH_GRADES = $@"{Environment.CurrentDirectory}\..\..\..\..\Persistenz\Savefiles\noten.csv";
    private static readonly String PATH_LAST_ID = $@"{Environment.CurrentDirectory}\..\..\..\..\Persistenz\Savefiles\last.txt";

    public static readonly List<Grade> Grades = new();


    public static Grade Read(int id)
    {
      foreach (Grade g in Grades)
        if (g.Id == id) return g;

      return null;
    }


    public static void ReadAll()
    {
      if (File.Exists(PATH_GRADES))
      {
        List<String> lines = new();
        StreamReader sr = new(PATH_GRADES);

        while (!sr.EndOfStream)
          lines.Add(sr.ReadLine());

        sr.Close();

        foreach (var line in lines)
        {
          var parts = line.Split(';');

          // Possibility 1:
          Grades.Add(new Grade(
            Int32.Parse(parts[0]),
            new(parts[1], true),
            Int32.Parse(parts[2]),
            (Type)Int32.Parse(parts[3]))
          );

          // Possibility 2:
          //lst.Add(new Grade() {
          //  Id = Int32.Parse(parts[0]), 
          //  Subject = new(parts[1]),
          //  Rating = Int32.Parse(parts[2]),
          //  TypeG = (Type)Int32.Parse(parts[3])
          //});
        }
      }
    }


    public static void Save(Grade g)
    {
      if (!Grades.Contains(g)) Grades.Add(g);
      SaveAll();
      SetLastId(g.Id);
    }


    public static void SaveAll()
    {
      StreamWriter sw = new(PATH_GRADES, false);
      foreach (var g in Grades) sw.WriteLine(g.ToSaveableString());
      sw.Close();
    }


    public static void Update(Grade g)
    {
      var old = Read(g.Id);
      if (old != null) Grades[Grades.IndexOf(old)] = g;
      SaveAll();
    }


    public static void Delete(Grade g)
    {
      if (Grades.Contains(g)) Grades.Remove(g);
      SaveAll();
    }


    public static int GetNextId()
    {
      StreamReader sr = new(PATH_LAST_ID);
      var id = Int32.Parse(sr.ReadLine()) + 1;
      sr.Close();
      return id;
    }


    public static void SetLastId(int id)
    {
      StreamWriter sr = new(PATH_LAST_ID, false);
      sr.WriteLine(id);
      sr.Close();
    }
  }
}
