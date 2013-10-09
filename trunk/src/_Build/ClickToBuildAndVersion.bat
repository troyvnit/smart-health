@echo off
@SET PATH=%windir%\Microsoft.NET\Framework\v4.0.30319\;%PATH%

@SET IsDesktopBuild="true"
@SET /p version= Version : 

@SET properties=IsDesktopBuild="%IsDesktopBuild%"
@SET properties=BUILD_NUMBER="%version%";%properties%

msbuild Build.proj /p:%properties%
pause