# 🎤 LocalVoiceGenerator

Convert text to speech in **English**, **Hindi**, and **Gujarati** using Google Cloud Text-to-Speech API with a modern, user-friendly interface.

![Status](https://img.shields.io/badge/status-active-brightgreen)
![License](https://img.shields.io/badge/license-MIT-blue)
![.NET](https://img.shields.io/badge/.NET-9.0-purple)

---

## ⚡ Quick Start (2 Minutes)

### 1. Get Your Credentials
- Follow **[docs/GOOGLE_CLOUD_SETUP.md](docs/GOOGLE_CLOUD_SETUP.md)** (10 minutes one-time setup)
- Download JSON credentials file

### 2. Update run.sh
```bash
# Edit run.sh and update the path to your credentials:
export GOOGLE_APPLICATION_CREDENTIALS="/path/to/your/credentials.json"
```

### 3. Run the App
```bash
cd LocalVoiceGenerator
./run.sh
```

### 4. Open in Browser
```
http://localhost:5033
```

---

## 📚 Full Documentation

### Getting Started
- **[SETUP.md](docs/SETUP.md)** ⭐ **START HERE** - Complete setup guide for new users
- **[docs/START_HERE.md](docs/START_HERE.md)** - Overview and introduction
- **[docs/RUN_APP.md](docs/RUN_APP.md)** - How to run the application
- **[docs/QUICKSTART.md](docs/QUICKSTART.md)** - 5-minute quick start

### Google Cloud Setup
- **[docs/GOOGLE_CLOUD_SETUP.md](docs/GOOGLE_CLOUD_SETUP.md)** - Complete 10-step guide
- **[docs/GOOGLE_CLOUD_QUICKREF.md](docs/GOOGLE_CLOUD_QUICKREF.md)** - Quick reference checklist
- **[docs/CREDENTIALS_STORAGE_OPTIONS.md](docs/CREDENTIALS_STORAGE_OPTIONS.md)** - 5 credential storage methods
- **[docs/USING_DIRECT_JSON_KEY.md](docs/USING_DIRECT_JSON_KEY.md)** - Direct file method guide

### Development & API
- **[docs/README.md](docs/README.md)** - Full project documentation
- **[docs/API_TESTING.md](docs/API_TESTING.md)** - API endpoint testing guide
- **[docs/DEVELOPMENT.md](docs/DEVELOPMENT.md)** - Development environment setup
- **[docs/IMPLEMENTATION_SUMMARY.md](docs/IMPLEMENTATION_SUMMARY.md)** - What's implemented

### Reference
- **[docs/DOCUMENTATION_INDEX.md](docs/DOCUMENTATION_INDEX.md)** - Complete documentation index
- **[docs/PROJECT_COMPLETION_REPORT.md](docs/PROJECT_COMPLETION_REPORT.md)** - Project status

---

## ✨ Features

✅ **Multiple Languages**
- 🇮🇳 English (India)
- 🇮🇳 Hindi (India)
- 🇮🇳 Gujarati (India)

✅ **Voice Options**
- Male & Female voices for each language
- Standard & Wavenet (Premium) voices
- Adjustable speaking rate (0.5x - 2.0x)

✅ **User-Friendly UI**
- Horizontal split-screen layout (70% form / 30% preview)
- Real-time character counter (5000 max)
- Audio preview before download
- One-click MP3 download
- Responsive design
- No scrollbars needed

✅ **Modern Tech Stack**
- ASP.NET Core MVC (.NET 9.0)
- Google Cloud Text-to-Speech API
- Bootstrap 5 + Custom CSS
- Vanilla JavaScript ES6+

---

## 🔧 Requirements

- **.NET 9.0** or higher
- **Google Cloud Account** (free tier available)
- **Text-to-Speech API** enabled
- **Service Account** with JSON credentials

---

## 🚀 First Time Setup

### For New Users:
1. Read **[docs/SETUP.md](docs/SETUP.md)** ⭐
2. Follow Google Cloud setup steps
3. Update `run.sh` with your credentials path
4. Run `./run.sh`

### For Developers:
1. Clone the repository
2. Set `GOOGLE_APPLICATION_CREDENTIALS` environment variable
3. Run `dotnet run` or `./run.sh`

---

## 📖 How to Use

1. **Enter Text** - Type or paste your script (up to 5000 characters)
2. **Select Language** - Choose English, Hindi, or Gujarati
3. **Choose Voice** - Pick Standard or Wavenet (Premium)
4. **Select Gender** - Choose Male or Female voice
5. **Adjust Speed** - Set speaking rate (0.5x to 2.0x)
6. **Preview** - Click 🔊 Preview to listen
7. **Download** - Click ⬇️ Download to save MP3

---

## 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| **Framework** | ASP.NET Core MVC (.NET 9.0) |
| **API** | Google Cloud Text-to-Speech V1 |
| **Frontend** | HTML5, CSS3, JavaScript ES6+ |
| **Styling** | Bootstrap 5, Custom CSS |
| **Authentication** | Google Service Account (JWT) |
| **Database** | None (stateless API) |

---

## 📁 Project Structure

```
LocalVoiceGenerator/
├── README.md                     # This file
├── run.sh                        # Easy run script
├── docs/                         # 📚 All documentation
│   ├── SETUP.md                  # ⭐ Start here
│   ├── GOOGLE_CLOUD_SETUP.md
│   ├── CREDENTIALS_STORAGE_OPTIONS.md
│   └── ... (11 more docs)
│
├── Controllers/
│   └── VoiceController.cs        # HTTP endpoints
│
├── Services/
│   └── VoiceService.cs           # Google Cloud integration
│
├── Models/
│   └── VoiceGenerationRequest.cs # Data model
│
├── Views/
│   └── Voice/
│       └── Index.cshtml          # UI (70% form / 30% preview)
│
├── Properties/
│   └── launchSettings.json
│
└── .gitignore                    # ✅ Protects credentials
```

---

## 🔐 Security

✅ **Credentials Protected**
- Service account keys are NOT in repository
- `.gitignore` prevents accidental commits
- Environment variables for safe credential management
- No hardcoded API keys anywhere

✅ **Best Practices**
- All credentials in `.gitignore`
- Environment variable support
- Multiple credential storage options
- Secure by default

---

## ⚙️ Configuration

### Change Port
Edit `Properties/launchSettings.json`:
```json
"applicationUrl": "http://localhost:YOUR_PORT"
```

### Change Default Values
Edit `appsettings.example.json`:
```json
{
  "VoiceGeneration": {
    "MaxCharacters": 5000,
    "DefaultLanguage": "en-IN",
    "DefaultVoiceType": "standard"
  }
}
```

### Add New Language
Edit `Services/VoiceService.cs`:
```csharp
case "ta-IN": // Tamil
    return gender.ToLower() == "male" ? 
        $"ta-IN-{voicePrefix}-A" : $"ta-IN-{voicePrefix}-B";
```

---

## 🚪 API Endpoints

| Method | Endpoint | Purpose |
|--------|----------|---------|
| GET | `/Voice/Index` | Main UI |
| POST | `/Voice/Preview` | Generate audio for preview (base64) |
| POST | `/Voice/Generate` | Generate and download MP3 file |

### Request Body
```json
{
  "text": "Hello, this is a test",
  "languageCode": "en-IN",
  "voiceType": "standard",
  "gender": "male",
  "speakingRate": 1.0
}
```

---

## 🔄 Supported Languages

| Language | Code | Male Voice | Female Voice |
|----------|------|-----------|--------------|
| English (India) | en-IN | ✅ | ✅ |
| Hindi (India) | hi-IN | ✅ | ✅ |
| Gujarati (India) | gu-IN | ✅ | ✅ |

---

## 🧪 Testing

### Preview Audio
```bash
curl -X POST http://localhost:5033/Voice/Preview \
  -H "Content-Type: application/json" \
  -d '{
    "text": "Hello world",
    "languageCode": "en-IN",
    "voiceType": "standard",
    "gender": "male",
    "speakingRate": 1.0
  }'
```

### Download MP3
```bash
curl -X POST http://localhost:5033/Voice/Generate \
  -H "Content-Type: application/json" \
  -d '{...}' \
  -o output.mp3
```

See **[docs/API_TESTING.md](docs/API_TESTING.md)** for complete testing guide.

---

## 📝 Environment Variables

```bash
# Required
export GOOGLE_APPLICATION_CREDENTIALS="/path/to/credentials.json"

# Optional
export ASPNETCORE_ENVIRONMENT="Development"
export ASPNETCORE_URLS="http://localhost:5033"
```

---

## 🐛 Troubleshooting

### "Credentials not found"
→ Follow **[docs/SETUP.md](docs/SETUP.md)** to set credentials

### "Port already in use"
→ Kill process: `lsof -i :5033 | awk 'NR!=1 {print $2}' | xargs kill -9`

### ".NET 9.0 not found"
→ Install from https://dotnet.microsoft.com/download

### "run.sh permission denied"
→ Make executable: `chmod +x run.sh`

See **[docs/DEVELOPMENT.md](docs/DEVELOPMENT.md)** for more troubleshooting.

---

## 🚀 Deployment

### Docker
```bash
# Create Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENV GOOGLE_APPLICATION_CREDENTIALS=/app/credentials.json
EXPOSE 5033
ENTRYPOINT ["dotnet", "LocalVoiceGenerator.dll"]
```

### Environment-Specific
Set `GOOGLE_APPLICATION_CREDENTIALS` in:
- Docker environment variables
- Kubernetes secrets
- Azure App Service
- AWS Lambda
- Cloud Run

---

## 📚 Learning Resources

- [Google Cloud Text-to-Speech Documentation](https://cloud.google.com/text-to-speech/docs)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [.NET 9.0 Release Notes](https://github.com/dotnet/release-notes/releases/tag/v9.0.0)

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License - see LICENSE file for details.

---

## 💡 Tips

- ✨ **Wavenet voices** sound more natural but cost 2x more
- 🎯 **Standard voices** are great for most use cases
- 📱 UI works on desktop and tablet (optimized for desktop)
- 🎵 Preview before downloading to save API costs
- 🔄 Try different genders and speaking rates

---

## ❓ FAQ

**Q: Can I use this offline?**
A: No, it requires Google Cloud API access.

**Q: How much does it cost?**
A: Google offers free tier. Standard voices: $1-2 per 1M characters, Wavenet: $4-6 per 1M characters.

**Q: Can I add more languages?**
A: Yes! Update `VoiceService.cs` and add language cases.

**Q: Is my data sent to Google?**
A: Yes, text is sent to Google Cloud for processing (this is how TTS works).

**Q: Can I run multiple instances?**
A: Yes, use different ports via launchSettings.json.

---

## 📞 Support

For issues and questions:
1. Check **[docs/DOCUMENTATION_INDEX.md](docs/DOCUMENTATION_INDEX.md)**
2. See **[docs/DEVELOPMENT.md](docs/DEVELOPMENT.md)** troubleshooting
3. Review **[docs/API_TESTING.md](docs/API_TESTING.md)** for endpoint help

---

## 🎉 Ready to Start?

```bash
cd LocalVoiceGenerator
./run.sh
```

Then open: **http://localhost:5033** 🎤

---

**Last Updated:** February 21, 2026
**Version:** 1.0.0
**Status:** Production Ready ✅
