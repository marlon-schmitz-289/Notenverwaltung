using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenverwaltung
{
  public class User
  {
    public String Username { get; set; }
    public string Password { get; set; }


    public User()
    {
      Username = "undefined";
      Password = "undefined";
    }


    public User(String un)
    {
      User u = Read(un);
      this.Username = u.Username;
      this.Password = u.Password;
    }


    public User(String un, String pw)
    {
      Username = un;
      Password = pw;
    }


    public User Read(String un) => DBUser.Read(un);
    public static List<User> ReadAll() => DBUser.ReadAll();
    public override string ToString() => $"{Username} | {Password}";
  }
}
