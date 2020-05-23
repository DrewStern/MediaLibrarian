using System;
using System.Collections.Generic;

namespace MediaLibraryCompareTool
{
    internal class MediaLibraryCompareToolCliHandler : BaseCliHandler
    {
        ArgRegistrar<MediaLibraryArgRegistry> ArgRegistrar { get; }

        public MediaLibraryCompareToolCliHandler(ArgRegistrar<MediaLibraryArgRegistry> argRegistrar)
        {
            ArgRegistrar = argRegistrar;
        }

        public Dictionary<MediaLibraryArgRegistry, string> ParseArgs(string[] args)
        {
            var argMap = ArgRegistrar.ArgRegistry;

            foreach (var arg in args)
            {
                var argPair = StripArgPrefix(arg).Split('=');

                if (argPair.Length != 2)
                {
                    Console.WriteLine("Unable to parse arg key/value pair: " + argPair);
                    continue;
                }

                var argKey = argPair[0];
                var argValue = argPair[1];

                if (!Enum.TryParse(argKey.ToUpperInvariant(), out MediaLibraryArgRegistry parsedArg))
                {
                    Console.WriteLine($"Given argument '{argKey}' is unrecognized - its value '{argValue}' was ignored.");
                    continue;
                }

                argMap[parsedArg] = argValue;
            }

            return argMap;
        }
    }

    internal abstract class BaseCliHandler : ICliHandler
    {
        protected string StripArgPrefix(string arg)
        {
            return
                arg.StartsWith("--") ? arg.Replace("--", String.Empty) :
                arg.StartsWith("-") ? arg.Replace("-", String.Empty) :
                arg.StartsWith("/") ? arg.Replace("/", String.Empty) :
                arg;
        }
    }

    internal interface ICliHandler
    {

    }
}
