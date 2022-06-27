using System;
using System.Collections.Generic;

namespace Notenverwaltung
{
  public class Subject
  {
    #region Fields
    public String Name { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Liest Fach mit übergebenem Namen ein.
    /// </summary>
    public Subject (String name)
    {
      Read(name, ReadAll());
    }


    /// <summary>
    /// Erstellt neues Objekt mit Übergebenem Namen wenn newSub true, liest andernfalls Fach mit Namen name ein.
    /// </summary>
    public Subject(String name, bool newSub)
    {
      if (newSub)
        Name = name;
      else
        Read(name, ReadAll());
    }
    #endregion

    #region Functions
    /// <summary>
    /// Konvertiert Objekt in String, der zum Speichern benutzt wird
    /// </summary>
    public String ToSaveableString() => $"{Name}";
    /// <summary>
    /// Konvertiert Objekt zu gut darstellbarem String
    /// </summary>
    public override string ToString() => $"{Name,-15}";
    /// <summary>
    /// Liest Fach mit übergebenem Namen ein
    /// </summary>
    public void Read(String name, List<Subject> lst) => CSVFach.Read(name, this, lst);
    /// <summary>
    /// Liest alle Fächer ein
    /// </summary>
    /// <returns>Liste mit allen Fächern
    /// </returns>
    public static List<Subject> ReadAll() => CSVFach.ReadAll();
    
    
    public override bool Equals(object obj)
    {
      Subject other = obj as Subject;
      return this.Name.Equals(other.Name);
    }


    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    public double CalculateAverage(List<Grade> grades)
    {
      double average = 0;
      int count = 0;

      foreach (Grade grade in grades)
      {
        if (grade.Subject.Equals(this))
        {
          if (grade.TypeG == Type.schulaufgabe)
          {
            average += grade.Rating * 2;
            count += 2;
          }
          else if (grade.TypeG == Type.muendlich)
          {
            average += grade.Rating;
            count++;
          }
        }
      }

      return average / (double)count;
    }
    #endregion
  }
}
