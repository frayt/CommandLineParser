
using System.Collections.Generic;

namespace Fray.CommandLineParser
{
    /// <summary>
    /// A single parsed Option from the command line args
    /// </summary>
    public class Option
    {
        /// <summary>
        /// The long name of the found option.  Ex: help
        /// </summary>
        public readonly string name;
        /// <summary>
        /// Any sub args for the option.  Ex: if name was "files", this might be a.txt, b.txt, c.txt
        /// </summary>
        public readonly List<string> args = new List<string>();
        internal Option(string name) { this.name = name; }
    }
}
