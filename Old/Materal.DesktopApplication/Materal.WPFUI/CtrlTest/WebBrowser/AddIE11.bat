@echo off 
SET ApplicationName="Materal.WPFUI.exe"
%1 mshta vbscript:CreateObject("Shell.Application").ShellExecute("cmd.exe","/c %~s0 ::","","runas",1)(window.close)&&exit
REG ADD "HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION" /v %ApplicationName% /t REG_DWORD /d 11000