set winmergePath=winmergeu.exe
set serverInstallPath=D:\Installers\Release 1.0\serverInstaller
set backofficePath=D:\svnrepo\InMenu6.0.1
set patchpath=D:\Installers\Release 6.1\Patch\BO Patch
set InstallerInMenuFilesPath=D:\Installers\Release 6.1\serverInstaller
set BOEncryptionPath=D:\PackageForEncryption With Extension Plus
set SmartAssemblyPath=C:\Program Files\{smartassembly}\{smartassembly}.exe


start /wait %winmergePath% /r "%backofficePath%\InMenu\bin\x86\Release" "%InstallerInMenuFilesPath%\InMenuFiles"

start /wait %winmergePath% /r "%InstallerInMenuFilesPath%\InMenuFiles" "%BOEncryptionPath%\originalFiles"

start /wait %winmergePath% /r "%InstallerInMenuFilesPath%\InMenuFiles" "%patchpath%\InMenuFiles"
