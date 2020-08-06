using MediaLibrarian.Interfaces;

namespace MediaLibrarian
{
    public abstract class BaseLibraryCompareResult<TLibraryItem, TLibrary> 
        : ILibraryCompareResult<TLibraryItem, TLibrary>
        where TLibraryItem : ILibraryItem
        where TLibrary : ILibrary<TLibraryItem>
    {

        public abstract TLibrary Left { get; }
        public abstract TLibrary Right { get; }
        public abstract TLibrary Sum { get; }
        public abstract TLibrary Intersection { get; }
        public abstract TLibrary LeftOutersection { get; }
        public abstract TLibrary RightOutersection { get; }
        public abstract TLibrary FullOutersection { get; }
    }
}
