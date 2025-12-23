using System;
using System.IO;
using System.Text.Json;
using DiscordRPC;
using DiscordRPC.Logging;

namespace Notenverwaltung.Utils;

public static class DiscordClient
{
    private static RichPresence? _presence;
    public static DiscordRpcClient? Client { get; private set; }

    private static string? LoadClientId()
    {
        var secretsPath = Path.Combine(AppContext.BaseDirectory, "secrets.json");
        if (!File.Exists(secretsPath))
        {
            Console.WriteLine("secrets.json not found. Discord Rich Presence disabled.");
            return null;
        }

        try
        {
            var json = File.ReadAllText(secretsPath);
            var secrets = JsonSerializer.Deserialize<JsonElement>(json);
            return secrets.GetProperty("DiscordClientId").GetString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read Discord client ID: {ex.Message}");
            return null;
        }
    }

    public static void Initialize()
    {
        var clientId = LoadClientId();
        if (string.IsNullOrEmpty(clientId))
            return;

        Client = new DiscordRpcClient(clientId)
        {
            Logger = new ConsoleLogger { Level = LogLevel.Warning }
        };

        Client.OnReady += (sender, e) =>
            Console.WriteLine("Received Ready from user {0}", e.User.Username);

        Client.OnPresenceUpdate += (sender, e) =>
            Console.WriteLine("Received Update! {0}", e.Presence);

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