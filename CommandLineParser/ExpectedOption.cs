
namespace Fray.CommandLineParser
{
    /// <summary>
    /// This is a single option that you might expect to see in the command line args.
    /// Ex: Option:"help" for --help  ShortOption:"h" for -h   'help' is a stand alone arg
    ///  and so would be type "StandAlone"
    /// Ex: Option:"timeout" for --timeout  ShortOption:"t" for -t   'timeout' would expect
    ///  one sub arg, so it's type would be "OneParam"
    /// </summary>
    public class ExpectedOption
    {
        /// <summary>
        /// Long version:  ex: help    so the command line arg would be --help
        /// </summary>
        public readonly string Option;
        /// <summary>
        /// Short version:  ex: h    so the command line arg would be -h
        /// </summary>
        public readonly string ShortOption;
        /// <summary>
        /// This is how you specify if any sub args are expected on the option
        /// </summary>
        public enum EOptionType
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
        public readonly EOptionType OptionType;

        /// <summary>
        /// Construct an ExpectedOption object
        /// </summary>
        /// <param name="option">Long name of option.  Ex: help</param>
        /// <param name="shortOption">Short name of option.  Ex: h</param>
        /// <param name="type">See EType enum for help</param>
        public ExpectedOption(string option, string shortOption, EOptionType type)
        {
            if (string.IsNullOrWhiteSpace(option))
                throw new System.ArgumentNullException(nameof(option));
            Option = option;
            ShortOption = shortOption;
            OptionType = type;
        }

        /// <summary>
        /// Get a help string for this ExpectedOption
        /// </summary>
        /// <returns>Help string, or null if not implemented</returns>
        public virtual string GetHelpText() => null;
    }
}
