# 🔧 Setup Guide for New Users

This guide will help you set up LocalVoiceGenerator with your own Google Cloud credentials.

---

## ⚡ Quick Setup (5 Minutes)

### Step 1: Clone or Download the Project
```bash
# If cloning from git
git clone <repository-url>
cd LocalVoiceGenerator

# Or navigate to your project folder
cd "/path/to/LocalVoiceGenerator"
```

### Step 2: Download Google Cloud Credentials

Follow **[docs/GOOGLE_CLOUD_SETUP.md](../docs/GOOGLE_CLOUD_SETUP.md)** for complete steps.

Quick version:
1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select existing one
3. Enable **Text-to-Speech API**
4. Create a **Service Account**
5. Create a **JSON key** for the service account
6. Download the JSON file

### Step 3: Set Up Your Credentials

You have **3 options** to set up credentials:

#### ✅ Option A: Easy Path (Recommended)
Place your JSON key file in your Downloads folder:
```
~/Downloads/api-project-[YOUR-PROJECT-ID].json
```

Then update `run.sh`:
```bash
# Open run.sh and change this line:
export GOOGLE_APPLICATION_CREDENTIALS="/Users/YOUR_USERNAME/Downloads/api-project-YOUR-PROJECT-ID.json"
```

Then run:
```bash
./run.sh
```

#### ✅ Option B: Environment Variable (Recommended for Developers)
```bash
# macOS/Linux - Add to ~/.zshrc or ~/.bashrc
export GOOGLE_APPLICATION_CREDENTIALS="/path/to/your/credentials.json"

# Then reload
source ~/.zshrc

# Then run
dotnet run
```

#### ✅ Option C: Direct File in Project (Quick Testing Only)
1. Place your `google-credentials.json` in project root
2. Run: `dotnet run`

⚠️ **Warning:** Don't commit this file! It's already in `.gitignore`, but be careful.

---

## 📋 Verification Checklist

Before committing or deploying, verify:

- [ ] Google Cloud credentials are NOT in the repository
- [ ] `.gitignore` includes credential files
- [ ] Environment variable is set correctly
- [ ] Application runs with `./run.sh`
- [ ] All three languages work (English, Hindi, Gujarati)
- [ ] Audio preview works
- [ ] MP3 download works

### Run This Command to Verify

```bash
# Check for hardcoded credentials
cd "/path/to/LocalVoiceGenerator"
grep -r "private_key\|AKIA\|api-project" . --include="*.cs" --include="*.json" \
  | grep -v "docs/" | grep -v "bin/" | grep -v "\.git/"
```

If nothing is returned, you're safe! ✅

---

## 🚀 Running the Application

### With the Run Script (Easiest)
```bash
cd "/path/to/LocalVoiceGenerator"
./run.sh
```

### With Direct Command (If environment variable set)
```bash
cd "/path/to/LocalVoiceGenerator"
dotnet run
```

### Using HTTPS
```bash
cd "/path/to/LocalVoiceGenerator"
dotnet run --launch-profile https
```

---

## 🌐 Access the App

Once running, open your browser to:
```
http://localhost:5033
```

---

## 🛑 Troubleshooting

### Error: "Credentials not found"
→ Follow Step 2 & 3 above to set up credentials

### Error: "Port 5033 already in use"
```bash
# Kill existing process
lsof -i :5033 | awk 'NR!=1 {print $2}' | xargs kill -9
```

### Error: ".NET 9.0 not installed"
```bash
# Install .NET 9.0
# macOS with Homebrew
brew install dotnet@9

# Or download from https://dotnet.microsoft.com/download
```

### Error: "run.sh permission denied"
```bash
chmod +x run.sh
./run.sh
```

---

## 📁 Directory Structure

```
LocalVoiceGenerator/
├── README.md                     # Main documentation
├── SETUP.md                      # This file
├── run.sh                        # Easy run script
├── docs/                         # All documentation
├── Controllers/
│   └── VoiceController.cs
├── Services/
│   └── VoiceService.cs
├── Models/
│   └── VoiceGenerationRequest.cs
├── Views/
│   └── Voice/
│       └── Index.cshtml
├── Properties/
├── appsettings.json
└── .gitignore                    # Protects credentials
```

---

## 🔐 Security Best Practices

1. ✅ **Never commit credentials to git**
   - `.gitignore` protects you
   - Always verify with: `git status`

2. ✅ **Use environment variables for production**
   - Set in deployment platform (Docker, Kubernetes, etc.)
   - Don't hardcode anywhere

3. ✅ **Rotate credentials regularly**
   - Delete old service account keys
   - Create new ones periodically

4. ✅ **Restrict API access**
   - Limit service account permissions
   - Use least-privilege principle

---

## 🎓 Learning Resources

- **[Google Cloud Setup Guide](../docs/GOOGLE_CLOUD_SETUP.md)** - Detailed setup
- **[Credentials Storage Options](../docs/CREDENTIALS_STORAGE_OPTIONS.md)** - All 5 ways
- **[API Testing Guide](../docs/API_TESTING.md)** - How to test endpoints
- **[Development Guide](../docs/DEVELOPMENT.md)** - Adding features

---

## ✅ You're Ready!

Once credentials are set up:

```bash
./run.sh
```

Then visit: **http://localhost:5033** 🎤

---

## 💡 Next Steps

1. **Test the app** - Try different languages and voices
2. **Read the docs** - Explore `docs/` folder
3. **Customize** - Modify UI or add features
4. **Deploy** - Share with others

---

**Need help?** Check `docs/START_HERE.md` or `docs/DOCUMENTATION_INDEX.md`
