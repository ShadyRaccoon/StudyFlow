#!/bin/bash

echo "Setting up StudyFlow API..."

# .NET SDK 10
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --version 10.0.109
export PATH=$PATH:$HOME/.dotnet
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc

# restore nuget packages
cd StudyFlow_API
dotnet restore

# sqlite
sudo apt install -y sqlite3

# user secrets
dotnet user-secrets init
echo ""
read -p "Enter your Anthropic API key: " api_key
dotnet user-secrets set "ANTHROPIC_API_KEY" "$api_key"

# db migration
dotnet ef database update

echo ""
echo "Setup complete. Run with: dotnet run"