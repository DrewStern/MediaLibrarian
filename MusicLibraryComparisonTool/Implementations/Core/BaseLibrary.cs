using System.Collections.Generic;
using MediaLibraryCompareTool.Interfaces;

namespace MediaLibraryCompareTool
{
    public class BaseLibrary : ILibrary
    {
        public virtual List<ILibraryItem> Collection { get; }
    }
}