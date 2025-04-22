using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build.Reporting;
using BuildTools.Editor.Options;

namespace BuildTools.Editor.BuildCommands
{
    public static partial class BuildCommand
    {
        [PublicAPI]
        public static void PerformCiBuild()
        {
            PerformBuild();
        }
        
        private static void PerformBuild(
            BuildToolsOptions appOptions = default, 
            IReadOnlyDictionary<string,  string> appArgs = default)
        {
            var options = appOptions ?? BuildToolsOptions.Parse(appArgs);
            
            if (options.BuildTarget != EditorUserBuildSettings.activeBuildTarget)
            {
                BuildLogger.Log($"Switch target platform to {options.BuildTarget}",
                    nameof(BuildCommand),
                    nameof(PerformBuild)
                );
                EditorUserBuildSettings.SwitchActiveBuildTarget(
                    GetBuildTargetGroup(options.BuildTarget),
                    options.BuildTarget);
            }
            
            BuildLogger.Log($"Performing build {options.BuildTarget}",
                nameof(BuildCommand),
                nameof(PerformBuild)
            );
            
            HandleAndroidOptions(options);
            HandleIosOptions(options);
            
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(options.BuildTarget);

            PlayerSettings.SetScriptingBackend(buildTargetGroup, options.ScriptingBackend);
            PlayerSettings.productName = options.ProjectName;
            PlayerSettings.SetApplicationIdentifier(buildTargetGroup, options.PackageName);
            PlayerSettings.bundleVersion = options.BuildVersion;
            
            var gameBinaryFilePath = GetGameBinaryFilePath(options);

            var buildReport = BuildPipeline.BuildPlayer(
                levels: GetEnabledScenes(),
                locationPathName: gameBinaryFilePath,
                target: options.BuildTarget,
                options: options.BuildOptions
            );

            if (buildReport.summary.result != BuildResult.Succeeded)
                throw new Exception($"Build ended with {buildReport.summary.result} status");

            BuildLogger.Log("Done with build",
                nameof(BuildCommand),
                nameof(PerformBuild)
            );
        }
    }
}
