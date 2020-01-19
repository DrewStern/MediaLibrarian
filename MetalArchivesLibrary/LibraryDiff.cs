using System;
using System.Collections.Generic;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryDiff
    {
        private LibraryData _mine;
        private LibraryData _theirs;

        public LibraryDiff(LibraryData mine, LibraryData theirs)
        {
            _mine = mine;
            _theirs = theirs;
        }

        public List<string> GetUnrecognizedArtists()
        {
            var unrecognizedArtists = new List<string>();

            foreach (string artist in _mine.Artists)
            {
                if (!_theirs.Artists.Contains(artist))
                {
                    unrecognizedArtists.Add(artist);
                }
            }

            return unrecognizedArtists;
        }

        public List<ArtistReleaseData> GetMissingAlbums()
        {
            var missingAlbums = new List<ArtistReleaseData>();

            foreach (ArtistReleaseData ard in _theirs.EntireCollection)
            {
                if (!_mine.EntireCollection.Any(x =>
                        x.ArtistName.Equals(ard.ArtistName, StringComparison.InvariantCultureIgnoreCase) &&
                        x.ReleaseName.Equals(ard.ReleaseName, StringComparison.InvariantCultureIgnoreCase) &&
                        x.Country.Equals(ard.Country, StringComparison.InvariantCultureIgnoreCase)))
                {
                    missingAlbums.Add(ard);
                }
            }

            return missingAlbums;
        }
    }
}
