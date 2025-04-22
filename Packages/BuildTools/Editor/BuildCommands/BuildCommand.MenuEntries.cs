using UnityEditor;
using BuildTools.Editor.Options;

namespace BuildTools.Editor.BuildCommands
{
    public partial class BuildCommand
    {
        [MenuItem("Tools/Build Tools/Build/WebGL")]
        public static void PerformWebGLBuild()
        {
            PerformBuild(new BuildToolsOptions
            {
                BuildTarget = BuildTarget.WebGL,
                ProjectName = PlayerSettings.productName,
                PackageName = PlayerSettings.applicationIdentifier,
                BuildPath = "../builds",
                BuildVersion = "editor"
            });
        }

        [MenuItem("Tools/Build Tools/Build/Android")]
        public static void PerformAndroidBuild()
        {
            PerformBuild(new BuildToolsOptions
            {
                BuildTarget = BuildTarget.Android,
                ProjectName = PlayerSettings.productName,
                PackageName = PlayerSettings.applicationIdentifier,
                BuildPath = "../builds",
                BuildVersion = "editor"
            });
        }

        [MenuItem("Tools/Build Tools/Build/Windows")]
        public static void PerformWindowsBuild()
        {
            PerformBuild(new BuildToolsOptions
            {
                BuildTarget = BuildTarget.StandaloneWindows64,
                ProjectName = PlayerSettings.productName,
                PackageName = PlayerSettings.applicationIdentifier,
                BuildPath = "../builds",
                BuildVersion = "editor"
            });
        }
    }
}