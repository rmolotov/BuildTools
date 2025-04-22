using JetBrains.Annotations;
using UnityEditor;
using CommandLine;

namespace BuildTools.Editor.Options
{
    [PublicAPI]
    public partial class BuildToolsOptions
    {
        [Option("buildTarget", Required = true, HelpText = "Unity build target")]
        public BuildTarget BuildTarget { get; set; }

        [Option("scriptingBackend", Required = false, Default = ScriptingImplementation.IL2CPP, HelpText = "Unity scripting backend")]
        public ScriptingImplementation ScriptingBackend { get; set; }

        [Option("projectName", Required = true, HelpText = "Project name (Used for build file name and unity product name)")]
        public string ProjectName { get; set; }

        [Option("packageName", Required = true, HelpText = "Package name (example: com.theloolastudio.spk)")]
        public string PackageName { get; set; }

        [Option("buildPath", Required = true, HelpText = "Build path")]
        public string BuildPath { get; set; }

        [Option("buildOptions", Required = false, Default = BuildOptions.None, HelpText = "Unity BuildOptions")]
        public BuildOptions BuildOptions { get; set; }

        [Option("buildVersion", Required = true, HelpText = "Build version")]
        public string BuildVersion { get; set; }

        [Option("buildChar", Required = false, Default = "i", HelpText = "Build char")]
        public string BuildChar { get; set; }

        [Option("buildNumber", Required = false, Default = 0, HelpText = "Build number")]
        public int BuildNumber { get; set; }
    }
}