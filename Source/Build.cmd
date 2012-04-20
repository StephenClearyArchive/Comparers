@echo off
if not exist ..\Binaries mkdir ..\Binaries
if not exist ..\Binaries\NuGet mkdir ..\Binaries\NuGet
devenv Comparers.sln /rebuild Release
..\Util\nuget.exe pack -sym Comparers.nuspec -o ..\Binaries\NuGet
pause