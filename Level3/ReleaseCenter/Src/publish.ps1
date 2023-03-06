$codeDir = "E:\Project\Materal\Project\Level3\ReleaseCenter\Src\"#Դ���ļ���·��
$publishDir = "E:\Project\Materal\Application\ReleaseCenter\"#�����ļ���·��

#$codeDir = "D:\Project\Materal\Project\Materal\Level3\ReleaseCenter\Src\"#Դ���ļ���·��
#$publishDir = "D:\Project\Materal\RCApplication\"#�����ļ���·��

$applicationSuffixs = "WebAPI","Web"#��׺��
$applicationPrefixs = "RC.Deploy"#ǰ׺��
#"RC.Authority","RC.Deploy","RC.ServerCenter","RC.EnvironmentServer"
$version = "Release"#Debug Release
Remove-Item -Path:"$publishDir\*" -Recurse:$true
Function GetChildItems(){
    if($applicationPrefixs.GetType().Name -eq "String"){
        if($applicationPrefixs -eq ""){
            $result = Get-ChildItem -Directory -Path $codeDir -Name
        }
        else{
            $result = Get-ChildItem -Directory -Path $codeDir -Name -Filter $applicationPrefixs
        }
    }
    else{
        $result = [System.Collections.ArrayList](Get-ChildItem -Directory -Path $codeDir -Name)
        for($i = 0; $i -lt $result.Count; $i++){
            $isContains = 0;
            for($j = 0; $j -lt $applicationPrefixs.Length; $j++){
                if($result[$i].Contains($applicationPrefixs[$j])){
                    $isContains = 1
                    break
                }
            }
            if($isContains -eq 0){
                $result.RemoveAt($i--)
            }
        }
    }
    return $result
}
$childItems = GetChildItems
foreach($childItem in $childItems){
    $projectDir = $codeDir + $childItem
    $projectChildItems = Get-ChildItem -Directory -Path $projectDir -Name
    foreach($projectChildItem in $projectChildItems){
        foreach($applicationSuffix in $applicationSuffixs){
            if($projectChildItem.Contains($applicationSuffix)){
                $indexOf = $projectChildItem.LastIndexOf($applicationSuffix)
                if($projectChildItem.Substring($indexOf) -eq $applicationSuffix){
                    $csproj = $projectDir + "\" + $projectChildItem + "\" + $projectChildItem + ".csproj"
                    $targetDir = $publishDir + $projectChildItem
                    Write "dotnet publish $csproj -o $targetDir -c $version"
                    dotnet publish $csproj -o $targetDir -c $version
                    break
                }
            }
        }
    }
}
$datetTimeNow = Get-Date
$dateTimeNowString = $datetTimeNow.ToString('yyyyMMddHHmmss')
#rar a -r -ep1 "$publishDir$dateTimeNowString.rar" "$publishDir"
Write "������� $publishDir"
explorer("$publishDir")