using System;
using System.Collections.Generic;

namespace MediaLibraryCompareTool
{
    public class ArgRegistrar<TArgRegistry> : IArgRegistrar<TArgRegistry>
        where TArgRegistry : Enum
    {
        private Dictionary<TArgRegistry, string> _argRegistry;

        // TODO: don't really like the idea of exposing this very much...
        public Dictionary<TArgRegistry, string> ArgRegistry
        {
            get
            {
                if (_argRegistry == null)
                {
                    _argRegistry = new Dictionary<TArgRegistry, string>();

                    foreach (TArgRegistry arg in Enum.GetValues(typeof(TArgRegistry)))
                    {
                        _argRegistry.Add(arg, String.Empty);
                    }
                }

                return _argRegistry;
            }
        }

        private List<TArgRegistry> GetArgRegistry()
        {
            var args = new List<TArgRegistry>();

            foreach (TArgRegistry arg in Enum.GetValues(typeof(TArgRegistry)))
            {
                args.Add(arg);
            }

            return args;
        }
    }

    internal interface IArgRegistrar<TArgRegistry>
    {
        Dictionary<TArgRegistry, string> ArgRegistry { get;}
    }
}
