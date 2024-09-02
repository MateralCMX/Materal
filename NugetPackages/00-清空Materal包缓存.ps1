$nugetCacheDir = 'E:\Packages\NuGet\packages'
Get-ChildItem -Path $nugetCacheDir -Directory -Filter "materal.*" | Remove-Item -Recurse -Force