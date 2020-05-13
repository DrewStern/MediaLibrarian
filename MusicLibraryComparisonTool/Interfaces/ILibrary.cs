using System.Collections.Generic;

namespace MediaLibraryCompareTool.Interfaces
{
    public interface ILibrary
    {
        List<ILibraryItem> Collection { get; }
    }
}
