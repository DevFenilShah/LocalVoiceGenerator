# 🔑 Google Cloud Setup Guide

## Complete Step-by-Step Instructions to Get Your Service Account Key

This guide will walk you through creating a Google Cloud project and obtaining the credentials you need for LocalVoiceGenerator.

---

## 📋 Prerequisites

- ✅ Personal email address (Gmail or other)
- ✅ Credit card (for billing, but won't be charged for free tier)
- ✅ 10-15 minutes of your time

---

## 🚀 Step 1: Create a Google Cloud Account

### Option A: If You Don't Have a Google Account

1. Go to: https://accounts.google.com/signup
2. Fill in:
   - First name
   - Last name
   - Email address (or create a new Gmail)
   - Password
3. Click "Next"
4. Verify your phone number
5. Accept terms and create account

### Option B: If You Already Have a Google Account

Just use your existing Gmail address.

---

## 📱 Step 2: Create a Google Cloud Project

1. Go to: https://console.cloud.google.com/
2. Sign in with your Google account
3. You should see a **"Welcome to Cloud Console"** page

### Create New Project

1. At the top, click the project dropdown that says **"Select a Project"**
2. Click **"NEW PROJECT"** button (top right)
3. Fill in:
   - **Project name:** `LocalVoiceGenerator`
   - **Organization:** Leave blank (or select if you have one)
4. Click **"CREATE"**
5. Wait 1-2 minutes for project to be created
6. Click **"SELECT PROJECT"** in the notification or from dropdown

---

## 💳 Step 3: Enable Billing (Required for API)

The Text-to-Speech API requires billing to be enabled (though free tier covers your usage).

### Enable Billing

1. Go to: https://console.cloud.google.com/billing
2. Click **"LINK A BILLING ACCOUNT"**
3. Click **"CREATE BILLING ACCOUNT"**
4. Fill in your details:
   - Account type: Select your type (Personal, Business)
   - Country: Your country
   - Address: Your address
5. Enter your **credit card information**
   - Card number
   - Expiration date
   - CVV
   - **Note:** Google will NOT charge you for free tier usage
6. Click **"START FREE TRIAL"** or **"NEXT"**
7. Accept terms and create account

### Link to Your Project

1. Return to: https://console.cloud.google.com/billing
2. Click your new billing account
3. Click **"PROJECTS"** link
4. Click **"LINK PROJECT"**
5. Select your **"LocalVoiceGenerator"** project
6. Click **"LINK PROJECT"**

---

## 🎯 Step 4: Enable Text-to-Speech API

1. Go to: https://console.cloud.google.com/apis/library
2. Search for: **"Text-to-Speech"**
3. Click the **"Text-to-Speech API"** result
4. Click the blue **"ENABLE"** button
5. Wait for it to say **"Enabled"** with a checkmark
6. You should see **"API enabled"** message at top

---

## 🔐 Step 5: Create a Service Account

A Service Account is what your application will use to authenticate.

### Create Service Account

1. Go to: https://console.cloud.google.com/iam-admin/serviceaccounts
2. Click **"CREATE SERVICE ACCOUNT"** button at top
3. Fill in:
   - **Service account name:** `voice-generator`
   - **Service account ID:** Auto-fills (leave as is)
   - **Description:** `Service account for LocalVoiceGenerator`
4. Click **"CREATE AND CONTINUE"**

### Grant Permissions

1. Under **"Grant this service account access to project"**:
   - Click **"Select a role"** dropdown
   - Search for: **"Text-to-Speech"**
   - Select: **"Text-to-Speech API User"**
2. Click **"CONTINUE"**
3. Click **"DONE"**

You should see your service account listed now.

---

## 🔑 Step 6: Create and Download JSON Key

This is the most important step - this key is what your application uses!

### Create Key

1. In the service accounts list, click the **email address** of the account you just created
   - (It should look like `voice-generator@project-id.iam.gserviceaccount.com`)
2. Click the **"KEYS"** tab at the top
3. Click **"ADD KEY"** → **"Create new key"**
4. Select: **"JSON"** (should be default)
5. Click **"CREATE"**

### Important! Download & Save

A JSON file will automatically download to your computer. **This is your credential key!**

✅ **SAVE THIS FILE SAFELY** - You need it to run LocalVoiceGenerator

Suggested location:
- **macOS/Linux:** `~/google-credentials.json`
- **Windows:** `C:\Users\YourUsername\google-credentials.json`

---

## 🛡️ Step 7: Protect Your Key (Important!)

⚠️ **Your JSON key is like a password - keep it secure!**

### Do NOT:
- ❌ Commit to GitHub
- ❌ Share with anyone
- ❌ Post online
- ❌ Leave in public folders

### Do:
- ✅ Keep in secure location
- ✅ Use environment variables
- ✅ Set proper file permissions

### Set File Permissions (macOS/Linux only)

```bash
chmod 600 ~/google-credentials.json
```

---

## 🔗 Step 8: Set Up Environment Variable

Now tell your computer where the key is located.

### macOS/Linux

**Option 1: Current Terminal Session Only**
```bash
export GOOGLE_APPLICATION_CREDENTIALS=~/google-credentials.json
```

**Option 2: Permanent (Add to ~/.zshrc or ~/.bash_profile)**
```bash
# Edit the file
nano ~/.zshrc

# Add this line at the end:
export GOOGLE_APPLICATION_CREDENTIALS=~/google-credentials.json

# Save: Ctrl+O, Enter, Ctrl+X
# Then reload:
source ~/.zshrc
```

### Windows (PowerShell)

**Permanent Setup:**
```powershell
# Run as Administrator
[Environment]::SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\Users\YourUsername\google-credentials.json", "User")

# Restart PowerShell for changes to take effect
```

### Windows (Command Prompt)

**Permanent Setup:**
```cmd
# Run as Administrator
setx GOOGLE_APPLICATION_CREDENTIALS "C:\Users\YourUsername\google-credentials.json"

# Restart Command Prompt for changes to take effect
```

---

## ✅ Step 9: Verify Setup

### Check Environment Variable is Set

**macOS/Linux:**
```bash
echo $GOOGLE_APPLICATION_CREDENTIALS
# Should output: ~/google-credentials.json
```

**Windows PowerShell:**
```powershell
echo $env:GOOGLE_APPLICATION_CREDENTIALS
# Should output: C:\Users\YourUsername\google-credentials.json
```

### Verify File Exists

**macOS/Linux:**
```bash
cat ~/google-credentials.json | head -5
# Should show JSON content starting with:
# {
#   "type": "service_account",
```

**Windows PowerShell:**
```powershell
Get-Content "C:\Users\YourUsername\google-credentials.json" | Select-Object -First 5
```

---

## 🚀 Step 10: Run LocalVoiceGenerator

Once you've completed all steps above:

```bash
cd LocalVoiceGenerator
dotnet run
```

Open: https://localhost:7125

Your application should now work! 🎉

---

## 🐛 Troubleshooting

### "GOOGLE_APPLICATION_CREDENTIALS not found"

**Solution:**
1. Verify the environment variable is set: `echo $GOOGLE_APPLICATION_CREDENTIALS`
2. Check file path is correct
3. Restart terminal/IDE
4. Try setting it again

### "Permission denied" Error

**Solution:**
1. Check file permissions: `ls -la ~/google-credentials.json`
2. Should show: `-rw-------` (600)
3. Fix permissions: `chmod 600 ~/google-credentials.json`

### "Text-to-Speech API not enabled"

**Solution:**
1. Go to: https://console.cloud.google.com/apis/library
2. Search and enable "Text-to-Speech API"
3. Wait 1-2 minutes
4. Try again

### "Billing not enabled"

**Solution:**
1. Go to: https://console.cloud.google.com/billing
2. Make sure billing account is created
3. Make sure project is linked to billing account
4. Wait 5 minutes for changes to propagate

### "Invalid service account"

**Solution:**
1. Check service account has correct role
2. Go to: https://console.cloud.google.com/iam-admin/serviceaccounts
3. Click your service account
4. Go to "Roles" tab
5. Make sure it has "Text-to-Speech API User" role

### "Cannot find JSON key"

**Solution:**
1. Check download folder for JSON file
2. If not found, create new key:
   - Go to: https://console.cloud.google.com/iam-admin/serviceaccounts
   - Click your service account
   - Click "KEYS" tab
   - Click "ADD KEY" → "Create new key"
   - Select "JSON"
   - Click "CREATE"

---

## 💰 Pricing & Costs

### Free Tier (Included)
- **First 1 million characters per month:** FREE
- **Wavenet voices:** FREE
- **Standard voices:** FREE

### After Free Tier
- **Wavenet:** $0.00002 per character
- **Standard:** $0.000004 per character

**Example:** 10,000 characters = $0.0002 (very cheap!)

Most users stay well within free tier.

---

## 📝 What You Should Have Now

After completing all steps:

1. ✅ Google Cloud Account
2. ✅ LocalVoiceGenerator project
3. ✅ Billing enabled
4. ✅ Text-to-Speech API enabled
5. ✅ Service account created
6. ✅ JSON key file downloaded
7. ✅ Environment variable set
8. ✅ Ready to run LocalVoiceGenerator!

---

## 🎯 Next Steps

1. ✅ Complete steps 1-8 above
2. ✅ Verify environment variable is set
3. ✅ Run: `cd LocalVoiceGenerator && dotnet run`
4. ✅ Open: https://localhost:7125
5. ✅ Test with sample text
6. ✅ Enjoy your voice generation! 🎤

---

## 📞 Additional Help

### Google Cloud Support
- Docs: https://cloud.google.com/text-to-speech/docs
- Pricing: https://cloud.google.com/text-to-speech/pricing
- Quotas: https://console.cloud.google.com/quotas

### LocalVoiceGenerator Help
- Check: README.md
- Check: QUICKSTART.md
- Check: DEVELOPMENT.md

---

## 🔐 Security Reminders

✅ **Keep your JSON key safe!**
- Never commit to git
- Never share
- Never post online
- Store in secure location
- Use file permissions (chmod 600 on Unix)

✅ **Rotate keys regularly**
- Every 3-6 months, create new key
- Delete old key from Google Cloud

✅ **Monitor usage**
- Check: https://console.cloud.google.com/billing
- Set up alerts for unusual usage

---

**You've got this! 🚀**

After completing these steps, your LocalVoiceGenerator will be ready to convert text to speech!

Need help? Check the troubleshooting section above or review the main documentation files.

**Happy Voice Generating! 🎤**
