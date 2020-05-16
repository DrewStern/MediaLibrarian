namespace MediaLibraryCompareTool.Interfaces
{
    public interface ILibraryCompareResult<ILibraryItem, ILibrary> 
    {
        ILibrary Left { get; }
        ILibrary Right { get; }
        ILibrary Sum { get; }
        ILibrary Intersection { get; }
        ILibrary LeftOutersection { get; }
        ILibrary RightOutersection { get; }
        ILibrary FullOutersection { get; }
    }
}
