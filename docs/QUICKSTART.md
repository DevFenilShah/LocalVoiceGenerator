# LocalVoiceGenerator - Quick Start Guide

## 🚀 Getting Started in 5 Minutes

### Prerequisites Check
- ✅ .NET 9 SDK installed
- ✅ Google Cloud Account
- ✅ Text-to-Speech API enabled

### Step 1: Set Up Google Cloud Credentials (2 minutes)

**👉 FIRST TIME?** Follow the complete guide: **[GOOGLE_CLOUD_SETUP.md](GOOGLE_CLOUD_SETUP.md)**

This will walk you through:
- Creating a Google Cloud account
- Setting up a project
- Enabling the Text-to-Speech API
- Creating a service account
- Downloading your JSON key
- Setting up the environment variable

**Already have credentials?** Continue below:

1. Download your service account JSON key from Google Cloud Console
2. Save it somewhere safe (e.g., `~/google-credentials.json`)
3. Set the environment variable:

**macOS/Linux:**
```bash
export GOOGLE_APPLICATION_CREDENTIALS=~/google-credentials.json
```

**Windows (PowerShell):**
```powershell
$env:GOOGLE_APPLICATION_CREDENTIALS = "$HOME\google-credentials.json"
```

### Step 2: Navigate to Project Directory (30 seconds)

```bash
cd "LocalVoiceGenerator"
```

### Step 3: Run the Application (30 seconds)

```bash
dotnet run
```

You should see:
```
Now listening on: https://localhost:7125
Now listening on: http://localhost:5219
Application started. Press Ctrl+C to exit.
```

### Step 4: Open in Browser (30 seconds)

Open your browser and go to: **https://localhost:7125** (or the HTTPS URL shown in console)

### Step 5: Start Generating Voice! (1 minute)

1. Enter text in the textarea
2. Select language and voice type
3. Adjust speaking rate with the slider
4. Click "Preview" to hear it first
5. Click "Download MP3" to save the audio file

## 🎯 Common Tasks

### Generate Hindi Audio
```
Text: "नमस्ते, यह एक परीक्षण है"
Language: Hindi (hi-IN)
Voice Type: Wavenet
Speaking Rate: 1.0x
```

### Generate Faster English Audio
```
Text: "Hello, this is a test"
Language: English (en-IN)
Voice Type: Wavenet
Speaking Rate: 1.5x
```

### Generate Slower Audio for Better Clarity
```
Speaking Rate: 0.7x
```

## 🔧 Troubleshooting

### Error: "GOOGLE_APPLICATION_CREDENTIALS not found"
**Solution:** Set the environment variable and restart your terminal:
```bash
# Verify it's set
echo $GOOGLE_APPLICATION_CREDENTIALS  # macOS/Linux
echo $env:GOOGLE_APPLICATION_CREDENTIALS  # Windows PowerShell
```

### Error: "Failed to authenticate with Google Cloud"
**Solution:** 
- Check that your service account JSON is correct
- Verify Text-to-Speech API is enabled in Google Cloud Console
- Check that service account has correct permissions

### Error: "Port already in use"
**Solution:** Kill the existing process or use a different port:
```bash
dotnet run --urls "https://localhost:5002"
```

### Application won't start
**Solution:**
```bash
# Clean and rebuild
dotnet clean
dotnet build
dotnet run
```

## 📝 Project Files Overview

| File | Purpose |
|------|---------|
| `Controllers/VoiceController.cs` | Handles HTTP requests for voice generation |
| `Services/VoiceService.cs` | Manages Google Cloud Text-to-Speech API calls |
| `Models/VoiceGenerationRequest.cs` | Data model for voice generation requests |
| `Views/Voice/Index.cshtml` | Main UI with HTML, CSS, and JavaScript |
| `Program.cs` | Dependency injection and app configuration |
| `.gitignore` | Protects credentials from git |

## 🔐 Security Reminders

⚠️ **DO NOT:**
- Commit service account JSON files to git
- Share your credentials
- Expose `GOOGLE_APPLICATION_CREDENTIALS` in logs
- Use personal Google account credentials

✅ **DO:**
- Use service accounts for applications
- Store credentials in environment variables
- Use `.gitignore` to protect sensitive files
- Rotate keys periodically

## 📚 Next Steps

1. **Customize the UI**: Edit `Views/Voice/Index.cshtml`
2. **Add More Languages**: Update `GetVoiceName()` in `VoiceService.cs`
3. **Deploy to Cloud**: Use Azure, Google Cloud, or AWS
4. **Add Database**: Store generated audios
5. **Add Authentication**: Protect the application

## 🆘 Need Help?

- **Google Cloud Docs**: https://cloud.google.com/text-to-speech/docs
- **ASP.NET Core Docs**: https://learn.microsoft.com/en-us/aspnet/core/
- **GitHub Issues**: Check project repository

## 💡 Pro Tips

1. **Batch Processing**: Modify controller to accept multiple texts
2. **Caching**: Store generated audio to avoid re-processing same text
3. **Rate Control**: Add throttling to prevent API quota exhaustion
4. **Audio Effects**: Use additional processing libraries for effects
5. **Download History**: Add database to track downloads

---

**Happy Voice Generating! 🎤**

Questions? Check the main README.md for more detailed documentation.
