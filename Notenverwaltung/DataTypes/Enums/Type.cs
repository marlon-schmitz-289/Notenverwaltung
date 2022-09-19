using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Notenverwaltung
{
  public enum TypeGrade
  {
    [Display(Name = "Mündliche Note")]
    muendlich = 1,
    [Display(Name = "Schulaufgabe")]
    schulaufgabe = 2
  }
}
