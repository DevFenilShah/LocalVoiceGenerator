# Development Setup Guide

## Prerequisites

### System Requirements
- **OS**: Windows 10+, macOS 10.15+, or Linux (Ubuntu 20.04+)
- **RAM**: Minimum 4GB (8GB recommended)
- **Disk Space**: 2GB for .NET SDK and dependencies

### Required Software
1. **.NET 9 SDK**
   - Download from: https://dotnet.microsoft.com/en-us/download/dotnet/9.0
   - Verify: `dotnet --version` (should show 9.0.x)

2. **IDE (Choose One)**
   - Visual Studio 2022 (Community/Professional/Enterprise)
   - Visual Studio Code with C# extension
   - JetBrains Rider

3. **Google Cloud Account**
   - Create at: https://console.cloud.google.com
   - Enable billing (required for API access)

## Project Setup

### 1. Install .NET SDK

**macOS (Homebrew):**
```bash
brew install dotnet@9
```

**Windows (Installer):**
- Download from Microsoft and run installer
- Or use Chocolatey: `choco install dotnet-sdk-9.0`

**Linux (Ubuntu):**
```bash
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --version 9.0
```

### 2. Set Up Google Cloud

#### Create Project
```bash
# Go to https://console.cloud.google.com
# 1. Create new project
# 2. Name it "LocalVoiceGenerator"
# 3. Select it from dropdown
```

#### Enable Text-to-Speech API
```bash
# Go to APIs & Services > Library
# Search for "Text-to-Speech"
# Click "Enable"
```

#### Create Service Account
```bash
# Go to APIs & Services > Credentials
# Click "Create Credentials" > "Service Account"
# Fill in details:
#   - Service account name: "voice-generator"
#   - Description: "Local voice generation service"
# Click "Create and Continue"
# Grant role: "Editor" (or "Text-to-Speech API User")
# Click "Continue" > "Done"
```

#### Create and Download Key
```bash
# Click on the service account
# Go to "Keys" tab
# Click "Add Key" > "Create new key"
# Select "JSON"
# Click "Create"
# File will download automatically - save it securely
```

### 3. Configure Environment

**macOS/Linux:**
```bash
# Create a .env file (optional, for shell sessions)
export GOOGLE_APPLICATION_CREDENTIALS="/path/to/service-account-key.json"

# Make permanent (add to ~/.zshrc or ~/.bash_profile)
echo 'export GOOGLE_APPLICATION_CREDENTIALS="/path/to/service-account-key.json"' >> ~/.zshrc
source ~/.zshrc
```

**Windows (PowerShell - Permanent):**
```powershell
# Set user environment variable
[Environment]::SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\path\to\service-account-key.json", "User")

# Restart PowerShell to apply changes
```

**Windows (Command Prompt - Permanent):**
```cmd
setx GOOGLE_APPLICATION_CREDENTIALS "C:\path\to\service-account-key.json"
# Restart command prompt to apply changes
```

### 4. Clone/Open Project

```bash
# Navigate to project directory
cd "LocalVoiceGenerator"

# Verify project file exists
ls -la LocalVoiceGenerator.csproj

# Restore dependencies
dotnet restore
```

## IDE Setup

### Visual Studio Code

1. **Install Extensions:**
   - C# Dev Kit (by Microsoft)
   - C# (by Microsoft)
   - REST Client (optional, for testing API)

2. **Open Project:**
   ```bash
   code .
   ```

3. **Configure Launch Settings:**
   - Press `F5` to start debugging
   - Select ".NET" from prompt

### Visual Studio 2022

1. **Open Solution:**
   - File > Open > Project/Solution
   - Navigate to `LocalVoiceGenerator.sln`

2. **Verify NuGet Packages:**
   - Tools > NuGet Package Manager > Manage NuGet Packages for Solution
   - Should show Google.Cloud.TextToSpeech.V1 installed

### JetBrains Rider

1. **Open Project:**
   - File > Open > Select project folder
   - Rider will auto-detect .NET project

2. **Run Configuration:**
   - Run > Run 'LocalVoiceGenerator'
   - Or press `Shift + F10`

## Development Workflow

### Daily Development

```bash
# 1. Start the application
dotnet run

# 2. Open in browser
# https://localhost:7125 (or shown URL)

# 3. Make changes to code
# Changes are detected automatically (hot reload)

# 4. Test API endpoints
# Use REST Client or Postman

# 5. Stop application
# Press Ctrl+C
```

### Code Organization

```
LocalVoiceGenerator/
├── Controllers/          # HTTP request handlers
│   └── VoiceController.cs
├── Services/             # Business logic
│   └── VoiceService.cs
├── Models/               # Data models
│   └── VoiceGenerationRequest.cs
├── Views/                # UI templates
│   └── Voice/Index.cshtml
├── Program.cs            # App entry point
├── appsettings.json      # Configuration
└── README.md             # Documentation
```

### Adding New Features

1. **New Endpoint:**
   - Add action method to `VoiceController`
   - Follow HTTP verb conventions (GET, POST, PUT, DELETE)

2. **New Service:**
   - Create interface in `Services/`
   - Implement interface
   - Register in `Program.cs`: `builder.Services.AddScoped<IYourService, YourService>()`

3. **New View:**
   - Create `.cshtml` file in appropriate `Views/Controller/` folder
   - Reference in controller action

4. **New Model:**
   - Create class in `Models/` folder
   - Use for input/output contracts

## Testing

### Manual Testing

```bash
# Test API with curl
curl -X POST https://localhost:7125/Voice/Preview \
  -H "Content-Type: application/json" \
  -d '{
    "text": "Hello world",
    "languageCode": "en-IN",
    "voiceType": "wavenet",
    "speakingRate": 1.0
  }' \
  -k  # Skip SSL verification for localhost
```

### Create Test File

Create `test-api.http` for VS Code REST Client:

```http
### Preview Voice
POST https://localhost:7125/Voice/Preview HTTP/1.1
Content-Type: application/json

{
  "text": "This is a test",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

### Download Voice
POST https://localhost:7125/Voice/Generate HTTP/1.1
Content-Type: application/json

{
  "text": "This is a test download",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}
```

## Debugging

### Enable Detailed Logging

Edit `appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Debug",
      "Microsoft.AspNetCore": "Debug"
    }
  }
}
```

### Debug in VS Code

1. Press `F5` or go to Run > Start Debugging
2. Set breakpoints by clicking line numbers
3. Use Debug Console to inspect variables
4. Continue, step over, step into with toolbar buttons

### Debug in Visual Studio

1. Press `F5` or Debug > Start Debugging
2. Set breakpoints
3. Use Locals window to inspect variables
4. Use Immediate Window for LINQ queries

## Common Issues & Solutions

### Issue: "Google Cloud credential not found"
```bash
# Verify environment variable
echo $GOOGLE_APPLICATION_CREDENTIALS  # macOS/Linux
echo %GOOGLE_APPLICATION_CREDENTIALS%  # Windows CMD

# Re-export if needed
export GOOGLE_APPLICATION_CREDENTIALS="/correct/path.json"
```

### Issue: "Port 7125 already in use"
```bash
# Find process using port
lsof -i :7125  # macOS/Linux
netstat -ano | findstr :7125  # Windows

# Kill process or use different port
dotnet run --urls "https://localhost:5002"
```

### Issue: "SSL certificate error"
```bash
# For development/testing only:
export ASPNETCORE_ENVIRONMENT=Development

# Or in URL, accept insecure connection
```

### Issue: "Dependencies not restored"
```bash
# Clean cache and restore
dotnet nuget locals all --clear
dotnet restore
dotnet build
```

## Performance Tips

1. **Use Release Build for Testing:**
   ```bash
   dotnet run -c Release
   ```

2. **Disable SSL for Development:**
   - Edit `Properties/launchSettings.json`
   - Remove "sslPort" or set to null

3. **Enable Hot Reload:**
   - Automatic in VS/VS Code
   - Faster development iteration

## Useful Commands

```bash
# Build
dotnet build

# Run
dotnet run

# Run in Release mode
dotnet run -c Release

# Test
dotnet test

# Clean
dotnet clean

# Add NuGet package
dotnet add package PackageName

# List project info
dotnet project-info

# View NuGet packages
dotnet list package
```

## Next Steps

1. ✅ Set up development environment
2. ✅ Configure Google Cloud credentials
3. ✅ Run `dotnet run`
4. ✅ Test in browser
5. 📝 Start development
6. 🚀 Deploy to cloud

---

**Happy Coding! 🚀**

For more information, see:
- [Microsoft .NET Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Google Cloud Text-to-Speech](https://cloud.google.com/text-to-speech/docs)
