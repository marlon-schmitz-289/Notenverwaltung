using System;
using System.IO;
using System.Text.Json;
using DiscordRPC;

namespace Notenverwaltung.Utils;

public static class DiscordClient
{
    private static RichPresence? _presence;
    public static DiscordRpcClient? Client { get; private set; }

    private static string? LoadClientId()
    {
        var secretsPath = Path.Combine(AppContext.BaseDirectory, "secrets.json");
        if (!File.Exists(secretsPath))
            return null;

        try
        {
            var json = File.ReadAllText(secretsPath);
            var secrets = JsonSerializer.Deserialize<JsonElement>(json);
            return secrets.GetProperty("DiscordClientId").GetString();
        }
        catch
        {
            return null;
        }
    }

    public static void Initialize()
    {
        var clientId = LoadClientId();
        if (string.IsNullOrEmpty(clientId))
            return;

        Client = new DiscordRpcClient(clientId);
        Client.Initialize();

        _presence = new RichPresence
        {
            Details = "Startet das Programm",
            State = "Lädt Daten...",
            Assets = new Assets
            {
                LargeImageKey = "image_large",
                LargeImageText = "sapnu puas",
                SmallImageKey = "image_small"
            },
            Timestamps = Timestamps.Now
        };

        Client.SetPresence(_presence);
    }

    public static void UpdateClient(string details, string state)
    {
        if (Client == null || _presence == null) return;

        _presence.Details = details;
        _presence.State = state;
        Client.SetPresence(_presence);
    }

    public static void Deinitialize()
    {
        if (Client == null || _presence == null)
        {
            Client?.Dispose();
            return;
        }

        _presence.Details = "Beendet das Programm";
        _presence.State = "Hat wohl doch keinen Bock mehr";
        Client.SetPresence(_presence);

        Client.Dispose();
    }
}