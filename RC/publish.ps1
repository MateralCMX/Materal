$codeDirPath = "E:\Project\Materal\Materal\RC";#源码地址
$publishDirPath = "E:\Project\Materal\Materal\Publish\RC\Plugins\";#发布地址
$applicationSuffixs = @("Application");#项目后缀
$applicationPrefixs = @("RC.Authority","RC.Deploy","RC.EnvironmentServer","RC.ServerCenter");#项目前缀
#"RC.Authority","RC.Deploy","RC.EnvironmentServer","RC.ServerCenter"
$createPackage = 0#创建压缩包
$version = "Release"#Debug Release
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
                Write-Host "寮€濮嬪彂甯?$projectFolder"
                dotnet publish $subFile.FullName -o $targetDirPath -c $version
                if($createPackage){
                    $datetTimeNow = Get-Date
                    $dateTimeNowString = $projectFolder.Name + $datetTimeNow.ToString('yyyyMMddHHmmss')
                    rar a -r -ep1 "$publishDirPath$dateTimeNowString.rar" "$targetDirPath"
                }
            }
        }
    }
}
Write-Host "鍙戝竷瀹屾瘯 $publishDirPath"
explorer("$publishDirPath")