#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile
open MSTest
open System.IO

let buildDir = "./build/Yam"
let testDir = "./build/test"
let packageDir = "NuGet"

let release = 
    File.ReadLines "RELEASE_NOTES.md" 
    |> ReleaseNotesHelper.parseReleaseNotes

let version = release.AssemblyVersion
let releaseNotes = release.Notes |> String.concat "\n"

// Targets
Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "AssemblyVersion" (fun _ ->
    CreateCSharpAssemblyInfo "Yam/Properties/AssemblyVersion.cs"
        [
            Attribute.Version version
            Attribute.FileVersion version
        ]
)

Target "Build" (fun _ ->
    !! "Yam/Yam.csproj"
        |> MSBuildRelease buildDir "Rebuild"
        |> Log "YamBuild-Output: "
)

Target "BuildTests" (fun _ ->
    !! "Tests/Tests.csproj"
        |> MSBuildDebug testDir "Build"
        |> Log "TestsBuild-Output"
)

Target "Test" (fun _ ->
    !! (testDir + "/Tests.dll")
        |> MSTest (fun p -> p)
)

Target "Package" (fun _ ->
    NuGet (fun p -> 
        { p with
            Project = "Yam"
            Version = version
            ReleaseNotes = releaseNotes
            OutputPath = packageDir
            ToolPath = ".nuget/nuget.exe"
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" })
        "NuGet/Yam.nuspec"
)

Target "All" DoNothing

// Dependencies
"Clean"
    ==> "AssemblyVersion"
    ==> "Build"
    ==> "BuildTests"
    ==> "Test"
    ==> "Package"
    ==> "All"

RunTargetOrDefault "All"
