using System.Collections.Generic;

namespace MediaLibraryCompareTool
{
    public interface ICliArgRegistrar<TCliArgKey>
    {
        Dictionary<TCliArgKey, string> Registry { get; }
    }
}
