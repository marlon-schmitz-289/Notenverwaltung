using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenverwaltung
{
  public class DBUser
  {
    public static User Read(String un)
    {
      MySqlConnection con = DBZugriff.OpenDB();
      String sql = $"SELECT * FROM user WHERE username = '{un}'";
      MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con);

      if (rdr.Read()) return GetDataFromReader(rdr);
      else throw new Exception($"Fehler: User mit dem Namen {un} konnte nicht gefunden werden!");
    }


    public static List<User> ReadAll()
    {
      List<User> list = new List<User>();
      String sql = "SELECT * FROM user";
      MySqlConnection con = DBZugriff.OpenDB();
      MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con);

      while (rdr.Read()) list.Add(GetDataFromReader(rdr));

      DBZugriff.CloseDB(con);
      rdr.Close();
      return list;
    }


    public static User GetDataFromReader(MySqlDataReader rdr)
    {
      User user = new User();

      user.Username = rdr.GetString("username");
      user.Password = rdr.GetString("passwort");

      return user;
    }
  }
}
