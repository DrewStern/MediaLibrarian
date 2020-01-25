using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryDiffService
    {
        public List<ArtistData> GetArtistDiffs(Library ld1, Library ld2)
        {
            var artistDataDiff = new List<ArtistData>();

            foreach (ArtistData artist in ld1.Artists)
            {
                if (!ld2.Artists.Contains(artist))
                {
                    artistDataDiff.Add(artist);
                }
            }

            return artistDataDiff;
        }

        public List<LibraryItem> GetArtistReleaseDiffs(Library ld1, Library ld2)
        {
            var artistReleaseDiffs = new List<LibraryItem>();

            foreach (LibraryItem ard in ld2.EntireCollection)
            {
                if (!ld1.EntireCollection.Any(x => x.Equals(ard)))
                {
                    artistReleaseDiffs.Add(ard);
                }
            }

            return artistReleaseDiffs;
        }
    }
}
