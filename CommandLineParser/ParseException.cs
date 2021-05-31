
using System;

namespace Fray.CommandLineParser
{
    /// <summary>
    /// Exception thrown from the Parser.Parse function
    /// </summary>
    public class ParseException : Exception
    {
        public ParseException(string msg) : base(msg) { }
    }
}
