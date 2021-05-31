nuget pack Dll.nuspec
mkdir NugetOutput
move /Y *.nupkg NugetOutput\output.nupkg
nuget add NugetOutput\output.nupkg -source \\Beast\NugetRepo

pause