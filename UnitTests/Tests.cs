
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fray.CommandLineParser;

namespace UnitTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test()
        {
            var expectedOptions = new ExpectedOption[]
            {
                new ExpectedOption("help", "h", ExpectedOption.EType.StandAlone),
                new ExpectedOption("retries", "r", ExpectedOption.EType.OneParam),
                new ExpectedOption("settings", "s", ExpectedOption.EType.OneParam),
                new ExpectedOption("randomNumbers", "rn", ExpectedOption.EType.OneOrMoreParams),
            };

            string[] args;
            Options options;

            // Single option tests:

            args = new string[] { "--help" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(1, options.options.Count);
            Assert.AreEqual(0, options.standAloneArgs.Count);
            Assert.AreEqual("help", options.options[0].name);
            Assert.AreEqual(0, options.options[0].args.Count);

            args = new string[] { "-h" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(1, options.options.Count);
            Assert.AreEqual(0, options.standAloneArgs.Count);

            args = new string[] { "-help" }; // -help doesn't exist!
            Assert.ThrowsException<ParseException>(() => Parser.Parse(args, expectedOptions));

            args = new string[] { "-h", "unexpected-param-after-help!" };
            Assert.ThrowsException<ParseException>(() => Parser.Parse(args, expectedOptions));

            // Stand alone args tests:

            args = new string[] { "srcFile", "destFile" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(2, options.standAloneArgs.Count);
            Assert.AreEqual(0, options.options.Count);

            args = new string[] { "srcFile", "destFile", "--retries", "5" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(2, options.standAloneArgs.Count);
            Assert.AreEqual(1, options.options.Count);
            Assert.AreEqual("retries", options.options[0].name);
            Assert.AreEqual(1, options.options[0].args.Count);
            Assert.AreEqual("5", options.options[0].args[0]);

            // Only 1 param fail:

            args = new string[] { "--retries", "5", "6" };
            Assert.ThrowsException<ParseException>(() => Parser.Parse(args, expectedOptions));

            // N params test:
            args = new string[] { "-h", "-rn", "1", "2", "3", "-r", "100" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(0, options.standAloneArgs.Count);
            Assert.AreEqual(3, options.options.Count);
            Assert.AreEqual("help", options.options[0].name);
            Assert.AreEqual("randomNumbers", options.options[1].name);
            Assert.AreEqual("retries", options.options[2].name);
            Assert.AreEqual(3, options.options[1].args.Count);
        }
    }
}
