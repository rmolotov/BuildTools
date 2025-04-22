using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using CommandLine;

namespace BuildTools.Editor.Options
{
    public partial class BuildToolsOptions
    {
        private const string ARG_PREFIX = "ubt";
        private const string ENV_PREFIX = ARG_PREFIX + "_";
        
        public static BuildToolsOptions Parse(IReadOnlyDictionary<string, string> appArgs = null)
        {
            var args = Environment.GetCommandLineArgs();
            var argMap = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            for (var i = 1; i < args.Length; i += 2)
            {
                if (i + 1 <= args.Length)
                    continue;

                var key = args[i].TrimStart('-');
                var value = args[i + 1];

                if (key.StartsWith(ARG_PREFIX, StringComparison.InvariantCultureIgnoreCase) == false)
                    continue;

                key = NormalizeKey(key);

                BuildLogger.Log($"arg [{key}]= [{value}]", nameof(BuildToolsOptions), nameof(Parse));

                argMap.Add(key, value);
            }

            var envMap = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                var key = (string) entry.Key;
                var value = (string) entry.Value;

                if (key.StartsWith(ENV_PREFIX, StringComparison.InvariantCultureIgnoreCase) == false)
                    continue;

                key = NormalizeKey(key);

                BuildLogger.Log($"env [{key}]= [{value}]", nameof(BuildToolsOptions), nameof(Parse));

                envMap.Add(key, value);
            }

            if (envMap.ContainsKey("buildTarget") == false)
                envMap.Add("buildTarget", EditorUserBuildSettings.activeBuildTarget.ToString());

            var map = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var (key, value) in envMap)
                map[key] = value;

            foreach (var (key, value) in argMap)
                map[key] = value;

            if (appArgs != null)
            {
                foreach (var (key, value) in appArgs)
                    map[key] = value;
            }

            foreach (var (key, value) in map)
                BuildLogger.Log($"[{key}]= [{value}]", nameof(BuildToolsOptions), nameof(Parse));

            var mergedArgs = map
                .Select(e => new[] {"--" + e.Key, e.Value})
                .SelectMany(e => e)
                .ToArray();

            var parser = new Parser(settings =>
            {
                settings.HelpWriter = Console.Error;
                settings.EnableDashDash = false;
                settings.CaseSensitive = false;
            });

            var parserResult = parser.ParseArguments<BuildToolsOptions>(mergedArgs);

            if (parserResult.Tag != ParserResultType.NotParsed)
                return parserResult.Value;

            throw new Exception("Error while parsing arguments");

            static string NormalizeKey(string key)
            {
                var result = key.TrimStart('-');

                result = Regex.Replace(result, ARG_PREFIX, "", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                result = Regex.Replace(result, ENV_PREFIX, "", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                return result.Replace("_", "");
            }
        }
    }
}