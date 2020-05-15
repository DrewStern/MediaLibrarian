namespace MediaLibraryCompareTool.Interfaces
{
    public interface ILibraryCompareResult<TLibrary, TLibraryItem> 
        where TLibraryItem : ILibraryItem
        where TLibrary : ILibrary<TLibraryItem>
    {
        TLibrary Left { get; }
        TLibrary Right { get; }
        TLibrary Sum { get; }
        TLibrary Intersection { get; }
        TLibrary LeftOutersection { get; }
        TLibrary RightOutersection { get; }
        TLibrary FullOutersection { get; }
    }
}
