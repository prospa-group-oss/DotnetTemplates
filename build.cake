var target          = Argument("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var buildArtifacts      = Directory("./artifacts/packages");
var feedDirectory       = Directory("./feed/content");
var packageVersion      = "3.11.0";

///////////////////////////////////////////////////////////////////////////////
// Clean
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
    .Does(() =>
{
    CleanDirectories(new DirectoryPath[] 
    {
        buildArtifacts,
        feedDirectory

    });
});

///////////////////////////////////////////////////////////////////////////////
// Build
///////////////////////////////////////////////////////////////////////////////
Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings 
    {
        Configuration = configuration,
    };

    var projects = GetFiles("./src/**/*.csproj");
    foreach(var project in projects)
    {
        DotNetCoreBuild(project.GetDirectory().FullPath, settings);
    }
});

///////////////////////////////////////////////////////////////////////////////
// Copy
///////////////////////////////////////////////////////////////////////////////
Task("Copy")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var contentDir = "./feed/content";
    CreateDirectory(contentDir);

    // copy the single csproj templates
    var copyFiles = GetFiles("./src/**/*.*");
    CopyFiles(copyFiles, feedDirectory, true);

    var directoriesToClean = new[] { $"{contentDir}/**/bin", $"{contentDir}/**/obj" };
    foreach(var dirPattern in directoriesToClean) {
        var cleanDirectories = GetDirectories(dirPattern);

        foreach(var directory in cleanDirectories)
        {
            if (DirectoryExists(directory))
            {
                DeleteDirectory(directory, new DeleteDirectorySettings { Recursive = true, Force = true });
            }
        }
    }
});

///////////////////////////////////////////////////////////////////////////////
// Pack
///////////////////////////////////////////////////////////////////////////////
Task("Pack")
    .IsDependentOn("Clean")
    .IsDependentOn("Copy")
    .Does(() =>
{
    var settings = new NuGetPackSettings
    {
        Version = packageVersion,
        OutputDirectory = buildArtifacts
    };

    NuGetPack("./feed/Prospa.Templates.nuspec", settings);
});


Task("Default")
  .IsDependentOn("Pack");

RunTarget(target);