$codeDirPath = "D:\Project\Materal\Materal\Level5\ReleaseCenter\";#Դ���ļ���·��
$publishDirPath = "D:\Project\Materal\Application\ReleaseCenter\";#�����ļ���·��
$applicationSuffixs = @("WebAPI","Web");#��׺��
$applicationPrefixs = @("RC.Authority","RC.Deploy","RC.ServerCenter","RC.EnvironmentServer");#ǰ׺��
#"RC.Authority","RC.Deploy","RC.ServerCenter","RC.EnvironmentServer"
$createPackage = 1#�Ƿ񴴽�ѹ����
$createNugetPackage = 1#�Ƿ񴴽�Nuget��
$removeConfigFile = 1#�Ƿ��Ƴ������ļ�
$version = "Release"#Debug Release
$targetCsprojFilePaths = New-Object System.Collections.Generic.List[System.String];
Remove-Item -Path:"$publishDirPath\*" -Recurse:$true
$allSubfolders = [System.IO.DirectoryInfo[]](Get-ChildItem -Path $codeDirPath -Directory | Select-Object);
foreach ($subfolder in $allSubfolders) {
    if(!$applicationPrefixs.Contains($subfolder.Name)) { continue; }
    $projectFolders = [System.IO.DirectoryInfo[]](Get-ChildItem -Path $subfolder.FullName -Directory | Select-Object);
    foreach($projectFolder in $projectFolders) {
        foreach($applicationSuffix in $applicationSuffixs) {
            if(!$projectFolder.Name.EndsWith($applicationSuffix)) { continue; }
            $subFiles = [System.IO.FileInfo[]](Get-ChildItem -Path $projectFolder.FullName -File | Select-Object);
            foreach($subFile in $subFiles) {
                if(!$subFile.Name.EndsWith(".csproj")) { continue; }
                $targetDirPath = $publishDirPath + $projectFolder.Name;
                Write "��ʼ���� $projectFolder"
                dotnet publish $subFile.FullName -o $targetDirPath -c $version -f net8.0
                if($removeConfigFile){
                    $appsettingsFilePath = $targetDirPath + "\appsettings.json"
                    if(Test-Path $appsettingsFilePath){
                        Remove-Item -Path:$appsettingsFilePath -Recurse:$true
                    }
                    $RCConfigFilePath = $targetDirPath + "\RCConfig.json"
                    if(Test-Path $RCConfigFilePath){
                        Remove-Item -Path:$RCConfigFilePath -Recurse:$true
                    }
                }
            }
        }
    }
}
if($createNugetPackage){
     dotnet build "$codeDirPath\RC.ConfigClient\RC.ConfigClient" -c $version
}
if($createPackage){
    $datetTimeNow = Get-Date
    $dateTimeNowString = "ReleaseCenter" + $datetTimeNow.ToString('yyyyMMddHHmmss')
    rar a -r -ep1 "$publishDirPath$dateTimeNowString.rar" "$publishDirPath"
}
Write "������� $publishDirPath"
explorer("$publishDirPath")