@echo off
@SET PATH=%windir%\Microsoft.NET\Framework\v4.0.30319\;%PATH%

msbuild _Build/Build.proj
pause