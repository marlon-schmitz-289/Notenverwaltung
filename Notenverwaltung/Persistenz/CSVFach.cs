using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Notenverwaltung
{
  public class CSVFach
  {
    private static readonly String PATH = $@".\..\..\..\..\Persistenz\Savefiles\fach.csv";


    public static void Read(String name, Subject s, List<Subject> lst)
    {
      foreach (var su in lst)
      {
        if (s.Name.Equals(name)) s = su;
      }
    }


    public static List<Subject> ReadAll()
    {
      var lst = new List<Subject>();

      if (File.Exists(PATH))
      {
        List<String> lines = new();
        StreamReader sr = new(PATH, new FileStreamOptions() { Access = FileAccess.Write });

        while (!sr.EndOfStream)
          lines.Add(sr.ReadLine());

        sr.Close();

        foreach (var line in lines)
          lst.Add(new Subject(line));

        lines = null;
      }
      else
        throw new Exception("File could not be found!");
      
      return lst;
    }
  }
}
