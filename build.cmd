@echo off
setlocal
pushd "%~dp0"
dotnet build || goto :finally
docker run --rm -it -v "%~dp0:/docs" --entrypoint /bin/sh squidfunk/mkdocs-material -c "mkdocs build" || goto :finally
:finally
popd
exit /b %errorlevel%
