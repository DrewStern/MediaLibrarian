using System;

namespace MediaLibrarian
{
    public class CliHandler : ICliHandler
    {
        public CliArgRegistrar<TCliArgKey> GetCliArgRegistrar<TCliArgKey>(string[] args)
            where TCliArgKey : Enum
        {
            var registrar = new CliArgRegistrar<TCliArgKey>();

            foreach (var arg in args)
            {
                var argPair = TrySplitArgPair(arg);

                if (argPair == null)
                {
                    Console.WriteLine($"Unable to parse arg key/value pair: {argPair}");
                    continue;
                }

                var argKey = argPair[0];
                var argValue = argPair[1];

                // Enum.TryParse doesn't work here - it claims to accept a type parameter of type TEnum, 
                // but enforces the constraint TEnum : struct. Meaning actual Enums are disallowed. Weird.
                var parsedArg = (TCliArgKey)Enum.Parse(typeof(TCliArgKey), argKey.ToUpperInvariant());

                if (parsedArg == null)
                {
                    Console.WriteLine($"Given argument '{argKey}' is unrecognized - its value '{argValue}' was ignored.");
                    continue;
                }

                registrar.Registry[parsedArg] = argValue;
            }

            return registrar;
        }

        private string[] TrySplitArgPair(string argPair)
        {
            return
                StripArgPrefix(argPair).Split('=').Length == 2 ? StripArgPrefix(argPair).Split('=') :
                StripArgPrefix(argPair).Split(':').Length == 2 ? StripArgPrefix(argPair).Split(':') :
                StripArgPrefix(argPair).Split('~').Length == 2 ? StripArgPrefix(argPair).Split('~') :
                null;
        }

        private string StripArgPrefix(string arg)
        {
            return
                arg.StartsWith("--") ? arg.Replace("--", string.Empty) :
                arg.StartsWith("-") ? arg.Replace("-", string.Empty) :
                arg.StartsWith("/") ? arg.Replace("/", string.Empty) :
                arg;
        }
    }
}
