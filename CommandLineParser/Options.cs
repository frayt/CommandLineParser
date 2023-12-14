
using System.Collections.Generic;

namespace Fray.CommandLineParser
{
    /// <summary>
    /// This holds the results of parsing the supplied command line args with the Parser.Parse function.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// The list of parsed Option objects
        /// </summary>
        public readonly List<Option> options = new List<Option>();

        /// <summary>
        /// Extra stand alone args that aren't -- or - options.
        /// Ex: "copy srcFile destFile --timeout 30"  srcFile and destFile would be in this list
        /// </summary>
        public readonly List<string> standAloneArgs = new List<string>();

        /// <summary>
        /// Does a simple check to see if the request option exists
        /// </summary>
        /// <param name="option">long/full name of option</param>
        /// <returns>true if option exists</returns>
        public bool OptionExists(string option)
        {
            return options.Find(o => o.name == option) != null;
        }

        /// <summary>
        /// Does a simple check to see if the request option exists
        /// </summary>
        /// <param name="option">ExpectedOption you're checking against</param>
        /// <returns>true if option exists</returns>
        public bool OptionExists(ExpectedOption option)
        {
            return options.Find(o => o.name == option.Option) != null;
        }

        /// <summary>
        /// Get Option object by option name
        /// </summary>
        /// <param name="option">long/full name of option you are matching against</param>
        /// <returns>Returns Option if exists, otherwise returns null</returns>
        public Option GetOption(string option)
        {
            return options.Find(o => o.name == option);
        }

        /// <summary>
        /// Get Option object by ExpectedOption
        /// </summary>
        /// <param name="option">The ExpectedOption that you are matching against</param>
        /// <returns>Returns Option if exists, otherwise returns null</returns>
        public Option GetOption(ExpectedOption option)
        {
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
            if (o.args.Count == 0)
                return null;
            return o.args[0];
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
            return GetOptionSingleArg(option.Option);
        }
    }
}
