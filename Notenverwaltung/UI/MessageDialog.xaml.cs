﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notenverwaltung
{
  /// <summary>
  /// Interaction logic for MessageDialog.xaml
  /// </summary>
  public partial class MessageDialog : Window
  {
    public String Text
    {
      get => tbxText.Text;
      set => tbxText.Text = value;
    }


    public MessageDialog(String text, Window owner)
    {
      InitializeComponent();

      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      Owner = owner;
      Text = text;

      this.MouseLeftButtonDown += (sender, e) => DragMove();
      brdOK.MouseLeftButtonDown += (sender, e) => this.Close();
    }


    private void Window_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) this.Close(); }
  }
}
