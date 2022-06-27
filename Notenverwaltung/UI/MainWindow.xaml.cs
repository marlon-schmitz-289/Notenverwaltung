﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Notenverwaltung
{
  public partial class MainWindow : Window
  {
    private CurrentPage _currPage = CurrentPage.showAll;
    public List<Grade> Grades { get; private set; } = new();

    public MainWindow()
    {
      InitializeComponent();
      WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }


    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      brdSchliessen.MouseLeftButtonDown += (sender, e) => Application.Current.Shutdown();
      brdMinimieren.MouseLeftButtonDown += (sender, e) => this.WindowState = WindowState.Minimized;

#if DEBUG
      MessageDialog dlg = new("<neuer Debugtext>", this);
      dlg.ShowDialog();
#endif
    }


    private void SwitchPage()
    {
      switch (_currPage)
      {
        case CurrentPage.showAll:
          this.frame.Content = new ShowGrades(Grades);
          break;
        case CurrentPage.showAvgs:
          this.frame.Content = new ShowAverages(Grades);
          break;
        case CurrentPage.newEntry:
          this.frame.Content = new AddGrade();
          break;
        case CurrentPage.editEntry:
          this.frame.Content = new Label() { Content = "<Liste mit Einträgen>\nAktuell ausgewählten Eintrag bearbeiten" };
          break;
      }
    }


    private void brdAddGrade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.newEntry;
      SwitchPage();
    }


    private void brdEditGrade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.editEntry;
      SwitchPage();
    }


    private void brdListAvgs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _currPage = CurrentPage.showAvgs;
      SwitchPage();
    }


    private void brdListAll_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (Grades != null)
      {
        _currPage = CurrentPage.showAll;
        SwitchPage();
      }
      else
      {
        MessageDialog dlg = new("Bitte warten, bis die Daten geladen wurden", this);
        dlg.ShowDialog();
      }
    }
  }
}
