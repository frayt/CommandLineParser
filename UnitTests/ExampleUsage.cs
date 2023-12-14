
using System;
using System.Collections.Generic;
using Fray.CommandLineParser;

// Example usage of this sample:
// CopyFilesTool.exe source dest --verbose --timeout 30 --ignore-these-files a.txt b.txt c.txt
// CopyFilesTool.exe source dest -t 30 -v -i a.txt b.txt c.txt

public static class CopyFilesTool
{
    private readonly static ExpectedOption _option_timeout =
        new ExpectedOptionWithHelp("timeout", "t", ExpectedOption.EOptionType.OneParam, "<timeout in minutes>");
    private readonly static ExpectedOption _option_verbose =
        new ExpectedOptionWithHelp("verbose", "v", ExpectedOption.EOptionType.StandAlone, null, "Show verbose info during operation");
    private readonly static ExpectedOption _option_ignoreFiles =
        new ExpectedOptionWithHelp("ignore-these-files", "i", ExpectedOption.EOptionType.OneOrMoreParams, "<file1 [file2] [file3] [fileN]>");
    private readonly static ExpectedOption _option_help =
        new ExpectedOptionWithHelp("help", "h", ExpectedOption.EOptionType.StandAlone, null, "Show Usage/Help");

    private static ExpectedOption[] _expectedOptions;
    public static int Main_(string[] args)
    {
        _expectedOptions = new ExpectedOption[]
        {
            _option_timeout,
            _option_verbose,
            _option_ignoreFiles,
            _option_help,
        };

        ParsedOptions options;
        try
        {
            options = Parser.Parse(args, _expectedOptions);
        }
        catch (ParseException e)
        {
            Console.WriteLine("Error parsing arguments: " + e.Message);
            return 1;
        }

        if (options.OptionExists(_option_help))
        {
            ShowUsage();
            return 0;
        }
        if (options.StandAloneArgs.Count != 2)
        {
            Console.WriteLine("Expect 2 args specifying source and dest");
            ShowUsage();
            return 1;
        }
        string source = options.StandAloneArgs[0];
        string dest = options.StandAloneArgs[1];
        bool verbose = options.OptionExists(_option_verbose);
        int.TryParse(options.GetOptionSingleArg(_option_timeout), out int timeout);
        IReadOnlyList<string> ignoreTheseFiles = options.GetOptionOneOrMoreArgs(_option_ignoreFiles);

        // Perform operation..
        Console.WriteLine($"Args: {source} {dest} verbose:{verbose} timeout:{timeout} ignore-count:{ignoreTheseFiles.Count}");

        return 0;
    }

    private static void ShowUsage()
    {
        Console.WriteLine($"Usage:");
        Console.WriteLine($" CopyFiles source dest");
        Console.WriteLine($"Optional params:");
        foreach (var option in _expectedOptions)
            Console.WriteLine(option.GetHelpText());
    }
}
