@ECHO OFF

IF EXIST %~dp0artifacts\publish rmdir /S /Q %~dp0artifacts\publish
Path=%~dp0src\Dorothy\node_modules\.bin;C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\\EXTENSIONS\MICROSOFT\WEB TOOLS\External;%PATH%;C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\\EXTENSIONS\MICROSOFT\WEB TOOLS\External\git

@ECHO ON
CALL %HOMEDRIVE%%HOMEPATH%\.dnx\runtimes\dnx-clr-win-x86.1.0.0-beta8\bin\dnu.cmd publish "%~dp0src\Dorothy" --out "%~dp0artifacts\publish" --configuration Release --no-source --runtime dnx-clr-win-x64.1.0.0-beta8 --wwwroot-out "wwwroot" --quiet

PAUSE



SET server=http://85.93.17.211
SET siteName=Dorothy
SET username=Administrator

ECHO Publishing to %server%...
SET /p password=Enter password for %username%: 

CALL "C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe" -verb:sync -source:recycleApp  -dest:recycleApp=%siteName%,recycleMode=StopAppPool,computername=%server%/MSDEPLOYAGENTSERVICE,username=%username%,password=%password%
CALL "C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe" -verb:sync -source:contentPath=%~dp0artifacts\publish\wwwroot -dest:contentPath=%siteName%,computername=%server%/MSDEPLOYAGENTSERVICE,username=%username%,password=%password%
CALL "C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe" -verb:sync -source:contentPathLib=%~dp0artifacts\publish\approot -dest:contentPathLib=%siteName%,computername=%server%/MSDEPLOYAGENTSERVICE,username=%username%,password=%password%
CALL "C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe" -verb:sync -source:recycleApp  -dest:recycleApp=%siteName%,recycleMode=StartAppPool,computername=%server%/MSDEPLOYAGENTSERVICE,username=%username%,password=%password%

PAUSE