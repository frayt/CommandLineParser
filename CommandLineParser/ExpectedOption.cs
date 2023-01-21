
namespace Fray.CommandLineParser
{
    /// <summary>
    /// This is a single option that you might expect to see in the command line args.
    /// Ex: option:"help" for --help  shortOption:"h" for -h   'help' is a stand alone arg
    ///  and so would be type "StandAlone"
    /// Ex: option:"timeout" for --timeout  shortOption:"t" for -t   'timeout' would expect
    ///  one sub arg, so it's type would be "OneParam"
    /// </summary>
    public class ExpectedOption
    {
        /// <summary>
        /// Long version:  ex: help    so the command line arg would be --help
        /// </summary>
        public readonly string option;
        /// <summary>
        /// Short version:  ex: h    so the command line arg would be -h
        /// </summary>
        public readonly string shortOption;
        /// <summary>
        /// This is how you specify if any sub args are expected on the option
        /// </summary>
        public enum EType
        {
            /// <summary>No sub args are expected on this option.  Ex: --help</summary>
            StandAlone,
            /// <summary>One sub arg is expected on this option.  Ex: --timeout 30</summary>
            OneParam,
            /// <summary>One or more sub args are expected on this option.  Ex: --files a.txt b.txt c.txt</summary>
            OneOrMoreParams,
        }
        /// <summary>
        /// Parameter type
        /// </summary>
        public readonly EType type;

        /// <summary>
        /// Construct an ExpectedOption object
        /// </summary>
        /// <param name="option">Long name of option.  Ex: help</param>
        /// <param name="shortOption">Short name of option.  Ex: h</param>
        /// <param name="type">See EType enum for help</param>
        public ExpectedOption(string option, string shortOption, EType type) { this.option = option; this.shortOption = shortOption; this.type = type; }
    }
}
