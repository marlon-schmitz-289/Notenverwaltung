using System;
using System.Collections.Generic;
using System.IO;

namespace Notenverwaltung
{
  public class CSVSubject
  {
    private static readonly String PATH = $@"{Environment.CurrentDirectory}\..\..\..\..\Persistenz\Savefiles\fach.csv";

    public static List<Subject> Subjects = new();


    public static Subject Read(String name)
    {
      foreach (var s in Subjects)
        if (s is not null && s.Name is not null && s.Name.Equals(name)) return s;

      return null;
    }


    public static void ReadAll()
    {
      if (File.Exists(PATH))
      {
        List<String> lines = new();
        StreamReader sr = new(File.Open(PATH, FileMode.Open));

        while (!sr.EndOfStream)
          lines.Add(sr.ReadLine());

        sr.Close();

        foreach (var line in lines)
          Subjects.Add(new Subject(line, true));
      }
      else
        throw new Exception("File could not be found!");
    }
  }
}
