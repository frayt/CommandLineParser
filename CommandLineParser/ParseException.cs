
using System;

namespace Fray.CommandLineParser
{
    /// <summary>
    /// Exception thrown from the Parser.Parse function
    /// </summary>
    public class ParseException : Exception
    {
        /// <summary>
        /// Construct a ParseException object
        /// </summary>
        /// <param name="msg">The error message</param>
        public ParseException(string msg) : base(msg) { }
    }
}
