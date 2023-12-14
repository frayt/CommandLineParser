
using System;
using System.Collections.Generic;

namespace Fray.CommandLineParser
{
    /// <summary>
    /// This class is used to parse a list of args into an Options object
    /// </summary>
    public static class Parser
    {
        private class FoundOption
        {
            public Option option; // This will get filled out and returned to the caller
            public ExpectedOption expectedOption; // This holds the ExpectionOption we just matched up against
            public bool optionWasShortVersion;
            // The actual text, like -h or --help
            public string actualOptionString { get { return optionWasShortVersion ? "-" + expectedOption.ShortOption : "--" + expectedOption.Option; } }
        }

        // Returns null if there are no options at all
        /// <summary>
        /// Pass in a list of command line args and a list of ExpectedOption objects, and it will
        /// parse the args into an Options object, which has a list of Option objects, and "stand alone" args.
        /// Throws ParseException if something is unexpected or incorrect.
        /// </summary>
        /// <param name="args">The command line args array</param>
        /// <param name="expectedOptions">Your list of options that are expected</param>
        /// <returns></returns>
        public static ParsedOptions Parse(string[] args, IEnumerable<ExpectedOption> expectedOptions)
        {
            if (expectedOptions == null)
                throw new ArgumentNullException();

            ParsedOptions options = new ParsedOptions();

            if (args == null || args.Length == 0)
                return options;

            FoundOption currentOption = null;

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (string.IsNullOrEmpty(arg))
                    continue;

                // Check to see if we're starting a new -x or --xxxx option..
                FoundOption foundOption = null;
                foreach (var eo in expectedOptions)
                {
                    if (arg == "--" + eo.Option)
                        { foundOption = new FoundOption { expectedOption = eo, optionWasShortVersion = false }; break; }
                    if (!string.IsNullOrWhiteSpace(eo.ShortOption) && arg == "-" + eo.ShortOption)
                        { foundOption = new FoundOption { expectedOption = eo, optionWasShortVersion = true }; break; }
                }

                // Error check: unknown option?
                if (foundOption == null && arg.StartsWith("-"))
                    throw new ParseException($"Unknown option '{arg}'");

                // Did we start a new option?
                if (foundOption != null)
                {
                    // Check for errors:
                    // If the current option needs params, and we haven't seen enough yet, error!
                    if (    currentOption != null
                        && (currentOption.expectedOption.OptionType == ExpectedOption.EOptionType.OneParam || currentOption.expectedOption.OptionType == ExpectedOption.EOptionType.OneOrMoreParams)
                        &&  currentOption.option.ArgsCount == 0
                       )
                    {
                        throw new ParseException($"Expected one arg after option '{currentOption.actualOptionString}'");
                    }

                    // Add this new option
                    Option option = new Option(foundOption.expectedOption.Option);
                    options.Options.Add(option);
                    foundOption.option = option;

                    // This found option can now become the current option
                    currentOption = foundOption;
                }
                else // NOT starting a new option, this is an arg for an option..
                {
                    // Check for errors:
                    // If the current option doesn't expect extra args, error!
                    if (currentOption != null && currentOption.expectedOption.OptionType == ExpectedOption.EOptionType.StandAlone)
                        throw new ParseException($"Unexpected arg after option '{currentOption.actualOptionString}'");
                    // If the current option expects 1 param, and this is 2+, error!
                    if (currentOption != null && currentOption.expectedOption.OptionType == ExpectedOption.EOptionType.OneParam && currentOption.option.ArgsCount == 1)
                        throw new ParseException($"Unexpected second arg after option '{currentOption.actualOptionString}'");

                    // If we have a current option, then add this arg (error checks have already been done)
                    if (currentOption != null)
                    {
                        currentOption.option.AddArg(arg);
                    }
                    else // NO currentOption, so this is a stand alone arg
                    {
                        options.StandAloneArgs.Add(arg);
                    }
                }
            }

            // If we are in a "current option", and it expected a param, but never got it, error!
            if (   currentOption != null
                && currentOption.expectedOption.OptionType != ExpectedOption.EOptionType.StandAlone
                && currentOption.option.ArgsCount == 0
               )
            {
                throw new ParseException($"Expected one arg after option '{currentOption.actualOptionString}'");
            }

            return options;
        }
    }
}
