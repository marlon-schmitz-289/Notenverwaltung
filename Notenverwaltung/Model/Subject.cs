using System;
using System.Collections.Generic;

namespace Notenverwaltung
{
  /// <summary>
  /// Einzelnes Fach mit Attribut Name und speziellen Funktionen
  /// </summary>
  public class Subject
  {
    #region Fields
    /// <summary>
    /// Name des Faches
    /// </summary>
    public String Name { get; set; }
    public static List<Subject> Subjects
    {
      get => CSVSubject.Subjects;
      set => CSVSubject.Subjects = value;
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Erstellt neues Objekt mit Übergebenem Namen wenn newSub true, liest andernfalls Fach mit Namen name ein.
    /// </summary>
    public Subject(String name, bool newSub)
    {
      if (newSub)
        Name = name;
      else
        Read(name);
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
    public override string ToString() => $"{Name,-12}";
    /// <summary>
    /// Liest Fach mit übergebenem Namen ein
    /// </summary>
    public void Read(String name) => CSVSubject.Read(name);
    /// <summary>
    /// Liest alle Fächer ein
    /// </summary>
    /// <returns>Liste mit allen Fächern
    /// </returns>
    public static void ReadAll() => CSVSubject.ReadAll();
    public static void SaveAll() => CSVSubject.SaveAll();


    public override bool Equals(object obj)
    {
      Subject other = obj as Subject;
      if (other is not null && other.Name is not null)
        return Name.Equals(other.Name);
      return false;
    }


    public override int GetHashCode() => base.GetHashCode();


    public double CalculateAverage()
    {
      double average = 0;
      int count = 0;

      foreach (Grade grade in CSVGrade.Grades)
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

      return Math.Round(average / count, 2);
    }
    #endregion
  }
}
