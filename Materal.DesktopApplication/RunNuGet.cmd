cd Materal.WPFCommon
nuget pack Materal.WPFCommon.csproj -Properties Configuration=Release -OutputDirectory bin\Release
cd ../Materal.WPFCustomControlLib
nuget pack Materal.WPFCustomControlLib.csproj -Properties Configuration=Release -OutputDirectory bin\Release
pause