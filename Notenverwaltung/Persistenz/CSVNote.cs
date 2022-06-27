using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenverwaltung
{
  public static class CSVNote
  {
    private static readonly String PATH_GRADES = $@".\..\..\..\..\Persistenz\Savefiles\noten.csv";
    private static readonly String PATH_LAST_ID = $@".\..\..\..\..\Persistenz\Savefiles\last.txt";


    public static Grade Read(int id)
    {
      foreach (Grade g in ReadAll())
      {
        if (g.Id == id) return g;
      }

      return null;
    }


    public static List<Grade> ReadAll()
    {
      List<Grade> lst = new();

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
          lst.Add(new Grade(
            Int32.Parse(parts[0]),
            new(parts[1]),
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
          
          parts = null;
        }

        lines = null;
      }

      return lst;
    }


    public static void Save(Grade g)
    {
      var lst = ReadAll();
      if (!lst.Contains(g)) lst.Add(g);
      SaveAll(lst);
      SetLastId(g.Id);
    }


    public static void SaveAll(List<Grade> lst)
    {
      StreamWriter sw = new(PATH_GRADES, false);
      foreach (var g in lst) sw.WriteLine(g.ToSaveableString());
      sw.Close();
    }


    public static void Update(Grade g)
    {
      var lst = ReadAll();
      var old = Read(g.Id);
      if (old != null) lst[lst.IndexOf(old)] = g;
      SaveAll(lst);
    }


    public static void Delete(Grade g)
    {
      var lst = ReadAll();
      if (lst.Contains(g)) lst.Remove(g);
      SaveAll(lst);
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
