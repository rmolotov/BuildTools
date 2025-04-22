using CommandLine;

namespace BuildTools.Editor.Options
{
    public partial class BuildToolsOptions
    {
        [Option("androidBuildAppBundle", Required = false, Default = false, HelpText = "Shell we build android .aab file")]
        public bool AndroidBuildAppBundle { get; set; }

        [Option("androidKeystoreFileName", Required = false, Default = "keystore.keystore", HelpText = "Android keystore file name")]
        public string AndroidKeystoreFileName { get; set; }

        [Option("androidKeystorePass", Required = false, HelpText = "Android keystore password")]
        public string AndroidKeystorePass { get; set; }

        [Option("androidKeystoreAliasName", Required = false, HelpText = "Android keystore alias name")]
        public string AndroidKeystoreAliasName { get; set; }

        [Option("androidKeystoreAliasPass", Required = false, HelpText = "Android keystore alias pass")]
        public string AndroidKeystoreAliasPass { get; set; }
    }
}