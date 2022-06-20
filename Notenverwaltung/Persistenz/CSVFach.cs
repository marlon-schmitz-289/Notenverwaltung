using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace Notenverwaltung
{
  public class CSVFach
  {
    private static readonly String PATH = "fach.csv";

    public static Subject Read(String name)
    {
      foreach (var s in ReadAll())
      {
        if (s.Name.Equals(name))
          return s;
      }
      return new();
    }


    public static List<Subject> ReadAll()
    {
      var lst = new List<Subject>();
      
      if (File.Exists(PATH))
      {
        String[] lines = File.ReadAllLines(PATH);
        foreach (var line in lines)
        {
          var parts = line.Split(';');
          lst.Add(new Subject(Int32.Parse(parts[0]), parts[1]));
        }
      }
      
      return lst;
    }
  }
}
