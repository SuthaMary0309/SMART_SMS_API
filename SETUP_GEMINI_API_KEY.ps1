# PowerShell Script to Set Gemini API Key
# Run this script to set your Gemini API key as an environment variable

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Gemini API Key Setup" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Prompt for API key
$apiKey = Read-Host "Enter your Gemini API Key (get it from https://makersuite.google.com/app/apikey)"

if ([string]::IsNullOrWhiteSpace($apiKey)) {
    Write-Host "Error: API key cannot be empty!" -ForegroundColor Red
    exit 1
}

# Set environment variable for current session
$env:GEMINI_API_KEY = $apiKey
Write-Host ""
Write-Host "✓ API key set for current session" -ForegroundColor Green
Write-Host ""

# Ask if user wants to set it permanently
$setPermanent = Read-Host "Do you want to set this permanently for your user? (Y/N)"

if ($setPermanent -eq "Y" -or $setPermanent -eq "y") {
    [System.Environment]::SetEnvironmentVariable("GEMINI_API_KEY", $apiKey, "User")
    Write-Host "✓ API key set permanently for your user account" -ForegroundColor Green
    Write-Host "  (You may need to restart your IDE/terminal for it to take effect)" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Setup Complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "You can now restart your application and the API key will be used." -ForegroundColor Yellow
Write-Host ""

