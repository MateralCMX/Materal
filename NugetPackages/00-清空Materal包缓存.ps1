$nugetCacheDir = 'E:\Packages\NuGet\packages'
Get-ChildItem -Path $nugetCacheDir -Directory -Filter "materal.*" | Remove-Item -Recurse -Force
Write-Host "已清空Materal Nuget包缓存"
Get-ChildItem -Path $nugetCacheDir -Directory -Filter "RC.*" | Remove-Item -Recurse -Force
Write-Host "已清空RC Nuget包缓存"