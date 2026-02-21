# ⚡ Google Cloud Setup - Quick Reference Card

Print this or bookmark it while setting up!

---

## 📋 Checklist (10 Steps)

- [ ] Step 1: Create Google Account (or use existing)
- [ ] Step 2: Create Google Cloud Project
- [ ] Step 3: Enable Billing
- [ ] Step 4: Enable Text-to-Speech API
- [ ] Step 5: Create Service Account
- [ ] Step 6: Create & Download JSON Key
- [ ] Step 7: Save Key Safely
- [ ] Step 8: Set Environment Variable
- [ ] Step 9: Verify Setup
- [ ] Step 10: Run Application

---

## 🔗 Quick Links

| Step | Link |
|------|------|
| 1. Google Account | https://accounts.google.com/signup |
| 2. Cloud Console | https://console.cloud.google.com/ |
| 3. Billing | https://console.cloud.google.com/billing |
| 4. APIs Library | https://console.cloud.google.com/apis/library |
| 5. Service Accounts | https://console.cloud.google.com/iam-admin/serviceaccounts |

---

## 📝 Quick Steps

### Create Account & Project (5 min)
1. Go to console.cloud.google.com
2. Click "Select a Project" → "New Project"
3. Name it "LocalVoiceGenerator"
4. Click "CREATE"

### Enable Billing (3 min)
1. Go to Billing
2. Click "Link a Billing Account"
3. Enter card details
4. Link project to billing

### Enable API (1 min)
1. Go to APIs Library
2. Search "Text-to-Speech"
3. Click "ENABLE"

### Create Service Account (2 min)
1. Go to Service Accounts
2. Click "CREATE SERVICE ACCOUNT"
3. Name: `voice-generator`
4. Click "CREATE AND CONTINUE"
5. Select role: "Text-to-Speech API User"
6. Click "CONTINUE" → "DONE"

### Get JSON Key (1 min)
1. Click the service account email
2. Click "KEYS" tab
3. Click "ADD KEY" → "Create new key"
4. Select "JSON"
5. Click "CREATE"
6. File downloads automatically
7. **Save it somewhere safe!**

---

## 🔐 Set Environment Variable

### macOS/Linux (Terminal)
```bash
# Temporary (current session only)
export GOOGLE_APPLICATION_CREDENTIALS=~/google-credentials.json

# Permanent (add to ~/.zshrc or ~/.bash_profile)
echo 'export GOOGLE_APPLICATION_CREDENTIALS=~/google-credentials.json' >> ~/.zshrc
source ~/.zshrc
```

### Windows PowerShell
```powershell
# Permanent (run as Administrator)
[Environment]::SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\Users\YourUsername\google-credentials.json", "User")

# Restart PowerShell
```

---

## ✅ Verify Setup

### Check Variable is Set
```bash
# macOS/Linux
echo $GOOGLE_APPLICATION_CREDENTIALS

# Windows PowerShell
echo $env:GOOGLE_APPLICATION_CREDENTIALS
```

### Should Output
- macOS/Linux: `/Users/YourUsername/google-credentials.json`
- Windows: `C:\Users\YourUsername\google-credentials.json`

---

## 🚀 Run Application

```bash
cd LocalVoiceGenerator
dotnet run
```

Open: **https://localhost:7125**

---

## 💡 Remember

✅ **Keep JSON key safe!**
- Store in secure location
- Don't commit to git
- Don't share

✅ **Verify each step**
- See ✅ confirmation
- Wait for processing (1-2 min)
- Check settings

✅ **Troubleshoot if needed**
- See GOOGLE_CLOUD_SETUP.md troubleshooting section
- Or check README.md

---

## 📞 Stuck?

Refer to: **[GOOGLE_CLOUD_SETUP.md](GOOGLE_CLOUD_SETUP.md)**
- Full step-by-step guide
- Screenshots references
- Detailed troubleshooting
- Pricing information

---

**Time estimate: 15-20 minutes total**

Once done, your app will work perfectly! 🎉
