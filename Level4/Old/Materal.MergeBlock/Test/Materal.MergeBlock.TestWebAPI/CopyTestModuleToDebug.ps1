$codeDirPath = "D:\Project\Materal\Materal\Level4\Materal.MergeBlock\Src\"; #Դ���ļ���·��
$codeDirPath2 = "D:\Project\Materal\Materal\Level4\Materal.MergeBlock\Test\"; #Դ���ļ���·��
$publishDirPath = "D:\Project\Materal\Materal\Level4\Materal.MergeBlock\Test\Materal.MergeBlock.TestWebAPI\bin\Debug\net8.0\Modules\"; #�����ļ���·��
$moduleName = @("AccessLog", "Consul", "EventBus", "ExceptionInterceptor", "HelloWorld", "Logger", "Swagger"); #ģ������
#"AccessLog", "Authorization", "Consul", "DependencyInjection", "EventBus", "ExceptionInterceptor", "HelloWorld", "Logger", "Oscillator", "Swagger"
$version = "Debug"#Debug Release
Remove-Item -Path:"$publishDirPath\*" -Recurse:$true
$allSubfolders = [System.IO.DirectoryInfo[]](Get-ChildItem -Path $codeDirPath -Directory | Select-Object);
for ($i = 0; $i -lt $moduleName.Count; $i++) {
    $moduleName[$i] = "Materal.MergeBlock." + $moduleName[$i]
}
foreach ($subfolder in $allSubfolders) {
    if (!$moduleName.Contains($subfolder.Name)) { continue; }
    $subFiles = [System.IO.FileInfo[]](Get-ChildItem -Path $subfolder.FullName -File | Select-Object);
    foreach ($subFile in $subFiles) {
        if (!$subFile.Name.EndsWith(".csproj")) { continue; }
        $targetDirPath = $publishDirPath + $subfolder.Name;
        Write-Output "��ʼ���� $subfolder"
        dotnet publish $subFile.FullName -o $targetDirPath -c $version
    }
}
$allSubfolders = [System.IO.DirectoryInfo[]](Get-ChildItem -Path $codeDirPath2 -Directory | Select-Object);
for ($i = 0; $i -lt $moduleName.Count; $i++) {
    $moduleName[$i] = $moduleName[$i]+"Test"
}
foreach ($subfolder in $allSubfolders) {
    if (!$moduleName.Contains($subfolder.Name)) { continue; }
    $subFiles = [System.IO.FileInfo[]](Get-ChildItem -Path $subfolder.FullName -File | Select-Object);
    foreach ($subFile in $subFiles) {
        if (!$subFile.Name.EndsWith(".csproj")) { continue; }
        $targetDirPath = $publishDirPath + $subfolder.Name;
        Write-Output "��ʼ���� $subfolder"
        dotnet publish $subFile.FullName -o $targetDirPath -c $version
    }
}
Write-Output "������� $publishDirPath"
explorer("$publishDirPath")