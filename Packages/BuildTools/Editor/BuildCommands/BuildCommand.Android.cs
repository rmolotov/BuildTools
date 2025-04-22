using System.IO;
using UnityEditor;
using BuildTools.Editor.Options;

namespace BuildTools.Editor.BuildCommands
{
    public static partial class BuildCommand
    {
        private static void HandleAndroidOptions(BuildToolsOptions options)
        {
            if (options.BuildTarget != BuildTarget.Android) 
                return;

            EditorUserBuildSettings.buildAppBundle = options.AndroidBuildAppBundle;

            PlayerSettings.Android.useCustomKeystore = false;

            if (File.Exists(options.AndroidKeystoreFileName) == false)
            {
                BuildLogger.LogWarning($"{options.AndroidKeystoreFileName} not found, skipping setup, using Unity's default keystore",
                    nameof(BuildCommand),
                    nameof(HandleAndroidOptions)
                );
                return;
            }

            PlayerSettings.Android.useCustomKeystore = true;

            PlayerSettings.Android.keystoreName = options.AndroidKeystoreFileName;
            PlayerSettings.Android.keystorePass = options.AndroidKeystorePass;
            PlayerSettings.Android.keyaliasName = options.AndroidKeystoreAliasName;
            PlayerSettings.Android.keyaliasPass = options.AndroidKeystoreAliasPass;

            PlayerSettings.Android.bundleVersionCode = options.BuildNumber;
        }
    }
}