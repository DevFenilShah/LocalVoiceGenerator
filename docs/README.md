# LocalVoiceGenerator

A localhost-based ASP.NET Core MVC (.NET 9) application that converts text to speech using Google Cloud Text-to-Speech API. Supports English (India) and Hindi (India) with Wavenet voice synthesis.

## Features

✨ **Text-to-Speech Conversion**
- Convert user-entered text to MP3 audio
- Support for English (en-IN) and Hindi (hi-IN) languages
- Wavenet voice synthesis for natural-sounding audio
- Customizable speaking rate (0.5x - 2.0x)

🎨 **Modern UI**
- Clean, minimal SaaS-style design with gradient background
- Bootstrap 5 responsive layout
- Real-time character counter
- Audio preview player
- Download MP3 button
- Loading spinner during generation
- Error handling with user-friendly messages

⚙️ **Technical Features**
- Dependency Injection for service management
- Async/await pattern for non-blocking operations
- Clean separation of concerns (Controller/Service architecture)
- Environment-based configuration
- Input validation (max 5000 characters)

## Project Structure

```
LocalVoiceGenerator/
├── Controllers/
│   └── VoiceController.cs          # API endpoints for voice generation
├── Services/
│   └── VoiceService.cs             # Google Cloud TTS integration
├── Models/
│   └── VoiceGenerationRequest.cs   # Request model
├── Views/
│   └── Voice/
│       └── Index.cshtml            # Main UI with Bootstrap 5
├── Program.cs                      # Dependency injection setup
├── appsettings.json               # Configuration
└── README.md                       # Documentation
```

## Prerequisites

- .NET 9 SDK or later
- Google Cloud Project with Text-to-Speech API enabled
- Service account JSON key file from Google Cloud

## Setup Instructions

### 1. Create a Google Cloud Project

1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project
3. Enable the Text-to-Speech API:
   - Go to "APIs & Services" → "Library"
   - Search for "Text-to-Speech"
   - Click "Enable"

### 2. Create a Service Account

1. Go to "APIs & Services" → "Credentials"
2. Click "Create Credentials" → "Service Account"
3. Fill in the service account details and create it
4. Go to the service account page, click "Keys" tab
5. Click "Add Key" → "Create new key" → "JSON"
6. Save the JSON file securely (DO NOT commit to git)

### 3. Set Environment Variable

Set the `GOOGLE_APPLICATION_CREDENTIALS` environment variable to point to your service account JSON file.

**macOS/Linux:**
```bash
export GOOGLE_APPLICATION_CREDENTIALS="/path/to/your/service-account-key.json"
```

**Windows (PowerShell):**
```powershell
$env:GOOGLE_APPLICATION_CREDENTIALS = "C:\path\to\service-account-key.json"
```

**Windows (Command Prompt):**
```cmd
set GOOGLE_APPLICATION_CREDENTIALS=C:\path\to\service-account-key.json
```

### 4. Install Dependencies

The Google Cloud Text-to-Speech NuGet package is already included in the `.csproj` file.

To verify:
```bash
dotnet restore
```

### 5. Run the Application

```bash
dotnet run
```

The application will start at `https://localhost:5001` (or the configured port).

## Usage

1. Open the application in your browser
2. Enter your script in the textarea
3. Select language (English or Hindi)
4. Choose voice type (Wavenet or Standard)
5. Adjust the speaking rate using the slider (0.5x - 2.0x)
6. Click "Preview" to hear the audio before downloading
7. Click "Download MP3" to save the generated audio file

### API Endpoints

#### Preview Audio
```
POST /Voice/Preview
Content-Type: application/json

{
  "text": "Your text here",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

Response: { "success": true, "audio": "base64-encoded-audio" }
```

#### Download Audio
```
POST /Voice/Generate
Content-Type: application/json

{
  "text": "Your text here",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

Response: MP3 file download
```

## Configuration

### Supported Languages
- `en-IN` - English (India)
- `hi-IN` - Hindi (India)

### Voice Types
- `wavenet` - Premium, natural-sounding voices
- `standard` - Standard quality voices

### Speaking Rate
- Range: 0.5 to 2.0
- Default: 1.0
- 0.5 = 50% slower, 2.0 = 2x faster

### Max Characters
- 5000 characters per request

## Troubleshooting

### "GOOGLE_APPLICATION_CREDENTIALS environment variable not set"
**Solution:** Make sure you've set the environment variable correctly and restarted your terminal/IDE.

### "Permission denied" on service account
**Solution:** Ensure your service account has the "Text-to-Speech API User" role assigned.

### API quota exceeded
**Solution:** Check your Google Cloud billing and API quota limits at https://console.cloud.google.com/quotas

### Port already in use
**Solution:** Change the port in `Properties/launchSettings.json` or use:
```bash
dotnet run --urls "https://localhost:5002"
```

## Security Notes

⚠️ **Important Security Guidelines**
- Never commit the service account JSON file to version control
- Add `google-cloud-credentials.json` to your `.gitignore`
- Use environment variables for credential management in production
- Rotate service account keys regularly
- Implement rate limiting in production environments

## Dependencies

- **Google.Cloud.TextToSpeech.V1** (v3.17.0)
  - Google Cloud SDK for Text-to-Speech
  - Includes gRPC and necessary dependencies

- **Microsoft.AspNetCore** (included with .NET 9)
  - ASP.NET Core MVC framework
  - Dependency Injection
  - Routing

- **Bootstrap 5** (CDN)
  - Responsive CSS framework
  - Included via CDN in the view

## Performance Considerations

- Audio generation typically takes 1-3 seconds depending on text length
- Wavenet voices are higher quality but may be slightly slower
- Speaking rate adjustment doesn't affect generation time
- Each request generates a new MP3 file

## License

This project is provided as-is for educational purposes.

## Support

For issues with:
- **Google Cloud API**: Visit [Google Cloud Support](https://cloud.google.com/support)
- **ASP.NET Core**: Visit [Microsoft Learn - ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- **This Application**: Check the troubleshooting section above

## Next Steps

1. Deploy to cloud (Azure, Google Cloud, AWS)
2. Add database for storing generated audios
3. Implement user authentication
4. Add batch processing for multiple texts
5. Add audio effects (pitch adjustment, etc.)
6. Implement caching for frequently generated audios
