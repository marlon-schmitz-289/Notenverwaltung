using Notenverwaltung.Model.Enums;
using Notenverwaltung.Resources;

namespace Notenverwaltung.Utils;

public static class Helper
{
    public static string GetDisplayName(this TypeGrade gradeType)
    {
        return gradeType switch
        {
            TypeGrade.Simple => Strings.GradeTypeSimple,
            TypeGrade.Double => Strings.GradeTypeDouble,
            _ => gradeType.ToString()
        };
    }
}
