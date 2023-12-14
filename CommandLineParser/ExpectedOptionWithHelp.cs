
using System.Text;

namespace Fray.CommandLineParser
{
    /// <summary>
    /// Convenience class extended from ExpectedOption that implements GetHelpText(), for easy Usage/Help reporting
    /// </summary>
    public class ExpectedOptionWithHelp : ExpectedOption
    {
        /// <summary>
        /// The text that follows your option text, such as 'filename' in "--export filename"
        /// </summary>
        public readonly string   ImmediateHelpText;
        /// <summary>
        /// One or more lines up folow up text, such as 'Exports all data to supplied filename'
        /// </summary>
        public readonly string[] FollowUpHelpText;

        private static StringBuilder _sb = null;
        private static readonly object _sbLock = new object();

        /// <summary>
        /// Construct an ExpectedOptionWithHelp object
        /// </summary>
        /// <param name="option">Long name of option.  Ex: help</param>
        /// <param name="shortOption">Short name of option.  Ex: h</param>
        /// <param name="type">See EType enum for help</param>
        /// <param name="immediateHelpText">Text that follows immediately after the "--your-option(-y)" text</param>
        /// <param name="followUpHelpText">One or more lines of text that follow on subsequent lines</param>
        public ExpectedOptionWithHelp(string option, string shortOption, EOptionType type,
                                      string immediateHelpText, params string[] followUpHelpText)
            : base(option, shortOption, type)
        {
            ImmediateHelpText = immediateHelpText;
            FollowUpHelpText = followUpHelpText;
        }

        /// <summary>
        /// Get a help string for this ExpectedOption
        /// </summary>
        public override string GetHelpText()
        {
            lock(_sbLock)
            {
                if (_sb == null)
                    _sb = new StringBuilder();

                _sb.Clear();
                _sb.Append(" --");
                _sb.Append(Option);
                _sb.Append("(-");
                _sb.Append(ShortOption);
                _sb.Append(")");
                if (ImmediateHelpText != null)
                {
                    _sb.Append(' ');
                    _sb.Append(ImmediateHelpText);
                }

                if (FollowUpHelpText != null)
                {
                    foreach(string text in FollowUpHelpText)
                    {
                        _sb.Append("\n    ");
                        _sb.Append(text);
                    }
                }
            }
            return _sb.ToString();
        }
    }
}
