using UnityEditor;
using BuildTools.Editor.Options;

namespace BuildTools.Editor.BuildCommands
{
    public static partial class BuildCommand
    {
        private static void HandleIosOptions(BuildToolsOptions options)
        {
            if (options.BuildTarget != BuildTarget.iOS)
                return;
            
            BuildLogger.Log($"Setting bundleVersionNumber to {options.IosVersion}",
                nameof(BuildCommand),
                nameof(HandleIosOptions)
            );
            PlayerSettings.bundleVersion = options.IosVersion;
        }
    }
}