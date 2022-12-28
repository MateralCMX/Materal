dotnet pack
dotnet tool uninstall -g ProjectClear
dotnet tool install -g --add-source .\nupkg ProjectClear
pause