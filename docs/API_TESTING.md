# API Examples & Testing Guide

## 🧪 Testing LocalVoiceGenerator APIs

This guide shows how to test the voice generation APIs using various tools.

## API Endpoints

### 1. Preview Audio (Inline Preview)

**Endpoint:** `POST /Voice/Preview`

**Purpose:** Generate audio and return as base64 for inline preview

**Content-Type:** `application/json`

#### Example Request

```json
{
  "text": "Hello, this is a test of the voice system",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}
```

#### Success Response

```json
{
  "success": true,
  "audio": "//NExAAqAGgAVAAQAEQAVABEAAAA...base64-encoded-mp3...AAAAAASUVORK5CYII="
}
```

#### Error Response

```json
{
  "success": false,
  "error": "Text exceeds maximum length of 5000 characters."
}
```

---

### 2. Generate & Download Audio (Download MP3)

**Endpoint:** `POST /Voice/Generate`

**Purpose:** Generate audio and return as downloadable MP3 file

**Content-Type:** `application/json`

#### Example Request

```json
{
  "text": "नमस्ते, यह हिंदी में एक परीक्षण है",
  "languageCode": "hi-IN",
  "voiceType": "wavenet",
  "speakingRate": 0.8
}
```

#### Success Response

- **Status:** 200 OK
- **Content-Type:** `audio/mpeg`
- **Content-Disposition:** `attachment; filename="voiceover.mp3"`
- **Body:** Binary MP3 file

#### Error Response

```json
{
  "success": false,
  "error": "Please enter some text to convert."
}
```

---

## 📝 Testing with cURL

### Test Preview Endpoint

**macOS/Linux:**
```bash
curl -X POST https://localhost:7125/Voice/Preview \
  -H "Content-Type: application/json" \
  --insecure \
  -d '{
    "text": "Hello world",
    "languageCode": "en-IN",
    "voiceType": "wavenet",
    "speakingRate": 1.0
  }'
```

**Windows PowerShell:**
```powershell
$body = @{
    text = "Hello world"
    languageCode = "en-IN"
    voiceType = "wavenet"
    speakingRate = 1.0
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7125/Voice/Preview" `
  -Method POST `
  -Headers @{"Content-Type"="application/json"} `
  -Body $body `
  -SkipCertificateCheck
```

### Test Generate Endpoint

**macOS/Linux:**
```bash
curl -X POST https://localhost:7125/Voice/Generate \
  -H "Content-Type: application/json" \
  --insecure \
  -d '{
    "text": "This is a test download",
    "languageCode": "en-IN",
    "voiceType": "wavenet",
    "speakingRate": 1.0
  }' \
  -o output.mp3
```

**Windows PowerShell:**
```powershell
$body = @{
    text = "This is a test download"
    languageCode = "en-IN"
    voiceType = "wavenet"
    speakingRate = 1.0
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7125/Voice/Generate" `
  -Method POST `
  -Headers @{"Content-Type"="application/json"} `
  -Body $body `
  -SkipCertificateCheck `
  -OutFile "output.mp3"
```

---

## 📮 Testing with Postman

### Import Collection

1. Open Postman
2. File → Import
3. Create new Collection called "LocalVoiceGenerator"

### Create Preview Request

**Tab 1: Preview**

| Field | Value |
|-------|-------|
| Method | POST |
| URL | `https://localhost:7125/Voice/Preview` |
| Header: Content-Type | `application/json` |
| SSL Verification | OFF (for localhost) |

**Body (Raw JSON):**
```json
{
  "text": "Hello, this is a preview test",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}
```

### Create Generate Request

**Tab 2: Generate**

| Field | Value |
|-------|-------|
| Method | POST |
| URL | `https://localhost:7125/Voice/Generate` |
| Header: Content-Type | `application/json` |
| SSL Verification | OFF (for localhost) |

**Body (Raw JSON):**
```json
{
  "text": "This is a download test",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}
```

**After Send:**
- Click "Save Response" → "Save as File"
- Filename: `voiceover.mp3`

---

## 🔌 Testing with VS Code REST Client

### Create test-api.rest file

Create a file named `test-api.rest` in project root:

```rest
### Variables
@baseUrl = https://localhost:7125
@contentType = application/json

### 1. Preview English Audio
POST {{baseUrl}}/Voice/Preview
Content-Type: {{contentType}}

{
  "text": "Hello, this is an English preview test. The speaking rate is normal.",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

### 2. Preview Hindi Audio
POST {{baseUrl}}/Voice/Preview
Content-Type: {{contentType}}

{
  "text": "नमस्ते, यह एक हिंदी पूर्वावलोकन परीक्षण है",
  "languageCode": "hi-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

### 3. Preview Slower English
POST {{baseUrl}}/Voice/Preview
Content-Type: {{contentType}}

{
  "text": "This is slower speech for better clarity",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 0.5
}

### 4. Preview Faster English
POST {{baseUrl}}/Voice/Preview
Content-Type: {{contentType}}

{
  "text": "This is faster speech at double speed",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 2.0
}

### 5. Download English Audio
POST {{baseUrl}}/Voice/Generate
Content-Type: {{contentType}}

{
  "text": "This audio will be downloaded as an MP3 file",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

### 6. Download Hindi Audio
POST {{baseUrl}}/Voice/Generate
Content-Type: {{contentType}}

{
  "text": "यह ऑडियो एक एमपी 3 फाइल के रूप में डाउनलोड किया जाएगा",
  "languageCode": "hi-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

### 7. Error Test - Empty Text
POST {{baseUrl}}/Voice/Preview
Content-Type: {{contentType}}

{
  "text": "",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

### 8. Error Test - Max Characters Exceeded
POST {{baseUrl}}/Voice/Preview
Content-Type: {{contentType}}

{
  "text": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 1.0
}

### 9. Error Test - Invalid Speaking Rate
POST {{baseUrl}}/Voice/Preview
Content-Type: {{contentType}}

{
  "text": "Test with invalid speaking rate",
  "languageCode": "en-IN",
  "voiceType": "wavenet",
  "speakingRate": 3.0
}

### 10. Standard Voice Type
POST {{baseUrl}}/Voice/Preview
Content-Type: {{contentType}}

{
  "text": "This uses standard voice type instead of wavenet",
  "languageCode": "en-IN",
  "voiceType": "standard",
  "speakingRate": 1.0
}
```

### Usage in VS Code

1. Install extension: "REST Client" by Huachao Mao
2. Create `test-api.rest` file
3. Copy content from above
4. Click "Send Request" above each request
5. View response in right panel

---

## 🧪 JavaScript Fetch Examples

### Test in Browser Console

**Preview Request:**
```javascript
const options = {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    text: 'Hello world, this is a test',
    languageCode: 'en-IN',
    voiceType: 'wavenet',
    speakingRate: 1.0
  })
};

fetch('/Voice/Preview', options)
  .then(response => response.json())
  .then(data => {
    if (data.success) {
      console.log('Audio generated!');
      // Create audio element
      const audio = document.createElement('audio');
      audio.src = 'data:audio/mpeg;base64,' + data.audio;
      audio.play();
    } else {
      console.error('Error:', data.error);
    }
  })
  .catch(error => console.error('Request failed:', error));
```

**Download Request:**
```javascript
const options = {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    text: 'This text will be downloaded as MP3',
    languageCode: 'en-IN',
    voiceType: 'wavenet',
    speakingRate: 1.0
  })
};

fetch('/Voice/Generate', options)
  .then(response => response.blob())
  .then(blob => {
    // Create download link
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'voiceover.mp3';
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);
    document.body.removeChild(a);
  })
  .catch(error => console.error('Request failed:', error));
```

---

## ✅ Test Scenarios

### Scenario 1: Basic English Generation

```
Text: "Hello, welcome to LocalVoiceGenerator"
Language: English (en-IN)
Voice Type: Wavenet
Speaking Rate: 1.0x
Expected: Smooth, clear English audio
```

### Scenario 2: Hindi Generation

```
Text: "नमस्ते, लोकल वॉयस जेनरेटर में आपका स्वागत है"
Language: Hindi (hi-IN)
Voice Type: Wavenet
Speaking Rate: 1.0x
Expected: Natural Hindi audio
```

### Scenario 3: Slow Speech

```
Text: "This is slow speech"
Language: English
Voice Type: Wavenet
Speaking Rate: 0.5x
Expected: Clearly articulated, slower audio
```

### Scenario 4: Fast Speech

```
Text: "This is fast speech"
Language: English
Voice Type: Wavenet
Speaking Rate: 2.0x
Expected: Rapid, high-speed audio
```

### Scenario 5: Empty Input (Error)

```
Text: ""
Expected: Error - "Please enter some text to convert."
Status: 400
```

### Scenario 6: Exceeds Max Length (Error)

```
Text: [5001+ characters]
Expected: Error - "Text exceeds maximum length of 5000 characters."
Status: 400
```

### Scenario 7: Invalid Speaking Rate (Error)

```
Speaking Rate: 2.5
Expected: Error - "Speaking rate must be between 0.5 and 2.0."
Status: 400
```

---

## 📊 Response Time Guidelines

| Text Length | Typical Response Time | Quality |
|-------------|----------------------|---------|
| < 100 chars | 1-2 seconds | Wavenet |
| 100-500 chars | 2-3 seconds | Wavenet |
| 500-1000 chars | 3-5 seconds | Wavenet |
| 1000-5000 chars | 5-10 seconds | Wavenet |

---

## 🔍 Debugging Tips

### Enable Verbose Logging

In **appsettings.Development.json**:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Debug"
    }
  }
}
```

### Check API Credentials

```bash
# Verify environment variable
echo $GOOGLE_APPLICATION_CREDENTIALS  # macOS/Linux
echo $env:GOOGLE_APPLICATION_CREDENTIALS  # Windows PowerShell

# Test Google Cloud credentials
gcloud auth list
```

### Monitor Network Requests

- Open browser DevTools (F12)
- Go to Network tab
- Make request
- Check request/response headers
- View response body

### Common HTTP Status Codes

| Code | Meaning | Solution |
|------|---------|----------|
| 200 | Success | Audio generated correctly |
| 400 | Bad Request | Check input validation |
| 401 | Unauthorized | Check Google credentials |
| 403 | Forbidden | Check service account permissions |
| 500 | Server Error | Check server logs |
| 503 | Service Unavailable | Check API quota/limits |

---

## 🚀 Performance Testing

### Load Testing with Apache Bench

```bash
# Test 100 requests with 10 concurrent
ab -n 100 -c 10 -H "Content-Type: application/json" \
  -d '{"text":"test","languageCode":"en-IN","voiceType":"wavenet","speakingRate":1.0}' \
  https://localhost:7125/Voice/Preview
```

### Monitor Resource Usage

```bash
# macOS
top -p $(pgrep -f "dotnet run")

# Linux
ps aux | grep dotnet

# Windows Task Manager
# Ctrl+Shift+Esc
```

---

## 📋 Checklist for Testing

- [ ] Preview endpoint works
- [ ] Generate/Download endpoint works
- [ ] English language generates audio
- [ ] Hindi language generates audio
- [ ] Speaking rate 0.5x works
- [ ] Speaking rate 2.0x works
- [ ] Empty text shows error
- [ ] Long text (>5000 chars) shows error
- [ ] Invalid speaking rate shows error
- [ ] Audio quality is good
- [ ] Download filename is correct
- [ ] Response times are acceptable

---

**Happy Testing! 🎉**

For more information, refer to the main README.md and DEVELOPMENT.md files.
