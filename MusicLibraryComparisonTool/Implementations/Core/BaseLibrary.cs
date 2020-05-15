using System.Collections.Generic;
using MediaLibraryCompareTool.Interfaces;

namespace MediaLibraryCompareTool
{
    public abstract class BaseLibrary<TLibraryItem> : ILibrary<TLibraryItem>
        where TLibraryItem : ILibraryItem
    {
        public abstract List<TLibraryItem> Collection { get; }
    }
}