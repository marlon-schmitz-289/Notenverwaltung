using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Notenverwaltung
{
  public static class DBNote
  {
    #region Create
    public static int Save(Grade n)
    {
      String sql = $"INSERT INTO note (fach, bewertung, art, user) VALUES ({n.Subject.Id}, {n.Rating}, {(Int32)n.TypeG}, '{n.User.Username}')";
      return DBZugriff.ExecuteNonQuery(sql);
    }
    #endregion

    #region Read
    #region All
    public static List<Grade> ReadAll(String un)
    {
      List<Grade> lst = new List<Grade>();
      String sql;

      if (un.Equals("admin")) sql = $"SELECT * FROM note";
      else sql = $"SELECT * FROM note WHERE user='{un}'";

      MySqlConnection con = DBZugriff.OpenDB();
      MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con);

      while (rdr.Read())
      {
        lst.Add(
          new Grade(
            rdr.GetInt32("id"),
            new Subject(rdr.GetInt32("fach")),
            rdr.GetInt32("bewertung"),
            (Type)rdr.GetInt32("art"),
            new User(rdr.GetString("user"))
          )
        );
      }

      DBZugriff.CloseDB(con);
      rdr.Close();

      return lst;
    }
    #endregion

    #region Specific
    public static Grade Read(int id)
    {
      Grade n = new Grade();
      String sql = $"SELECT * FROM note WHERE id = {id}";
      MySqlConnection con = DBZugriff.OpenDB();
      MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con);

      if (rdr.Read())
      {
        n = new Grade(
          rdr.GetInt32("id"),
          new Subject(rdr.GetInt32("fach")),
          rdr.GetInt32("bewertung"),
          (Type)rdr.GetInt32("art"),
          new User(rdr.GetString("user"))
        );
      }

      DBZugriff.CloseDB(con);
      rdr.Close();

      return n;
    }
    #endregion

    #region Last One
    public static Grade LetzteNote(String un)
    {
      String sql = $"SELECT n.* FROM note AS n WHERE user={un} ORDER BY n.id DESC LIMIT 1";
      Grade n = new Grade();

      MySqlConnection con = DBZugriff.OpenDB();
      MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con);

      if (rdr.Read())
      {
        n = new Grade(
          rdr.GetInt32("id"),
          new Subject(rdr.GetInt32("fach")),
          rdr.GetInt32("bewertung"),
          (Type)rdr.GetInt32("art"),
          new User(rdr.GetString("user"))
        );
      }

      DBZugriff.CloseDB(con);
      return n;
    }
    #endregion

    #region Last ID
    public static void GetLastID(Grade n)
    {
      MySqlConnection con = DBZugriff.OpenDB();
      n.Id = DBZugriff.GetLastInsertId(con);
      DBZugriff.CloseDB(con);
    }
    #endregion
    #endregion

    #region Update
    public static int Update(Grade n)
    {
      String sql = $"UPDATE note SET fach = {n.Subject.Id}, bewertung = {n.Rating}, art = {(Int32)n.TypeG} WHERE id = {n.Id}";
      return DBZugriff.ExecuteNonQuery(sql);
    }
    #endregion

    #region Delete
    public static int Delete(Grade n)
    {
      String sql = $"DELETE FROM note WHERE id = {n.Id}";
      return DBZugriff.ExecuteNonQuery(sql);
    }
    #endregion
  }
}
