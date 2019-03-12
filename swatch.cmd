@echo off
pushd "%~dp0"
start "dotnet watch build [%~dp0]" dotnet watch build
start "mkdocs serve [%~dp0]" docker run --rm -it -p 8000:8000 -v "%~dp0:/docs" squidfunk/mkdocs-material
