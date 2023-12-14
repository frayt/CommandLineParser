
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
        public readonly string Name;
        /// <summary>
        /// Any sub args for the option.  Ex: if name was "files", this might be a.txt, b.txt, c.txt
        /// </summary>
        public IReadOnlyList<string> Args => _args ?? _emptyList;
        /// <summary>
        /// Return the number of arguments in the Args list.
        /// </summary>
        public int ArgsCount => _args != null ? _args.Count : 0;

        private List<string> _args = null;
        private static readonly IReadOnlyList<string> _emptyList = new List<string>();

        internal Option(string name)
        {
            Name = name;
        }

        internal void AddArg(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
                throw new System.ArgumentNullException(nameof(arg));
            if (_args == null)
                _args = new List<string>();
            _args.Add(arg);
        }
    }
}
