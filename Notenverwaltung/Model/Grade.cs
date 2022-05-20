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
    public User User { get; set; }
    #endregion

    #region Constructors
    public Grade()
    {
      Id = 0;
      Subject = new Subject();
      Rating = 6;
      TypeG = Type.muendlich;
      User = new User();
    }

    public Grade(int id)
    {
      Grade n = Read(id);
      Id = n.Id;
      Subject = n.Subject;
      Rating = n.Rating;
      TypeG = n.TypeG;
      User = n.User;
    }

    public Grade(Subject fach, int note, Type artn, User user)
    {
      Subject = fach;
      Rating = note;
      TypeG = artn;
      User = user;
    }

    public Grade(int id, Subject fach, int note, Type artn, User user)
    {
      Id = id;
      Subject = fach;
      Rating = note;
      TypeG = artn;
      User = user;
    }
    #endregion

    #region Functions
    public override string ToString() => $"{Subject} | {Rating} | {TypeG,-15}";
    public static List<Grade> ReadAll(String un) => DBNote.ReadAll(un);
    public Grade Read(int id) => DBNote.Read(id);
    public void Delete() => DBNote.Delete(this);
    public void Update() => DBNote.Update(this);
    public void Save() => DBNote.Save(this);
    #endregion
  }
}
