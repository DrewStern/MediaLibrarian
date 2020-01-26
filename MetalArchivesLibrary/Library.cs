using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class Library
    {
        private List<LibraryItem> _collection;
        private LibraryItemEqualityComparer _libraryItemEqualityComparer;
        private ArtistDataEqualityComparer _artistDataEqualityComparer;

        public List<LibraryItem> Collection
        {
            get { return _collection ?? (_collection = new List<LibraryItem>()); }
        }

        public List<ArtistData> Artists
        {
            get
            {
                return Collection.
                    Select(x => x.ArtistData).
                    Distinct(ArtistDataEqualityComparer).
                    ToList();
            }
        }

        public List<ReleaseData> Releases
        {
            get
            {
                return Collection.
                    Select(x => x.ReleaseData).
                    Distinct().
                    ToList();
            }
        }

        private LibraryItemEqualityComparer LibraryItemEqualityComparer
        {
            get
            {
                if (_libraryItemEqualityComparer == null)
                {
                    _libraryItemEqualityComparer = new LibraryItemEqualityComparer();
                }

                return _libraryItemEqualityComparer;
            }
        }

        private ArtistDataEqualityComparer ArtistDataEqualityComparer
        {
            get
            {
                if (_artistDataEqualityComparer == null)
                {
                    _artistDataEqualityComparer = new ArtistDataEqualityComparer();
                }

                return _artistDataEqualityComparer;
            }
        }

        public Library()
        {
        }

        public Library(List<LibraryItem> libraryData)
        {
            AddToCollection(libraryData);
        }

        public Library(DirectoryInfo fromPath)
        {
            if (!fromPath.Exists)
            {
                throw new ArgumentException("The given path must exist on disk.");
            }

            foreach (DirectoryInfo artistLayer in fromPath.GetDirectories())
            {
                foreach (DirectoryInfo albumLayer in artistLayer.GetDirectories())
                {
                    Collection.Add(new LibraryItem(artistLayer.Name, albumLayer.Name));
                }
            }
        }

        public void AddToCollection(Library other)
        {
            AddToCollection(other.Collection);
        }

        public void AddToCollection(List<LibraryItem> lli)
        {
            foreach (LibraryItem li in lli)
            {
                AddToCollection(li);
            }
        }

        public void AddToCollection(LibraryItem li)
        {
            if (!Collection.Contains(li, LibraryItemEqualityComparer))
            {
                Collection.Add(li);
            }
        }
    }
}
