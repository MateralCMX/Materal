$nugetCacheDir = 'E:\Packages\NuGet\packages'
Get-ChildItem -Path $nugetCacheDir -Directory -Filter "materal.*" | Remove-Item -Recurse -Force
Write-Host "�����Materal Nuget������"
Get-ChildItem -Path $nugetCacheDir -Directory -Filter "RC.*" | Remove-Item -Recurse -Force
Write-Host "�����RC Nuget������"