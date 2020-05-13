using MusicLibraryCompareTool.Interfaces;
using System.Collections.Generic;

namespace MusicLibraryComparisonTool.Interfaces
{
    public interface ILibrary
    {
        List<ILibraryItem> Collection { get; }
    }
}
