using System;
using System.Collections.Generic;

using Notenverwaltung.Persistenz;

namespace Notenverwaltung
{
    public class Grade
  {
    #region Fields
    public int Id { get; set; }
    public Subject Subject { get; set; }
    public int Rating { get; set; }
    public TypeGrade TypeG { get; set; }
    public DateTime Creation { get; set; }
    public static List<Grade> Grades
    {
      get => CSVGrade.Grades;
      set => CSVGrade.Grades = value;
    }
    #endregion

    #region Constructors
    public Grade()
    {
      Id = 0;
      Subject = new Subject("unbekannt", true);
      Rating = 6;
      TypeG = TypeGrade.muendlich;
      Creation = DateTime.Now;
    }

    public Grade(int id)
    {
      Grade n = Read(id);
      Id = n.Id;
      Subject = n.Subject;
      Rating = n.Rating;
      TypeG = n.TypeG;
      Creation = n.Creation;
    }

    public Grade(Subject fach, int note, TypeGrade artn, DateTime create)
    {
      Id = GetNextID();
      Subject = fach;
      Rating = note;
      TypeG = artn;
      Creation = create;
    }

    public Grade(int id, Subject fach, int note, TypeGrade artn, DateTime create)
    {
      Id = id;
      Subject = fach;
      Rating = note;
      TypeG = artn;
      Creation = create;
    }
    #endregion

    #region Functions
    public String ToSaveableString() => $"{Id};{Subject.Name};{Rating};{(Int32)TypeG};{Creation:yyyy-MM-dd HH:mm:ss}";
    public String[] ToStringArray() { return new String[] { this.Id.ToString(), this.Subject.Name, this.Rating.ToString(), ((Int32)this.TypeG).ToString(), Creation.ToString("yyyy-MM-dd HH:mm:ss") }; }
    public override string ToString() => $"{this.Subject} | {Rating} | {TypeG,-15} | {Creation:dd.MM.yyyy HH:mm:ss}";

    public static int GetNextID() => CSVGrade.GetNextId();
    public static Grade Read(int id) => CSVGrade.Read(id);
    public static void SaveAll() => CSVGrade.SaveAll();
    public static void ReadAll() => CSVGrade.ReadAll();
    public void Delete() => CSVGrade.Delete(this);
    public void Update() => CSVGrade.Update(this);
    public void Save() => CSVGrade.Save(this);
    #endregion
  }
}
