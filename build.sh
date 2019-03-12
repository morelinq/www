#!/usr/bin/env bash
set -e
cd "$(dirname "$0")"
dotnet build
docker run --rm -it -v "$(pwd):/docs" --entrypoint /bin/sh squidfunk/mkdocs-material -c "mkdocs build"
