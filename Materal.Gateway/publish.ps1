$targetVersion = "Development"#125 129 Development
$codeDir = "E:\Project\Materal\Project\Materal.Gateway"
$publishDir = "E:\Project\Materal\Application\Gateway"
$version = "Release"#Debug Release
$application = 
"Gateway"
Remove-Item -Path:"$publishDir\*" -Recurse:$true
$csproj = "$codeDir\$application\$application.csproj"
$targetDir = "$publishDir\$application"
dotnet publish $csproj -o $targetDir -c $version
ren $publishDir\$application\ocelot.$targetVersion.json $publishDir\$application\ocelot.json
$datetTimeNow = Get-Date
$dateTimeNowString = $datetTimeNow.ToString('yyyyMMddHHmmss')
rar a -r -ep1 "$publishDir\$dateTimeNowString.rar" "$publishDir\"
Write-Output "发布完毕 $publishDir"
explorer("$publishDir")
Write-Output "按任意键退出.........."
[Console]::ReadKey("?")