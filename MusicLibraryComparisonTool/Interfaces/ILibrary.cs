using System.Collections.Generic;

namespace MusicLibraryCompareTool.Interfaces
{
    public interface ILibrary
    {
        List<ILibraryItem> Collection { get; }
    }
}
