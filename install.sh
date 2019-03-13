#!/usr/bin/env bash
set -e
cd "$(dirname "$0")"
dotnet tool install dotnet-script         --version 0.28.0 --tool-path tools
dotnet tool install MarkdownSnippets.Tool --version  9.1.0 --tool-path tools
