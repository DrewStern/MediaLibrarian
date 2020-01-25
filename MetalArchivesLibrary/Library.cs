using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class Library
    {
        private LibraryItemEqualityComparer _libraryItemEqualityComparer;
        private ArtistDataEqualityComparer _artistDataEqualityComparer;

        public List<LibraryItem> EntireCollection { get; }

        public List<ArtistData> Artists
        {
            get
            {
                return EntireCollection.
                    Select(x => x.ArtistData).
                    Distinct(ArtistDataEqualityComparer).
                    ToList();
            }
        }

        public List<ReleaseData> Releases
        {
            get
            {
                return EntireCollection.
                    Select(x => x.ReleaseData).
                    Distinct().
                    ToList();
            }
        }

        private LibraryItemEqualityComparer Comparer
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
            EntireCollection = new List<LibraryItem>();
        }

        public Library(List<LibraryItem> libraryData)
        {
            // prevent the client from adding duplicate data to our set
            EntireCollection = libraryData.
                Distinct(Comparer).
                OrderByDescending(x => x.ToString()).
                ToList();
        }

        public Library(DirectoryInfo fromPath)
        {
            if (!fromPath.Exists)
            {
                throw new ArgumentException("The given path must exist on disk.");
            }

            EntireCollection = new List<LibraryItem>();

            foreach (DirectoryInfo artistLayer in fromPath.GetDirectories())
            {
                foreach (DirectoryInfo albumLayer in artistLayer.GetDirectories())
                {
                    EntireCollection.Add(new LibraryItem(artistLayer.Name, albumLayer.Name));
                }
            }
        }

        public void AddToLibrary(List<LibraryItem> lli)
        {
            foreach (LibraryItem li in lli)
            {
                AddToLibrary(li);
            }
        }

        public void AddToLibrary(LibraryItem li)
        {
            if (!EntireCollection.Contains(li))
            {
                EntireCollection.Add(li);
            }
        }
    }
}
