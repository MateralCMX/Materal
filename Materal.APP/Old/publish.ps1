$codeDir = "E:\Project\Materal\Project\Materal.APP\"#Դ���ļ���·��
$publishDir = "E:\Project\Materal\Application\Materal.APP\"#�����ļ���·��
$version = "Release"#Debug Release
#�Ƴ�������
Remove-Item -Path:"$publishDir\*" -Recurse:$true
#Web��վ
$csproj = $codeDir + "BlazorWebAPP\BlazorWebAPP\BlazorWebAPP.csproj"
$targetDir = $publishDir + "BlazorWebAPP"
dotnet publish $csproj -o $targetDir -c $version
#���ķ���
$csproj = $codeDir + "Materal.APP.Server\Materal.APP.Server.csproj"
$targetDir = $publishDir + "Materal.APP.Server"
dotnet publish $csproj -o $targetDir -c $version
#Ȩ�޷���
$csproj = $codeDir + "Authority\Authority.Server\Authority.Server.csproj"
$targetDir = $publishDir + "Authority.Server"
dotnet publish $csproj -o $targetDir -c $version
#���
$datetTimeNow = Get-Date
$dateTimeNowString = $datetTimeNow.ToString('yyyyMMddHHmmss')
rar a -r -ep1 "$publishDir$dateTimeNowString.rar" "$publishDir"
Write "������� $publishDir"
explorer("$publishDir")