$codeDir = "E:\Project\Materal\Project\Materal.APP\"#源码文件夹路径
$publishDir = "E:\Project\Materal\Application\Materal.APP\"#发布文件夹路径
$version = "Release"#Debug Release
#移除老数据
Remove-Item -Path:"$publishDir\*" -Recurse:$true
#Web网站
$csproj = $codeDir + "BlazorWebAPP\BlazorWebAPP\BlazorWebAPP.csproj"
$targetDir = $publishDir + "BlazorWebAPP"
dotnet publish $csproj -o $targetDir -c $version
#核心服务
$csproj = $codeDir + "Materal.APP.Server\Materal.APP.Server.csproj"
$targetDir = $publishDir + "Materal.APP.Server"
dotnet publish $csproj -o $targetDir -c $version
#权限服务
$csproj = $codeDir + "Authority\Authority.Server\Authority.Server.csproj"
$targetDir = $publishDir + "Authority.Server"
dotnet publish $csproj -o $targetDir -c $version
#打包
$datetTimeNow = Get-Date
$dateTimeNowString = $datetTimeNow.ToString('yyyyMMddHHmmss')
rar a -r -ep1 "$publishDir$dateTimeNowString.rar" "$publishDir"
Write "发布完毕 $publishDir"
explorer("$publishDir")