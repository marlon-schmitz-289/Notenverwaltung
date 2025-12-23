using System;
using System.Collections.Generic;
using Notenverwaltung.Model.Enums;
using Notenverwaltung.Persistenz;

namespace Notenverwaltung.Model;

public class Subject
{
    public string Name { get; set; }

    public static List<Subject> Subjects
    {
        get => XmlDataStore.Subjects;
        set => XmlDataStore.Subjects = value;
    }

    public Subject()
    {
        Name = "";
    }

    public Subject(string name, bool newSub)
    {
        Name = name;
    }

    public override string ToString() => Name;

    public void Delete() => XmlDataStore.DeleteSubject(this);

    public static void ReadAll() => XmlDataStore.LoadAll();

    public static void SaveAll() => XmlDataStore.SaveAll();

    public override int GetHashCode() => Name?.GetHashCode() ?? 0;

    public override bool Equals(object? obj)
    {
        if (obj is Subject other && other.Name is not null)
            return Name.Equals(other.Name);
        return false;
    }

    public double CalculateAverage()
    {
        double average = 0;
        var count = 0;

        foreach (var grade in XmlDataStore.Grades)
        {
            if (!grade.Subject.Equals(this)) continue;

            if (grade.TypeG == TypeGrade.Double)
            {
                average += grade.Rating * 2;
                count += 2;
            }
            else if (grade.TypeG == TypeGrade.Simple)
            {
                average += grade.Rating;
                count++;
            }
        }

        return Math.Round(average / count, 2);
    }
}