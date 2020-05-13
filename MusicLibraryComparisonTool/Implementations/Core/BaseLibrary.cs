using System.Collections.Generic;
using MediaLibraryCompareTool.Interfaces;

namespace MediaLibraryCompareTool
{
    public abstract class BaseLibrary : ILibrary
    {
        public virtual List<ILibraryItem> Collection { get; }
    }
}