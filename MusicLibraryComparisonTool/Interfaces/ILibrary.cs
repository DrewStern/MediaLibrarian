using System.Collections.Generic;

namespace MediaLibrarian.Interfaces
{
    public interface ILibrary<ILibraryItem> 
    {
        List<ILibraryItem> Collection { get; }

        void AddToCollection(ILibrary<ILibraryItem> other);

        void AddToCollection(List<ILibraryItem> other);

        void AddToCollection(ILibraryItem other);
    }
}
