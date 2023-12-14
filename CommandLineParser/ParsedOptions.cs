
using System;
using System.Collections.Generic;

namespace Fray.CommandLineParser
{
    /// <summary>
    /// This holds the results of parsing the supplied command line args with the Parser.Parse function.
    /// </summary>
    public class ParsedOptions
    {
        /// <summary>
        /// The list of parsed Option objects
        /// </summary>
        public readonly List<Option> Options = new List<Option>();

        /// <summary>
        /// Extra stand alone args that aren't -- or - options.
        /// Ex: "copy srcFile destFile --timeout 30"  srcFile and destFile would be in this list
        /// </summary>
        public readonly List<string> StandAloneArgs = new List<string>();

        /// <summary>
        /// Does a simple check to see if the request option exists
        /// </summary>
        /// <param name="option">long/full name of option</param>
        /// <returns>true if option exists</returns>
        public bool OptionExists(string option)
        {
            return Options.Find(o => o.Name == option) != null;
        }

        /// <summary>
        /// Does a simple check to see if the request option exists
        /// </summary>
        /// <param name="option">ExpectedOption you're checking against</param>
        /// <returns>true if option exists</returns>
        public bool OptionExists(ExpectedOption option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));
            return Options.Find(o => o.Name == option.Option) != null;
        }

        /// <summary>
        /// Get Option object by option name
        /// </summary>
        /// <param name="option">long/full name of option you are matching against</param>
        /// <returns>Returns Option if exists, otherwise returns null</returns>
        internal Option GetOption(string option)
        {
            return Options.Find(o => o.Name == option);
        }

        /// <summary>
        /// Get Option object by ExpectedOption
        /// </summary>
        /// <param name="option">The ExpectedOption that you are matching against</param>
        /// <returns>Returns Option if exists, otherwise returns null</returns>
        internal Option GetOption(ExpectedOption option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));
            return GetOption(option.Option);
        }

        /// <summary>
        /// If an option expects a single sub arg, this retrieves it.
        /// Ex: If your tool's Copy command looked like this: "Copy srcFile destFile --timeout 30"
        ///  GetOptionSingleArg("timeout") would return "30"
        /// </summary>
        /// <param name="option">long/full name of option</param>
        /// <returns>The sub arg</returns>
        public string GetOptionSingleArg(string option)
        {
            Option o = GetOption(option);
            if (o == null)
                return null;
            if (o.ArgsCount == 0)
                return null;
            return o.Args[0];
        }

        /// <summary>
        /// If an option expects a single sub arg, this retrieves it.
        /// Ex: If your tool's Copy command looked like this: "Copy srcFile destFile --timeout 30"
        ///  GetOptionSingleArg("timeout") would return "30"
        /// </summary>
        /// <param name="option">long/full name of option</param>
        /// <returns>The sub arg</returns>
        public string GetOptionSingleArg(ExpectedOption option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));
            return GetOptionSingleArg(option.Option);
        }

        /// <summary>
        /// If an option expects one or more sub args, this retrieves them.
        /// Ex: If your tool's Copy command looked like this: "Copy srcFile destFile --ignore a.txt b.txt c.txt"
        ///  GetOptionSingleArg("timeout") would return the list "a.txt","b.txt","c.txt"
        /// </summary>
        /// <param name="option">long/full name of option</param>
        /// <returns>The sub args</returns>
        public IReadOnlyList<string> GetOptionOneOrMoreArgs(string option)
        {
            Option o = GetOption(option);
            if (o == null)
                return null;
            if (o.ArgsCount == 0)
                return null;
            return o.Args;
        }

        /// <summary>
        /// If an option expects one or more sub args, this retrieves them.
        /// Ex: If your tool's Copy command looked like this: "Copy srcFile destFile --ignore a.txt b.txt c.txt"
        ///  GetOptionSingleArg("timeout") would return the list "a.txt","b.txt","c.txt"
        /// </summary>
        /// <param name="option">long/full name of option</param>
        /// <returns>The sub args</returns>
        public IReadOnlyList<string> GetOptionOneOrMoreArgs(ExpectedOption option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));
            return GetOptionOneOrMoreArgs(option.Option);
        }
    }
}
