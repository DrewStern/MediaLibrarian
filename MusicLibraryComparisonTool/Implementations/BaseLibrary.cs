using System.Collections.Generic;
using MusicLibraryCompareTool.Interfaces;

namespace MusicLibraryCompareTool
{
    public class BaseLibrary : ILibrary
    {
        public virtual List<ILibraryItem> Collection { get; }
    }
}