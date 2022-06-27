using System;
using System.Collections.Generic;

namespace Notenverwaltung
{
  public class Grade
  {
    #region Fields
    public int Id { get; set; }
    public Subject Subject { get; set; }
    public int Rating { get; set; }
    public Type TypeG { get; set; }
    #endregion

    #region Constructors
    public Grade()
    {
      Id = 0;
      Subject = new Subject("unbekannt", true);
      Rating = 6;
      TypeG = Type.muendlich;
    }

    public Grade(int id)
    {
      Grade n = Read(id);
      Id = n.Id;
      Subject = n.Subject;
      Rating = n.Rating;
      TypeG = n.TypeG;
    }

    public Grade(Subject fach, int note, Type artn)
    {
      Subject = fach;
      Rating = note;
      TypeG = artn;
    }

    public Grade(int id, Subject fach, int note, Type artn)
    {
      Id = id;
      Subject = fach;
      Rating = note;
      TypeG = artn;
    }
    #endregion

    #region Functions
    public String ToSaveableString() => $"{Id};{Subject.Name};{Rating};{(Int32)TypeG}";
    public String[] ToStringArray() { return new string[] { this.Id.ToString(), this.Subject.Name, this.Rating.ToString(), ((Int32)this.TypeG).ToString() }; }
    public override string ToString() => $"{this.Subject} | {Rating} | {TypeG,-15}";
    public static List<Grade> ReadAll() => CSVNote.ReadAll();
    public Grade Read(int id) => CSVNote.Read(id);
    public void Delete() => CSVNote.Delete(this);
    public void Update() => CSVNote.Update(this);
    public void Save() => CSVNote.Save(this);
    #endregion
  }
}
