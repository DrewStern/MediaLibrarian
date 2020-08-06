using System;
using System.Collections.Generic;

namespace MediaLibrarian
{
    public class CliArgRegistrar<TCliArgKey> : ICliArgRegistrar<TCliArgKey>
        where TCliArgKey : Enum
    {
        private Dictionary<TCliArgKey, string> _argRegistry;

        // TODO: don't really like the idea of exposing this very much...
        public Dictionary<TCliArgKey, string> Registry
        {
            get
            {
                if (_argRegistry == null)
                {
                    _argRegistry = new Dictionary<TCliArgKey, string>();

                    foreach (TCliArgKey arg in Enum.GetValues(typeof(TCliArgKey)))
                    {
                        _argRegistry.Add(arg, string.Empty);
                    }
                }

                return _argRegistry;
            }
        }
    }
}
