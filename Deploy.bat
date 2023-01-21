@echo off
dotnet pack
mkdir NugetOutput

echo The .nupkg file has been built!  Please copy it to NugetOuput, and rename it to output.nupkg
echo Then press enter..
pause

rem move /Y CommandLineParser\bin\Debug\*.nupkg NugetOutput\output.nupkg

nuget add NugetOutput\output.nupkg -source \\Beast\NugetRepo

pause