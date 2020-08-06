using System.Collections.Generic;

namespace MediaLibrarian
{
    public interface ICliArgRegistrar<TCliArgKey>
    {
        Dictionary<TCliArgKey, string> Registry { get; }
    }
}
