using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Notenverwaltung.Utils
{
  public static class Helper
  {
    public static string GetDisplayName(this Enum enumValue)
    {
      return enumValue?.GetType()
        .GetMember(enumValue.ToString())
        .First()
        .GetCustomAttribute<DisplayAttribute>()
        ?.GetName();
    }
  }
}
