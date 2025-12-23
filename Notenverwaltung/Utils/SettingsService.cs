using System;
using System.IO;
using System.Xml.Serialization;

namespace Notenverwaltung.Utils;

[XmlRoot("Settings")]
public class AppSettings
{
    [XmlElement("Language")]
    public string Language { get; set; } = "system";

    [XmlElement("Theme")]
    public string Theme { get; set; } = "system";

    [XmlElement("SidebarCollapsed")]
    public bool SidebarCollapsed { get; set; }

    [XmlElement("AveragesSortMode")]
    public int AveragesSortMode { get; set; } // 0=Name, 1=GradeAsc, 2=GradeDesc
}

public static class SettingsService
{
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "Notenverwaltung",
        "settings.xml");

    private static AppSettings _settings = new();

    public static AppSettings Settings => _settings;

    public static void Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var serializer = new XmlSerializer(typeof(AppSettings));
                using var stream = File.OpenRead(SettingsPath);
                _settings = (AppSettings?)serializer.Deserialize(stream) ?? new AppSettings();
            }
        }
        catch
        {
            _settings = new AppSettings();
        }
    }

    public static void Save()
    {
        try
        {
            var dir = Path.GetDirectoryName(SettingsPath);
            if (dir != null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var serializer = new XmlSerializer(typeof(AppSettings));
            using var stream = File.Create(SettingsPath);
            serializer.Serialize(stream, _settings);
        }
        catch
        {
            // Ignore save errors
        }
    }
}
