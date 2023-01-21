# CommandLineParser
Simple lib for parsing command line arguments

# Usage
Create a list of ExpectedOption objects:  These are all the arg types that you might expect to be used by the caller

Call Parser.Parse() with that expected options list, and the string[] command line args, and a Options object will be returned

If the args contain anything invalid, ParseException will be thrown

# Example

```
      var expectedOptions = new List<ExpectedOption>()
      {
          new ExpectedOption("timeout", "t", ExpectedOption.EType.OneParam),
          new ExpectedOption("port", "p", ExpectedOption.EType.OneParam),
          new ExpectedOption("help", "h", ExpectedOption.EType.StandAlone),
          new ExpectedOption("verbose", "v", ExpectedOption.EType.StandAlone)
      };
      Options options = Parser.Parse(args, expectedOptions);

      // Example:
      // Reading the args for your 'connectToServer' app would look like this:
      // command-line example:  connectToServer.exe srv.someurl.com -p 9000 --timeout 60

      string uri = options.standAloneArgs[0];
      int port = int.Parse(options.GetOption("port").args[0]);
      int timeout = int.Parse(options.GetOption("timeout").args[0]);
```
