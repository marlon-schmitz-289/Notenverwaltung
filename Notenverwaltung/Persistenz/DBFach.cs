using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Notenverwaltung
{
  public static class DBFach
  {
    public static List<Subject> ReadAll()
    {
      List<Subject> lst = new List<Subject>();

      String sql = $"SELECT * FROM fach";
      MySqlConnection con = DBZugriff.OpenDB();
      MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con);

      while (rdr.Read())
      {
        Subject f = new Subject();
        GetDataFromReader(f, rdr);
        lst.Add(f);
      }

      DBZugriff.CloseDB(con);
      return lst;
    }

    public static void Read(int id, Subject f)
    {
      String sql = $"SELECT * FROM fach WHERE id = {id}";
      MySqlConnection con = DBZugriff.OpenDB();
      MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con);

      if (rdr.Read())
      {
        GetDataFromReader(f, rdr);
      }

      DBZugriff.CloseDB(con);
    }


    public static void GetDataFromReader(Subject f, MySqlDataReader rdr)
    {
      f.Id = rdr.GetInt32("id");
      f.Name = rdr.GetString("name");
    }
  }
}