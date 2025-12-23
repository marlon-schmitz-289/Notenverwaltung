using System;
using System.Collections.Generic;
using Notenverwaltung.Model.Enums;
using Notenverwaltung.Persistenz;

namespace Notenverwaltung.Model;

public class Grade
{
    public int Id { get; set; }
    public Subject Subject { get; set; } = new();
    public int Rating { get; set; }
    public TypeGrade TypeG { get; set; }
    public DateTime Creation { get; set; }

    public static List<Grade> Grades
    {
        get => XmlDataStore.Grades;
        set => XmlDataStore.Grades = value;
    }

    public override string ToString() => $"{Subject.Name} | {Rating} | {Creation:dd.MM.yyyy}";

    public static int GetNextId() => XmlDataStore.GetNextId();

    public static void SaveAll() => XmlDataStore.SaveAll();

    public static void ReadAll() => XmlDataStore.LoadAll();

    public void Delete() => XmlDataStore.DeleteGrade(this);

    public void Update() => XmlDataStore.UpdateGrade(this);

    public void Save() => XmlDataStore.SaveGrade(this);
}