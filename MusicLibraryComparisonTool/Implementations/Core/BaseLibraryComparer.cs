using MediaLibraryCompareTool.Interfaces;

namespace MediaLibraryCompareTool
{
    public abstract class BaseLibraryComparer<TLibraryItem, TLibrary, TLibraryCompareResult> 
        : ILibraryComparer<TLibraryItem, TLibrary, TLibraryCompareResult>
        where TLibraryItem : ILibraryItem
        where TLibrary : ILibrary<TLibraryItem>
        where TLibraryCompareResult : ILibraryCompareResult<TLibraryItem, TLibrary>
    {
        public abstract TLibraryCompareResult Compare(TLibrary left, TLibrary right);
    }
}
