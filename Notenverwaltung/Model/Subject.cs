using System;
using System.Collections.Generic;

namespace Notenverwaltung
{
  public class Subject
  {
    #region Fields
    public int Id { get; set; }
    public String Name { get; set; }
    #endregion

    #region Constructors
    public Subject()
    {
      Id = 0;
      Name = "Unbekannt";
    }


    public Subject(int id)
    {
      Read(id);
    }


    public Subject(int id, String name)
    {
      Id = id;
      Name = name;
    }
    #endregion

    #region Functions
    public override string ToString() => $"{Name,-15}";
    public static List<Subject> ReadAll() => DBFach.ReadAll();
    public void Read(int id) => DBFach.Read(id, this);
    
    
    public override bool Equals(object obj)
    {
      Subject other = obj as Subject;
      return this.Id == other.Id;
    }


    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
    #endregion
  }
}
