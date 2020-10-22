$codeDir = "E:\Project\Materal\Project\Materal.APP\src\"#源码文件夹路径
$publishDir = "E:\Project\Materal\Application\Materal.APP\"#发布文件夹路径
$applicationSuffixs = "Server","WebAPP"#后缀名
$applicationPrefixs = "ConfigCenter.Environment"#前缀名 "Authority","ConfigCenter","ConfigCenter.Environment","Deploy","Materal.APP","WebAPP"
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
                    dotnet publish $csproj -o $targetDir -c $version
                    break
                }
            }
        }
    }
}
$datetTimeNow = Get-Date
$dateTimeNowString = $datetTimeNow.ToString('yyyyMMddHHmmss')
rar a -r -ep1 "$publishDir$dateTimeNowString.rar" "$publishDir"
Write "发布完毕 $publishDir"
explorer("$publishDir")