using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryData
    {
        private ArtistReleaseDataEqualityComparer _comparer;

        public List<ArtistReleaseData> EntireCollection { get; }

        public List<string> Artists
        {
            get
            {
                return EntireCollection.
                    OrderBy(x => x.ArtistData.ArtistName).
                    Select(x => x.ArtistData.ArtistName).
                    Distinct().
                    ToList();
            }
        }

        public List<string> Releases
        {
            get
            {
                return EntireCollection.
                    OrderBy(x => x.ReleaseData.ReleaseName).
                    Select(x => x.ReleaseData.ReleaseName).
                    Distinct().
                    ToList();
            }
        }

        private ArtistReleaseDataEqualityComparer Comparer
        {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new ArtistReleaseDataEqualityComparer();
                }

                return _comparer;
            }
        }

        public LibraryData(List<ArtistReleaseData> libraryData)
        {
            // prevent the client from adding duplicate data to our set
            // TODO: maybe OrderByDescending(x => x.ArtistReleaseData.ToString())
            EntireCollection = libraryData.
                Distinct(Comparer).
                OrderByDescending(x => x.ArtistData.ArtistName).
                OrderByDescending(x => x.ReleaseData.ReleaseName).
                ToList();
        }

        public LibraryData(DirectoryInfo fromPath)
        {
            if (!fromPath.Exists)
            {
                throw new ArgumentException("The given path must exist on disk.");
            }

            EntireCollection = new List<ArtistReleaseData>();

            foreach (DirectoryInfo artistLayer in fromPath.GetDirectories())
            {
                foreach (DirectoryInfo albumLayer in artistLayer.GetDirectories())
                {
                    EntireCollection.Add(new ArtistReleaseData(artistLayer.Name, albumLayer.Name));
                }
            }
        }
    }
}
