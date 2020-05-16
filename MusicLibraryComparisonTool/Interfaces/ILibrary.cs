using System.Collections.Generic;

namespace MediaLibraryCompareTool.Interfaces
{
    public interface ILibrary<ILibraryItem> 
    {
        List<ILibraryItem> Collection { get; }
    }
}
