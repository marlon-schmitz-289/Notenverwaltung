# Notenverwaltung

A cross-platform grade management application for students built with Avalonia UI.

## Features

- **Grade Management**: Add, edit, and delete grades for your subjects
- **Subject Management**: Create and organize your school subjects
- **Grade Averages**: View calculated averages per subject with final grade predictions
- **Filtering**: Filter grades by subject, type (oral/exam), and date range
- **Discord Rich Presence**: Show your activity status on Discord
- **Themes**: Light and dark mode support (follows system preference)
- **Localization**: Available in German and English

## Installation

### Pre-built Binaries

Download the latest release for your platform from the [Releases](https://github.com/marlon-schmitz-289/Notenverwaltung/releases) page.

### Build from Source

**Requirements:**
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

```bash
# Clone the repository
git clone https://github.com/marlon-schmitz-289/Notenverwaltung.git
cd Notenverwaltung

# Build
dotnet build

# Run
dotnet run --project Notenverwaltung
```

### Publishing

```bash
# Windows
dotnet publish -c Release -r win-x64

# macOS (Apple Silicon)
dotnet publish -c Release -r osx-arm64

# macOS (Intel)
dotnet publish -c Release -r osx-x64

# Linux
dotnet publish -c Release -r linux-x64
```

Output will be in `Notenverwaltung/publish/<platform>/`

## Discord Rich Presence

To enable Discord Rich Presence, create a `secrets.json` file in the application directory:

```json
{
  "DiscordClientId": "YOUR_DISCORD_APPLICATION_ID"
}
```

See `secrets.example.json` for reference.

## Tech Stack

- [Avalonia UI](https://avaloniaui.net/) - Cross-platform .NET UI framework
- [.NET 9.0](https://dotnet.microsoft.com/) - Runtime and SDK
- [Discord RPC](https://github.com/Lachee/discord-rpc-csharp) - Discord Rich Presence

## Authors

- **Marlon Schmitz** - [@marlon-schmitz-289](https://github.com/marlon-schmitz-289)
- **Paul Zipfel** - [@PaulZipfel](https://github.com/PaulZipfel)

## License

This project is for personal/educational use.
