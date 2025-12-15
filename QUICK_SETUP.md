# Quick Setup Guide - Gemini API Key

## 🚀 Fastest Way to Fix the API Key Error

You have **3 options** to configure your Gemini API key:

---

## Option 1: Environment Variable (Recommended for Development)

### Windows (PowerShell):
```powershell
# Run this in PowerShell (one-time setup)
$env:GEMINI_API_KEY = "YOUR_ACTUAL_API_KEY_HERE"

# Or set permanently:
[System.Environment]::SetEnvironmentVariable("GEMINI_API_KEY", "YOUR_ACTUAL_API_KEY_HERE", "User")
```

### Windows (Command Prompt):
```cmd
setx GEMINI_API_KEY "YOUR_ACTUAL_API_KEY_HERE"
```

### Or use the provided script:
```powershell
.\SETUP_GEMINI_API_KEY.ps1
```

**Then restart your application!**

---

## Option 2: Update appsettings.Development.json

1. Open: `SMART_SMS_API/appsettings.Development.json`
2. Add or update:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Gemini": {
    "ApiKey": "YOUR_ACTUAL_API_KEY_HERE",
    "Model": "gemini-pro"
  }
}
```
3. Save and restart your application

---

## Option 3: Update appsettings.json

1. Open: `SMART_SMS_API/appsettings.json`
2. Find line 36 and replace:
```json
"ApiKey": "YOUR_ACTUAL_API_KEY_HERE",
```
3. Save and restart your application

---

## 📝 How to Get Your API Key

1. **Visit:** https://makersuite.google.com/app/apikey
2. **Sign in** with your Google account
3. **Click** "Create API Key"
4. **Copy** the generated key (starts with `AIzaSy...`)

---

## ✅ Verify It Works

After setting the API key, test the endpoint:

```bash
curl -X POST http://localhost:5283/api/chat/send \
  -H "Content-Type: application/json" \
  -d '{"message": "Hello"}'
```

You should get an AI response instead of an error!

---

## 🔒 Security Note

- **Never commit** your API key to Git
- Use environment variables for production
- The `appsettings.Development.json` file should be in `.gitignore`

---

## 🆘 Still Having Issues?

1. Make sure you **restarted** your application after setting the key
2. Check that the API key doesn't have extra spaces
3. Verify the key is active in Google AI Studio
4. Check the application logs for detailed error messages

