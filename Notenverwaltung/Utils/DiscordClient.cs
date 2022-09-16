using System;

using DiscordRPC;
using DiscordRPC.Logging;

namespace Notenverwaltung
{
  public static class DiscordClient
  {
    public static DiscordRpcClient Client { get; private set; }


    public static void Initialize()
    {
      Client = new("1019555860135026758")
      {
        Logger = new ConsoleLogger() { Level = LogLevel.Warning }
      };


      Client.OnReady += (sender, e) =>
        Console.WriteLine("Received Ready from user {0}", e.User.Username);


      Client.OnPresenceUpdate += (sender, e) =>
        Console.WriteLine("Received Update! {0}", e.Presence);


      Client.Initialize();


      Client.SetPresence(new RichPresence()
      {
        Details = "Beendet das Programm",
        State = "Hat wohl doch keinen Bock mehr",
        Assets = new Assets()
        {
          LargeImageKey = "image_large",
          LargeImageText = "sapnu puas",
          SmallImageKey = "image_small"
        }
      });
    }


    public static void UpdateClient(String details, String state)
    {
      Client.SetPresence(new RichPresence()
      {
        Details = details,
        State = state,
        Assets = new Assets()
        {
          LargeImageKey = "image_large",
          LargeImageText = "sapnu puas",
          SmallImageKey = "image_small"
        }
      });
    }


    public static void Deinitialize()
    {
      Client.SetPresence(new RichPresence()
      {
        Details = "Beendet das Programm",
        State = "Hat wohl doch keinen Bock mehr",
        Assets = new Assets()
        {
          LargeImageKey = "image_large",
          LargeImageText = "sapnu puas",
          SmallImageKey = "image_small"
        }
      });


      Client.Dispose();
    }
  }
}
