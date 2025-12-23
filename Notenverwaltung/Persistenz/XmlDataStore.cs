using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Notenverwaltung.Model;
using Notenverwaltung.Model.Enums;

namespace Notenverwaltung.Persistenz;

/// <summary>
///     Data container for XML serialization
/// </summary>
[XmlRoot("Notenverwaltung")]
public class AppData
{
    [XmlElement("LastGradeId")] public int LastGradeId { get; set; }

    [XmlArray("Subjects")]
    [XmlArrayItem("Subject")]
    public List<SubjectData> Subjects { get; set; } = new();

    [XmlArray("Grades")]
    [XmlArrayItem("Grade")]
    public List<GradeData> Grades { get; set; } = new();
}

/// <summary>
///     XML-serializable subject data
/// </summary>
public class SubjectData
{
    [XmlAttribute("name")] public string Name { get; set; } = "";
}

/// <summary>
///     XML-serializable grade data
/// </summary>
public class GradeData
{
    [XmlAttribute("id")] public int Id { get; set; }

    [XmlElement("Subject")] public string SubjectName { get; set; } = "";

    [XmlElement("Rating")] public int Rating { get; set; }

    [XmlElement("Type")] public int Type { get; set; }

    [XmlElement("Created")] public DateTime Creation { get; set; }
}

/// <summary>
///     XML-based data store replacing CSV persistence
/// </summary>
public static class XmlDataStore
{
    private static readonly string DataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "Notenverwaltung");

    private static readonly string DataFile = Path.Combine(DataPath, "data.xml");
    private static int _lastGradeId;
    private static bool _isLoaded;

    public static List<Grade> Grades { get; set; } = new();
    public static List<Subject> Subjects { get; set; } = new();

    /// <summary>
    ///     Load all data from XML file
    /// </summary>
    public static void LoadAll()
    {
        // Prevent double-loading
        if (_isLoaded) return;

        EnsureDirectoryExists();

        if (!File.Exists(DataFile))
        {
            Grades = new List<Grade>();
            Subjects = new List<Subject>();
            _lastGradeId = 0;
            _isLoaded = true;
            return;
        }

        try
        {
            var serializer = new XmlSerializer(typeof(AppData));
            using var stream = File.OpenRead(DataFile);
            var data = (AppData?)serializer.Deserialize(stream);

            if (data != null)
            {
                _lastGradeId = data.LastGradeId;

                // Load subjects
                Subjects = new List<Subject>();
                foreach (var subjectData in data.Subjects) Subjects.Add(new Subject(subjectData.Name, true));

                // Load grades
                Grades = new List<Grade>();
                foreach (var gradeData in data.Grades)
                {
                    var subject = FindSubject(gradeData.SubjectName)
                                  ?? new Subject(gradeData.SubjectName, true);

                    Grades.Add(new Grade
                    {
                        Id = gradeData.Id,
                        Subject = subject,
                        Rating = gradeData.Rating,
                        TypeG = (TypeGrade)gradeData.Type,
                        Creation = gradeData.Creation
                    });
                }

                _isLoaded = true;
            }
        }
        catch
        {
            Grades = new List<Grade>();
            Subjects = new List<Subject>();
            _lastGradeId = 0;
            _isLoaded = true;
        }
    }

    /// <summary>
    ///     Save all data to XML file
    /// </summary>
    public static void SaveAll()
    {
        EnsureDirectoryExists();

        var data = new AppData
        {
            LastGradeId = _lastGradeId,
            Subjects = new List<SubjectData>(),
            Grades = new List<GradeData>()
        };

        // Convert subjects
        foreach (var subject in Subjects) data.Subjects.Add(new SubjectData { Name = subject.Name });

        // Convert grades
        foreach (var grade in Grades)
            data.Grades.Add(new GradeData
            {
                Id = grade.Id,
                SubjectName = grade.Subject.Name,
                Rating = grade.Rating,
                Type = (int)grade.TypeG,
                Creation = grade.Creation
            });

        try
        {
            var serializer = new XmlSerializer(typeof(AppData));
            using var stream = File.Create(DataFile);
            serializer.Serialize(stream, data);
        }
        catch
        {
            // Silently fail - data will be saved on next attempt
        }
    }

    #region Grade Operations

    public static Grade? ReadGrade(int id)
    {
        foreach (var g in Grades)
            if (g.Id == id)
                return g;
        return null;
    }

    public static void SaveGrade(Grade g)
    {
        if (!Grades.Contains(g)) Grades.Add(g);
        SetLastId(g.Id);
        SaveAll();
    }

    public static void UpdateGrade(Grade updated)
    {
        var existing = ReadGrade(updated.Id);
        if (existing != null) Grades[Grades.IndexOf(existing)] = updated;
        SaveAll();
    }

    public static void DeleteGrade(Grade g)
    {
        Grades.Remove(g);
        SaveAll();
    }

    public static int GetNextId()
    {
        return _lastGradeId + 1;
    }

    public static void SetLastId(int id)
    {
        _lastGradeId = id;
    }

    #endregion

    #region Subject Operations

    public static void DeleteSubject(Subject s)
    {
        Subjects.Remove(s);
        SaveAll();
    }

    #endregion

    #region Helpers

    private static void EnsureDirectoryExists()
    {
        if (!Directory.Exists(DataPath))
            Directory.CreateDirectory(DataPath);
    }

    private static Subject? FindSubject(string name)
    {
        foreach (var s in Subjects)
            if (s.Name == name)
                return s;
        return null;
    }

    #endregion
}