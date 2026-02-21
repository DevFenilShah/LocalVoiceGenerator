# 🚀 How to Run LocalVoiceGenerator

There are two ways to run the application:

## Option 1: Using the Run Script (Recommended) ⭐

### macOS/Linux:
```bash
cd "/Users/richashah/Voice Gen/LocalVoiceGenerator"
./run.sh
```

This script will:
- ✅ Automatically set up the Google Cloud credentials
- ✅ Verify the credentials file exists
- ✅ Start the application
- ✅ Show you the URL to access it

### Windows:
```bash
cd "C:\path\to\LocalVoiceGenerator"
run.bat
```

---

## Option 2: Manual Run (If environment variable is set)

If you've already set `GOOGLE_APPLICATION_CREDENTIALS` in your system environment variables:

```bash
cd "/Users/richashah/Voice Gen/LocalVoiceGenerator"
dotnet run
```

---

## 🌐 Access the Application

Once running, open your browser and go to:
```
http://localhost:5033
```

---

## ⚙️ Environment Variable Setup (One-time)

The app needs Google Cloud credentials. There are two ways:

### Method 1: Using the Run Script (Easiest)
The `run.sh` script automatically handles this - just use Option 1 above.

### Method 2: Set Environment Variable Manually

**macOS/Linux (Permanent):**
```bash
echo 'export GOOGLE_APPLICATION_CREDENTIALS="/Users/richashah/Downloads/api-project-972748005580-cb5aee2857bc.json"' >> ~/.zshrc
source ~/.zshrc
```

**Windows PowerShell (Permanent):**
```powershell
[Environment]::SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\path\to\credentials.json", "User")
```

Then restart your terminal and use `dotnet run` directly.

---

## 📝 Credentials File Location

Your credentials file should be at:
```
/Users/richashah/Downloads/api-project-972748005580-cb5aee2857bc.json
```

If it's in a different location, update the path in `run.sh`.

---

## 🛑 Stop the Application

Press `Ctrl+C` in the terminal to stop the server.

---

## 🔧 Troubleshooting

### "dotnet run" fails with credentials error
→ Use the `run.sh` script instead, which handles credentials automatically

### Port 5033 already in use
→ Kill the old process:
```bash
lsof -i :5033 | awk 'NR!=1 {print $2}' | xargs kill -9
```

### Credentials file not found
→ Make sure your JSON credentials file is in the correct location:
```
/Users/richashah/Downloads/api-project-972748005580-cb5aee2857bc.json
```

---

## ✅ That's it!

Now you can run the app easily with:
```bash
./run.sh
```

Happy voice generating! 🎤
