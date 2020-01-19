using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryData
    {
        public List<ArtistReleaseData> EntireCollection { get; }

        public List<string> Artists
        {
            get { return EntireCollection.OrderBy(x => x.ArtistName).Select(x => x.ArtistName).Distinct().ToList(); }
        }

        public List<string> Releases
        {
            get { return EntireCollection.OrderBy(x => x.ReleaseName).Select(x => x.ReleaseName).Distinct().ToList(); }
        }

        public LibraryData(List<ArtistReleaseData> libraryData)
        {
            EntireCollection = libraryData;
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
