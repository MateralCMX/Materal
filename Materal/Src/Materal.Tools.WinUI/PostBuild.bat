@echo off
set outputPath=%cd%\bin\x64\Release\net8.0-windows10.0.19041.0

set destinationDirectory=%cd%
for %%i in ("%destinationDirectory%") do set destinationDirectory=%%~dpi
set destinationDirectory=%destinationDirectory:~0,-1%
for %%i in ("%destinationDirectory%") do set destinationDirectory=%%~dpi
set destinationDirectory=%destinationDirectory:~0,-1%
for %%i in ("%destinationDirectory%") do set destinationDirectory=%%~dpi
set destinationDirectory=%destinationDirectory:~0,-1%

set destinationDirectory=%destinationDirectory%\Publish\Materal.Tools.WinUI
if exist "%destinationDirectory%" rmdir /S /Q "%destinationDirectory%"

xcopy "%outputPath%\*" "%destinationDirectory%" /E /I /Y