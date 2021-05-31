# CommandLineParser
Simple lib for parsing command line arguments

# Usage
Create a list of ExpectedOption objects:  These are all the arg types that you might expect to be used by the caller

Call Parser.Parse() with that expected options list, and the string[] command line args, and a Options object will be returned

If the args contain anything invalid, ParseException will be thrown
