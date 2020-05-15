using System.Collections.Generic;

namespace MediaLibraryCompareTool.Interfaces
{
    public interface ILibrary<TLibraryItem> 
        where TLibraryItem : ILibraryItem
    {
        List<TLibraryItem> Collection { get; }
    }
}
