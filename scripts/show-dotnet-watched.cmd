@echo off
setlocal
pushd "%~dp0.."
set TEMP_FILE_PATH=%TEMP%\dotnet-watch-list-%random%.txt
dotnet msbuild -v:n ^
               -t:GenerateWatchList ^
               "-p:_DotNetWatchListFile=%TEMP_FILE_PATH%" ^
    && type "%TEMP_FILE_PATH%" ^
    && del "%TEMP_FILE_PATH%"
popd
