using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibrary
{
    public class LibraryData
    {
        private List<ArtistReleaseData> _entireCollection;

        public List<ArtistReleaseData> EntireCollection
        {
            get { return _entireCollection ?? (_entireCollection = new List<ArtistReleaseData>()); }
            set { _entireCollection = value; }
        }

        public List<string> Artists
        {
            get { return _entireCollection.OrderBy(x => x.ArtistName).Select(x => x.ArtistName).Distinct().ToList(); }
        }


        public LibraryData(DirectoryInfo fromPath)
        {
            if (!fromPath.Exists)
            {
                throw new ArgumentException("The given path must exist on disk.");
            }

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
