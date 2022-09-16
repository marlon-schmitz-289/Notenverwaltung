using System.ComponentModel.DataAnnotations;

namespace Notenverwaltung
{
  public enum CurrentPage
  {
    [Display(Name = "Alle anzeigen")]
    showAll = 0,
    [Display(Name = "Neuen Eintrag erstellen")]
    newEntry = 1,
    [Display(Name = "Angelegten Eintrag bearbeiten")]
    editEntry = 2,
    [Display(Name = "Durchschnitte anzeigen")]
    showAvgs = 3,
    [Display(Name = "Eingetragene Fächer bearbeiten")]
    editSubs = 4
  }
}
