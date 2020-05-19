using MediaLibraryCompareTool.Interfaces;

namespace MediaLibraryCompareTool
{
    public abstract class BaseLibraryItem : ILibraryItem
    {
        // TODO: not sure this needs to exist? 
        // the concrete implementations of this seem as though they will have very little overlapping data
        // maybe release data? or maybe something stupid like if I want to have refs to wikipedia/etc?
    }
}