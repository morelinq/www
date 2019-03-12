#!/usr/bin/env bash
set -e
cd "$(dirname "$0")"
if [ "$TERM_PROGRAM" = "Apple_Terminal" ]; then
    cmd1="cd \\\"$(pwd)\\\"; dotnet watch build"
    cmd2="docker run --rm -it -p 8000:8000 -v \\\"$(pwd):/docs\\\" squidfunk/mkdocs-material"
    osascript -e "tell application \"Terminal\" to do script \"$cmd1\""
    osascript -e "tell application \"Terminal\" to do script \"$cmd2\""
else
    echo "Run these commands in separate terminals:"
    echo "cd \"$(pwd)\"; dotnet watch build"
    echo "docker run --rm -it -p 8000:8000 -v \"$(pwd):/docs\" squidfunk/mkdocs-material"
fi
