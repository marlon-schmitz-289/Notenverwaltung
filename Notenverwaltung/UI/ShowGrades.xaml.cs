﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notenverwaltung
{
  public partial class ShowGrades : Page
  {
    public List<Grade> Grades { get; set; }


    public ShowGrades(List<Grade> grades)
    {
      InitializeComponent();
      Grades = grades;
    }


    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      FillListBox();
    }


    private void FillListBox()
    {
      FillSubjects();
      FillGrades();
    }


    private void FillSubjects()
    {
      cbxSubjects.Items.Clear();
      List<Subject> subjects = Subject.ReadAll();

      foreach (Subject s in subjects) cbxSubjects.Items.Add(s);
    }


    private void FillGrades()
    {
      lbxGrades.Items.Clear();

      if (cbxSubjects.SelectedItem != null)
      {
        Subject s = cbxSubjects.SelectedItem as Subject;


        foreach (Grade g in Grades)
          if (g.Subject == s)
            lbxGrades.Items.Add(g);
      }
    }

    
    private void CbxSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      FillGrades();
    }
  }
}
