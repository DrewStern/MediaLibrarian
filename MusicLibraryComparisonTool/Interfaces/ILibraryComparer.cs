namespace MediaLibraryCompareTool.Interfaces
{
    public interface ILibraryComparer<ILibraryItem, ILibrary, ILibraryCompareResult>
    {
        ILibraryCompareResult Compare(ILibrary left, ILibrary right);
    }
}
