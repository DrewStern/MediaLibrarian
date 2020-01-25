using System.Collections.Generic;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryDiffService
    {
        public LibraryDiffService()
        {
            // intentionally empty
        }

        public List<LibraryItem> GetDiffs(Library l1, Library l2)
        {
            var diffs = new List<LibraryItem>();

            foreach (LibraryItem item in l1.EntireCollection)
            {
                if (!l2.EntireCollection.Contains(item))
                {
                    diffs.Add(item);
                }
            }

            return diffs;
        }

        public List<ArtistData> GetArtistDiffs(Library l1, Library l2)
        {
            var artistDataDiff = new List<ArtistData>();

            foreach (ArtistData artist in l1.Artists)
            {
                if (!l2.Artists.Contains(artist))
                {
                    artistDataDiff.Add(artist);
                }
            }

            return artistDataDiff;
        }

        public List<LibraryItem> GetReleaseDiffs(Library ld1, Library ld2)
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
