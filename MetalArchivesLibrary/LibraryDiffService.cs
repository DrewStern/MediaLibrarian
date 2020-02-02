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

        /// <summary>
        /// Represents l1 - l2 (i.e., anything in the intersection of l1 and l2 is not included in the return value).
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
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

            foreach (LibraryItem li in ld2.Collection)
            {
                if (!ld1.Collection.Any(x => x.Equals(li)))
                {
                    artistReleaseDiffs.Add(li);
                }
            }

            return artistReleaseDiffs;
        }
    }
}
