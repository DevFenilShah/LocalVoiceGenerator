# 🔧 How to Use JSON Key Directly in Project (Local Testing Only)

⚠️ **WARNING: This method is NOT recommended for security reasons. Use only for local testing. See CREDENTIALS_STORAGE_OPTIONS.md for alternatives.**

---

## Quick Setup (If You Must Use Direct File)

### Step 1: Place Key in Project Root

After downloading your JSON key from Google Cloud:

1. Copy it to your project root folder
2. Name it: `google-credentials.json`

Your project structure:
```
LocalVoiceGenerator/
├── google-credentials.json          ← Place key here
├── Controllers/
├── Services/
├── Models/
├── Views/
├── Program.cs
└── ... other files
```

### Step 2: Update .gitignore (CRITICAL!)

Ensure your `.gitignore` file includes:

```
# Google Cloud Credentials
google-credentials.json
service-account-key.json
*.json.local
*-credentials.json
```

Check that it's already there:
```bash
grep -i "credentials\|json" .gitignore
```

### Step 3: Code to Load Key Directly

Update your `Services/VoiceService.cs`:

```csharp
using Google.Cloud.TextToSpeech.V1;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;

namespace LocalVoiceGenerator.Services
{
    public interface IVoiceService
    {
        Task<byte[]> GenerateVoiceAsync(string text, string languageCode, 
                                       string voiceType, float speakingRate);
    }

    public class VoiceService : IVoiceService
    {
        private readonly TextToSpeechClient _client;
        private const int MaxCharacters = 5000;

        public VoiceService()
        {
            _client = CreateClientWithDirectKey();
        }

        private TextToSpeechClient CreateClientWithDirectKey()
        {
            try
            {
                // Try to load from direct file first (NOT RECOMMENDED)
                var keyPath = Path.Combine(
                    Directory.GetCurrentDirectory(), 
                    "google-credentials.json"
                );

                if (File.Exists(keyPath))
                {
                    Console.WriteLine($"[WARNING] Loading credentials from direct file: {keyPath}");
                    Console.WriteLine("[WARNING] This is NOT secure for production use!");
                    
                    // Load credentials from JSON file
                    var credential = GoogleCredential.FromFile(keyPath)
                        .CreateScoped(TextToSpeechClient.DefaultScopes);
                    
                    // Create gRPC channel with credentials
                    var channel = new Grpc.Net.Client.GrpcChannel(
                        "texttospeech.googleapis.com",
                        new Grpc.Net.Client.GrpcChannelOptions
                        {
                            Credentials = ChannelCredentials.Create(
                                ChannelCredentials.SecureSsl,
                                credential.ToChannelCredentials())
                        }
                    );
                    
                    return new TextToSpeechClient(channel);
                }
                else
                {
                    throw new FileNotFoundException(
                        $"JSON credentials file not found at: {keyPath}\n" +
                        "Please place 'google-credentials.json' in the project root.\n" +
                        "Or set GOOGLE_APPLICATION_CREDENTIALS environment variable.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Failed to create Text-to-Speech client. " +
                    "Ensure google-credentials.json exists in project root.", ex);
            }
        }

        public async Task<byte[]> GenerateVoiceAsync(string text, string languageCode, 
                                                     string voiceType, float speakingRate)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Text cannot be empty.");
            }

            if (text.Length > MaxCharacters)
            {
                throw new ArgumentException(
                    $"Text exceeds maximum length of {MaxCharacters} characters.");
            }

            if (speakingRate < 0.5f || speakingRate > 2.0f)
            {
                throw new ArgumentException(
                    "Speaking rate must be between 0.5 and 2.0.");
            }

            try
            {
                var input = new SynthesisInput { Text = text };
                var voiceName = GetVoiceName(languageCode, voiceType);
                
                var voice = new VoiceSelectionParams
                {
                    LanguageCode = languageCode,
                    Name = voiceName
                };

                var audioConfig = new AudioConfig
                {
                    AudioEncoding = AudioEncoding.Mp3,
                    SpeakingRate = speakingRate
                };

                var response = await _client.SynthesizeSpeechAsync(
                    new SynthesizeSpeechRequest
                    {
                        Input = input,
                        Voice = voice,
                        AudioConfig = audioConfig
                    });

                return response.AudioContent.ToByteArray();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Failed to generate voice. Please check your Google Cloud credentials and API key.", ex);
            }
        }

        private string GetVoiceName(string languageCode, string voiceType)
        {
            if (voiceType.ToLower() != "wavenet" && voiceType.ToLower() != "standard")
            {
                voiceType = "wavenet";
            }

            return languageCode switch
            {
                "hi-IN" => "hi-IN-Wavenet-A",
                "en-IN" => "en-IN-Wavenet-B",
                _ => "en-IN-Wavenet-B"
            };
        }
    }
}
```

### Step 4: Install Required NuGet Packages

Make sure you have all the required packages:

```bash
dotnet add package Google.Cloud.TextToSpeech.V1
dotnet add package Grpc.Net.Client
dotnet restore
```

### Step 5: Run the Application

```bash
cd LocalVoiceGenerator
dotnet run
```

Open your browser: `https://localhost:7125`

---

## 🧪 Testing It Works

### Test 1: Check File Exists
```bash
ls -la google-credentials.json
```

You should see the file listed.

### Test 2: Check Credentials Are Valid
```bash
cat google-credentials.json | grep -i "type"
```

Should output: `"type": "service_account",`

### Test 3: Run Application
```bash
dotnet run
```

Should start without errors. Look for (don't worry about the warning):
```
[WARNING] Loading credentials from direct file: ...
[WARNING] This is NOT secure for production use!
```

### Test 4: Test Voice Generation
1. Open: https://localhost:7125
2. Enter text: "Hello, this is a test"
3. Click "Preview"
4. Should hear audio 🔊

---

## ⚠️ Critical Security Rules

### DO:
- ✅ Add `google-credentials.json` to `.gitignore`
- ✅ Verify it's in `.gitignore` before committing
- ✅ Store key in secure location
- ✅ Use environment variable instead (recommended)
- ✅ Delete key before sharing code
- ✅ Rotate keys regularly

### DON'T:
- ❌ Commit `google-credentials.json` to git
- ❌ Share the key with anyone
- ❌ Post it online
- ❌ Use this method in production
- ❌ Leave it on public computers
- ❌ Deploy this code with key to servers

---

## 🔄 Verify .gitignore is Working

Before committing any code:

```bash
# Check that git ignores the credentials file
git check-ignore google-credentials.json

# Should output: google-credentials.json

# If nothing outputs, add it to .gitignore!
echo "google-credentials.json" >> .gitignore
```

---

## 🔄 Switching to Environment Variable (Recommended Later)

When you want to switch to the secure method:

### Step 1: Set Environment Variable
```bash
# macOS/Linux
export GOOGLE_APPLICATION_CREDENTIALS=~/google-credentials.json

# Windows PowerShell
$env:GOOGLE_APPLICATION_CREDENTIALS = "C:\path\to\google-credentials.json"
```

### Step 2: Revert VoiceService.cs to Original

Replace the code above with:
```csharp
public VoiceService()
{
    // Google Cloud SDK automatically uses GOOGLE_APPLICATION_CREDENTIALS
    _client = TextToSpeechClient.Create();
}
```

### Step 3: Delete the JSON File from Project
```bash
rm google-credentials.json
```

### Step 4: Test
```bash
dotnet run
```

Should work exactly the same, but more securely!

---

## 🐛 Troubleshooting

### Error: "JSON credentials file not found"

```
Solution:
1. Check file exists: ls -la google-credentials.json
2. Check it's in project root (same folder as Program.cs)
3. Check filename is exactly: google-credentials.json (case sensitive on Linux/Mac)
4. Try moving it to the exact location
```

### Error: "Invalid credentials format"

```
Solution:
1. Download fresh JSON from Google Cloud Console
2. Check file is valid JSON: cat google-credentials.json | head
3. Should start with: {
4. Should have: "type": "service_account",
```

### Error: "Text-to-Speech API not enabled"

```
Solution:
1. Go to: https://console.cloud.google.com/apis/library
2. Search "Text-to-Speech"
3. Click "ENABLE"
4. Wait 1-2 minutes
5. Try again
```

### Error: "Permission denied"

```
Solution:
1. Verify service account has "Text-to-Speech API User" role
2. Go to: https://console.cloud.google.com/iam-admin/serviceaccounts
3. Click your service account
4. Click "ROLES" tab
5. Verify role is assigned
```

---

## ℹ️ Why This Is Not Recommended

1. **Risk of Committing to Git**
   - Even with .gitignore, human error happens
   - If committed, key is exposed to everyone
   - Repository history keeps it forever

2. **Security Risk**
   - Anyone with computer access can steal the key
   - They could generate enormous bills
   - They could compromise your Google Cloud account

3. **Not Production-Ready**
   - Doesn't work on cloud platforms (Azure, AWS, GCP)
   - Cannot be used in Docker containers securely
   - Fails security audits

4. **Bad Practice**
   - Violates industry standards
   - Will be rejected in code reviews
   - Professional developers never do this

---

## ✅ Recommended Approach

See: **CREDENTIALS_STORAGE_OPTIONS.md**

For long-term, professional use:
- Use **Environment Variables** (Option 1) - ⭐ Recommended
- Or use **.NET User Secrets** (Option 4) - For development
- Or use **Cloud Secrets** (Option 5) - For production

All are more secure and are industry standard.

---

## 📝 Checklist

- [ ] `google-credentials.json` downloaded from Google Cloud
- [ ] File placed in project root folder
- [ ] `.gitignore` includes `google-credentials.json`
- [ ] Verified with: `git check-ignore google-credentials.json`
- [ ] VoiceService.cs updated with new code
- [ ] NuGet packages installed: `dotnet restore`
- [ ] Application runs: `dotnet run`
- [ ] Tested in browser: https://localhost:7125
- [ ] Audio generation works
- [ ] Plan to switch to environment variables soon ✅

---

## 🎯 Next Steps

1. ✅ Use direct file for immediate testing
2. ✅ Verify it works
3. 🔄 **Soon:** Switch to environment variable (more secure)
4. 📚 Read: CREDENTIALS_STORAGE_OPTIONS.md (understand the options)

---

**Remember: This is a temporary solution for testing. Switch to environment variables for proper security!** 🔐

For production use, never include credentials in your project code.
