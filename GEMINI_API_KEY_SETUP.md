# How to Get and Configure Gemini API Key

## Step 1: Get Your Gemini API Key

1. **Visit Google AI Studio:**
   - Go to: https://makersuite.google.com/app/apikey
   - Or visit: https://aistudio.google.com/app/apikey

2. **Sign in with your Google Account:**
   - Use the same Google account you want to use for the API

3. **Create API Key:**
   - Click on "Create API Key" button
   - Select or create a Google Cloud project (if prompted)
   - Copy the generated API key (it will look like: `AIzaSy...`)

## Step 2: Configure in Your Application

1. **Open `appsettings.json`** in your project:
   ```
   SMART_SMS_API/appsettings.json
   ```

2. **Replace the placeholder:**
   ```json
   "Gemini": {
     "ApiKey": "YOUR_ACTUAL_API_KEY_HERE",
     "Model": "gemini-pro"
   }
   ```

   Example:
   ```json
   "Gemini": {
     "ApiKey": "AIzaSyAbCdEfGhIjKlMnOpQrStUvWxYz1234567",
     "Model": "gemini-pro"
   }
   ```

3. **Save the file**

4. **Restart your application** for the changes to take effect

## Step 3: Verify It Works

Test the endpoint:
```bash
curl -X POST http://localhost:5283/api/chat/send \
  -H "Content-Type: application/json" \
  -d '{"message": "Hello"}'
```

You should get a response from the AI instead of an API key error.

## Important Notes

- **Keep your API key secure** - Don't commit it to public repositories
- **API key format:** Should start with `AIzaSy` and be about 39 characters long
- **Free tier:** Google provides free API usage with rate limits
- **Model options:** You can use `gemini-pro` or `gemini-1.5-pro` (if available)

## Troubleshooting

### Error: "API key not valid"
- Make sure you copied the entire API key (no spaces, no line breaks)
- Verify the API key is active in Google AI Studio
- Check that you haven't exceeded your quota

### Error: "API key is missing"
- Verify the `appsettings.json` file has the correct structure
- Make sure you saved the file after editing
- Restart your application after making changes

### Still having issues?
- Check the application logs for detailed error messages
- Verify your internet connection
- Make sure the Gemini API is available in your region

