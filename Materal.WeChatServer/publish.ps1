$targetVersion = "Development"#148,Development
$codeDir = "E:\Project\Materal\Project\Materal.WeChatServer"
$publishDir = "E:\Project\Materal\Application\WeChatServer"
$version = "Release"#Debug Release
$applications = 
"Authority.IdentityServer","Authority.WebAPI",
"WeChatService.WebAPI",
"Log.WebAPI"
Remove-Item -Path:"$publishDir\*" -Recurse:$true
for($i=0;$i -lt $applications.Length; $i++){
    $application = $applications[$i]
    $csproj = "$codeDir\$application\$application.csproj"
    $targetDir = "$publishDir\$application"
    dotnet publish $csproj -o $targetDir -c $version
    ren $publishDir\$application\appsettings.$targetVersion.json $publishDir\$application\appsettings.json
}
$datetTimeNow = Get-Date
$dateTimeNowString = $datetTimeNow.ToString('yyyyMMddHHmmss')
rar a -r -ep1 "$publishDir\$dateTimeNowString.rar" "$publishDir\"
Write-Output "发布完毕 $publishDir"
explorer("$publishDir")
Write-Output "按任意键退出.........."
[Console]::ReadKey("?")