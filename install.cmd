@echo off
pushd "%~dp0"
call :install dotnet-script 0.28.0 || goto :end
call :install MarkdownSnippets.Tool 9.1.0 || goto :end
:end
exit /b %errorlevel

:install
dotnet tool install %1 --version %2 --tool-path tools
exit /b %errorlevel
