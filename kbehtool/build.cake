var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var outputDir = "./publish";
var projectDir = "./KBEHtool/";
var suffix = Argument("suffix", "");

Task("Clean")
    .Does(() =>
{
    if (DirectoryExists(outputDir))
    {
        DeleteDirectory(outputDir, new DeleteDirectorySettings { Recursive = true, Force = true });
    }
    CreateDirectory(outputDir);
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetBuild(projectDir, new DotNetBuildSettings
    {
        Configuration = configuration,
        NoIncremental = true,
    });
});

Task("Package")
    .IsDependentOn("Build")
    .Does(() =>
{
    var packSettings = new DotNetPackSettings
    {
        Configuration = configuration,
        NoRestore = true,
        NoBuild = true,
        IncludeSymbols = true,
        OutputDirectory = outputDir,
    };

    if (suffix != "")
    {
        var version = XmlPeek(projectDir + "KBEHtool.csproj", "/Project/PropertyGroup/Version");
        packSettings.MSBuildSettings = new DotNetMSBuildSettings()
            .WithProperty("Version", $"{version}-{suffix}");
    }

    DotNetPack(projectDir, packSettings);
});

Task("Default")
    .IsDependentOn("Package");

RunTarget(target);