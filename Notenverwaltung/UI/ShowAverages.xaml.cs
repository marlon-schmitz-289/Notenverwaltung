using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Notenverwaltung
{
  public partial class ShowAverages : Page
  {
    public User CurrUser { get; set; }
    
    public ShowAverages(User curr)
    {
      InitializeComponent();
      CurrUser = curr;
    }
  }
}
