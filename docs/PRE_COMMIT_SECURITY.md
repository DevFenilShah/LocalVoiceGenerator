# 🔐 Pre-Commit Security Checklist

Use this checklist before committing code to ensure no credentials are exposed.

---

## ✅ Credential Security Checklist

### 1. No Hardcoded Secrets
- [ ] Search for `api-project` strings → Should find 0 results
- [ ] Search for `private_key` → Should find 0 results
- [ ] Search for `AKIA` (AWS keys) → Should find 0 results
- [ ] Search for base64-encoded credentials → Should find 0 results

### 2. gitignore Configuration
- [ ] `.gitignore` includes `*.json` credential files
- [ ] `.gitignore` includes `.env` files
- [ ] `.gitignore` includes environment variable files
- [ ] `.gitignore` includes service account keys

### 3. Configuration Files
- [ ] `appsettings.json` contains no credentials ✅
- [ ] `appsettings.Development.json` contains no credentials ✅
- [ ] `appsettings.example.json` contains no credentials ✅
- [ ] `launchSettings.json` contains no credentials ✅

### 4. Application Code
- [ ] `VoiceService.cs` uses environment variables (not hardcoded paths)
- [ ] `VoiceController.cs` doesn't log credentials
- [ ] No credentials in connection strings
- [ ] No credentials in comments

### 5. Documentation
- [ ] Documentation doesn't include real project IDs
- [ ] Examples use placeholder values (`YOUR-PROJECT-ID`, etc.)
- [ ] Setup docs reference environment variables
- [ ] Security best practices documented

### 6. Run Scripts
- [ ] `run.sh` uses variable path (not hardcoded)
- [ ] `run.sh` path is documented
- [ ] Users must update path for their credentials

---

## 🔍 Run These Commands to Verify

### Check for Hardcoded Credentials
```bash
cd "/Users/richashah/Voice Gen/LocalVoiceGenerator"

# Search for Google Cloud project IDs
grep -r "api-project-" . --include="*.cs" --include="*.json" --include="*.cshtml" \
  | grep -v "docs/" | grep -v "bin/" | grep -v ".git/"

# Expected output: EMPTY (no results)
```

### Check for API Keys
```bash
# Search for AWS keys (start with AKIA)
grep -r "AKIA" . --include="*.cs" --include="*.json"

# Search for private keys
grep -r "private_key\|PRIVATE_KEY" . --include="*.cs" --include="*.json" \
  | grep -v "docs/" | grep -v "bin/"

# Expected output: EMPTY (no results)
```

### Verify .gitignore
```bash
# List files that will NOT be committed
git check-ignore -v *

# Should include:
# - Credential JSON files
# - .env files
# - Environment-specific settings
```

### Git Status Check
```bash
# See what's staged for commit
git status

# IMPORTANT: Make sure NO credential files are listed
# If you see .json files with credentials, add to .gitignore and run:
git rm --cached <file>
```

---

## 📋 Before Each Commit

### Step 1: Run Security Checks
```bash
# Quick security scan
bash -c '
echo "🔍 Searching for credentials..."
if grep -r "api-project-\|AKIA\|private_key" . --include="*.cs" --include="*.json" \
  | grep -v "docs/" | grep -v "bin/" | grep -v ".git/"; then
  echo "❌ FOUND CREDENTIALS! Do not commit!"
  exit 1
else
  echo "✅ No credentials found"
  exit 0
fi
'
```

### Step 2: Verify .gitignore
```bash
# List all files that would be committed
git add .
git status --short

# NEVER commit .json credential files
# If you see them, remove from staging:
git reset HEAD <file>
```

### Step 3: Review Changes
```bash
# Preview changes before committing
git diff --staged
```

### Step 4: Commit Safely
```bash
git commit -m "Your commit message"
```

---

## 🚨 If You Accidentally Committed Credentials

### Immediate Actions
```bash
# 1. Regenerate the compromised credentials immediately!

# 2. Remove from git history
git rm --cached <credential-file>
git commit --amend

# 3. Or rewrite entire history (DANGEROUS - only if not pushed)
git filter-branch --tree-filter 'rm -f <file>' HEAD

# 4. Force push (only if local, not shared)
git push --force-with-lease
```

### Long-term Actions
1. ✅ Regenerate ALL Google Cloud credentials
2. ✅ Rotate ALL API keys
3. ✅ Audit access logs
4. ✅ Notify team immediately
5. ✅ Update security policies

---

## ✅ Safe Credential Patterns

### ✅ GOOD - Environment Variables
```csharp
// VoiceService.cs
var credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
```

### ✅ GOOD - Documentation Examples
```markdown
# Set your credentials path:
export GOOGLE_APPLICATION_CREDENTIALS="/path/to/your/credentials.json"
```

### ❌ BAD - Hardcoded Paths
```csharp
var credentialsPath = "/Users/richashah/Downloads/api-project-123.json";
```

### ❌ BAD - Hardcoded Keys
```csharp
var apiKey = "AIzaSy..."; // NEVER DO THIS
```

### ❌ BAD - In Config Files
```json
{
  "googleCredentials": "api-project-123.json"
}
```

---

## 📚 Related Documentation

- **[SETUP.md](./SETUP.md)** - Setup for new users
- **[CREDENTIALS_STORAGE_OPTIONS.md](./CREDENTIALS_STORAGE_OPTIONS.md)** - Credential storage methods
- **[GOOGLE_CLOUD_SETUP.md](./GOOGLE_CLOUD_SETUP.md)** - Google Cloud setup

---

## 🎯 Final Checklist Before Committing

- [ ] All security checks passed ✅
- [ ] No hardcoded credentials found ✅
- [ ] .gitignore is configured ✅
- [ ] No credential files staged ✅
- [ ] Documentation is updated ✅
- [ ] Team is aware of changes ✅
- [ ] Ready to commit ✅

```bash
# When all checks pass:
git commit -m "Your changes"
git push origin main
```

---

**Remember:** Security is everyone's responsibility! 🔐
