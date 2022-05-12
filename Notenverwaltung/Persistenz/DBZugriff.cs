using MySql.Data.MySqlClient;
using System;

namespace Notenverwaltung
{
  public static class DBZugriff
  {
    #region Fields
    // Schule (aber nur mit LAN-Kabel):
    //private const String CONSTR = "Server=mysql2.schule;Database=bfit2023a_schmitzm;Uid=bfit2023a;Pwd=geheim;";

    // Privat:
    private const String CONSTR = "Server=marlonschmitz.ddns.net;Database=marlon;Uid=root;Pwd=geheim;";
    //private const String CONSTR = "Server=95.111.228.206;Database=marlon;Uid=root;Pwd=geheim;";

    // Marco:
    //private const String CONSTR = "Server=c.baeuml.io;Database=marlon;Uid=root;Pwd=geheim;";
    #endregion

    #region DB
    #region Close
    public static void CloseDB(MySqlConnection con) => con.Close();
    #endregion

    #region Open
    public static MySqlConnection OpenDB()
    {
      MySqlConnection con = new MySqlConnection(CONSTR);
      con.Open();
      return con;
    }
    #endregion
    #endregion

    #region Execute
    #region Non Query
    /// <summary>
    /// Öffnet die Datenbank und setzt einen "Non-Query"-Befehl (Insert, Update, Delete) ab.
    /// </summary>
    /// <param name="sql">Der auszuführende SQL-Befehl</param>
    /// <returns>Anzahl der betroffenen Datensätze</returns>
    public static int ExecuteNonQuery(String sql)
    {
      MySqlConnection con = DBZugriff.OpenDB();

      MySqlCommand cmd = new MySqlCommand(sql, con);
      int anz = cmd.ExecuteNonQuery();
      DBZugriff.CloseDB(con);

      return anz;
    }
    #endregion

    #region Reader
    /// <summary>
    /// Führt einen SQL-Select Befehl über eine bestehende Verbindung ab.
    /// </summary>
    /// <param name="sql">Der Select Befehl</param>
    /// <param name="con">Die Verbindung zur Datenbank</param>
    /// <returns>Das zugehörige DataReader Objekt</returns>
    public static MySqlDataReader ExecuteReader(String sql, MySqlConnection con)
    {
      MySqlCommand cmd = new MySqlCommand(sql, con);
      MySqlDataReader rdr = cmd.ExecuteReader();
      return rdr;
    }
    #endregion
    #endregion

    #region Zusatz
    public static int GetLastInsertId(MySqlConnection con)
    {
      MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", con);
      return Convert.ToInt32(cmd.ExecuteScalar());
    }
    #endregion
  }
}
