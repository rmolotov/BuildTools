using System.Linq;
using UnityEditor;
using BuildTools.Editor.Options;

namespace BuildTools.Editor.BuildCommands
{
    public static partial class BuildCommand
    {
        private static string[] GetEnabledScenes()
        {
            return EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .Where(s => string.IsNullOrWhiteSpace(s.path) == false)
                .Select(s => s.path)
                .ToArray();
        }
        
        private static string GetGameBinaryFilePath(BuildToolsOptions options)
        {
            var basePath = $"{options.BuildPath}/{options.ProjectName}/{options.BuildTarget.ToString().ToLowerInvariant()}";
            
            return options.BuildTarget switch
            {
                BuildTarget.StandaloneWindows 
                    or BuildTarget.StandaloneWindows64 => basePath + $"/{options.ProjectName}.exe",
                BuildTarget.Android                    => basePath + (options.AndroidBuildAppBundle ? ".aab" : ".apk"),
                _                                      => basePath
            };
        }
        
        private static BuildTargetGroup GetBuildTargetGroup(BuildTarget buildTarget)
        {
            return buildTarget switch
            {
                BuildTarget.StandaloneLinux64 
                    or BuildTarget.StandaloneOSX 
                    or BuildTarget.StandaloneWindows
                    or BuildTarget.StandaloneWindows64 => BuildTargetGroup.Standalone,
                BuildTarget.WebGL                      => BuildTargetGroup.WebGL,
                BuildTarget.Android                    => BuildTargetGroup.Android,
                BuildTarget.iOS                        => BuildTargetGroup.iOS,
                _                                      => BuildTargetGroup.Unknown
            };
        }
    }
}