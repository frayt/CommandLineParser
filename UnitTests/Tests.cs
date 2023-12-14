
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fray.CommandLineParser;
using System.ComponentModel.Design;

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
                new ExpectedOption("help", "h", ExpectedOption.EOptionType.StandAlone),
                new ExpectedOption("retries", "r", ExpectedOption.EOptionType.OneParam),
                new ExpectedOption("settings", "s", ExpectedOption.EOptionType.OneParam),
                new ExpectedOption("randomNumbers", "rn", ExpectedOption.EOptionType.OneOrMoreParams),
            };

            string[] args;
            ParsedOptions options;

            // Single option tests:

            args = new string[] { "--help" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(1, options.Options.Count);
            Assert.AreEqual(0, options.StandAloneArgs.Count);
            Assert.AreEqual("help", options.Options[0].Name);
            Assert.AreEqual(0, options.Options[0].Args.Count);

            args = new string[] { "-h" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(1, options.Options.Count);
            Assert.AreEqual(0, options.StandAloneArgs.Count);

            args = new string[] { "-help" }; // -help doesn't exist!
            Assert.ThrowsException<ParseException>(() => Parser.Parse(args, expectedOptions));

            args = new string[] { "-h", "unexpected-param-after-help!" };
            Assert.ThrowsException<ParseException>(() => Parser.Parse(args, expectedOptions));

            // Stand alone args tests:

            args = new string[] { "srcFile", "destFile" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(2, options.StandAloneArgs.Count);
            Assert.AreEqual(0, options.Options.Count);

            args = new string[] { "srcFile", "destFile", "--retries", "5" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(2, options.StandAloneArgs.Count);
            Assert.AreEqual(1, options.Options.Count);
            Assert.AreEqual("retries", options.Options[0].Name);
            Assert.AreEqual(1, options.Options[0].ArgsCount);
            Assert.AreEqual("5", options.Options[0].Args[0]);

            // Only 1 param fail:

            args = new string[] { "--retries", "5", "6" };
            Assert.ThrowsException<ParseException>(() => Parser.Parse(args, expectedOptions));

            // N params test:
            args = new string[] { "-h", "-rn", "1", "2", "3", "-r", "100" };
            options = Parser.Parse(args, expectedOptions);
            Assert.AreEqual(0, options.StandAloneArgs.Count);
            Assert.AreEqual(3, options.Options.Count);
            Assert.AreEqual("help", options.Options[0].Name);
            Assert.AreEqual("randomNumbers", options.Options[1].Name);
            Assert.AreEqual("retries", options.Options[2].Name);
            Assert.AreEqual(3, options.Options[1].Args.Count);

            // Missing last param
            args = new string[] { "--retries" }; // <-- no param!
            Assert.ThrowsException<ParseException>(() => Parser.Parse(args, expectedOptions));
        }

        [TestMethod]
        public void TestExampleUsage()
        {
            string[] args = new string[] { "source", "dest", "-v", "-i", "a.txt", "b.txt", "c.txt", "--timeout", "30" };
            CopyFilesTool.Main_(args);
        }
    }
}