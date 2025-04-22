using CommandLine;

namespace BuildTools.Editor.Options
{
    public partial class BuildToolsOptions
    {
        [Option("iosVersion", Required = false, HelpText = "iOS version")]
        public string IosVersion { get; set; }
    }
}