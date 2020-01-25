using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class Library
    {
        private LibraryItemEqualityComparer _comparer;

        public List<LibraryItem> EntireCollection { get; }

        public List<ArtistData> Artists
        {
            get
            {
                return EntireCollection.
                    Select(x => x.ArtistData).
                    Distinct().
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
                if (_comparer == null)
                {
                    _comparer = new LibraryItemEqualityComparer();
                }

                return _comparer;
            }
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
    }
}
