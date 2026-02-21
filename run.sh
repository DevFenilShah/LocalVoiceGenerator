#!/bin/bash

# LocalVoiceGenerator - Easy Run Script
# This script sets up the environment and runs the application

# Source the shell configuration to get environment variables
source ~/.zshrc

# Set the Google Cloud credentials path
export GOOGLE_APPLICATION_CREDENTIALS="/Users/richashah/Downloads/api-project-972748005580-cb5aee2857bc.json"

# Verify the credentials file exists
if [ ! -f "$GOOGLE_APPLICATION_CREDENTIALS" ]; then
    echo "❌ Error: Credentials file not found at:"
    echo "   $GOOGLE_APPLICATION_CREDENTIALS"
    echo ""
    echo "Please download your Google Cloud credentials and place them at that path."
    exit 1
fi

echo "✅ Google Cloud credentials loaded"
echo "📂 Credentials: $GOOGLE_APPLICATION_CREDENTIALS"
echo ""
echo "🚀 Starting LocalVoiceGenerator..."
echo "📍 Access the app at: http://localhost:5033"
echo ""
echo "Press Ctrl+C to stop the server"
echo ""

# Run the dotnet application
cd "$(dirname "$0")" && dotnet run --launch-profile http
