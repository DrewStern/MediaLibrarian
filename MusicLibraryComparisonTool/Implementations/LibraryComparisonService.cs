using System.Collections.Generic;
using System.Linq;

namespace MusicLibraryCompareTool
{
    public class LibraryComparisonService
    {
        public MusicLibrary GetSum(MusicLibrary l1, MusicLibrary l2)
        {
            var sum = new MusicLibrary(l1.Collection);
            sum.AddToCollection(l2);
            return sum;
        }

        public MusicLibrary GetLeftOutersection(MusicLibrary l1, MusicLibrary l2)
        {
            return new MusicLibrary(l1.Collection.FindAll(x => !l2.Collection.Contains(x)));
        }

        public MusicLibrary GetRightOutersection(MusicLibrary l1, MusicLibrary l2)
        {
            return new MusicLibrary(l2.Collection.FindAll(x => !l1.Collection.Contains(x)));
        }

        public MusicLibrary GetFullOutersection(MusicLibrary l1, MusicLibrary l2)
        {
            var libraryItems = new List<MusicLibraryItem>();

            var largerCollection = l1.Collection.Count > l2.Collection.Count ? l1.Collection : l2.Collection;
            var smallerCollection = l1.Collection.Count > l2.Collection.Count ? l2.Collection : l1.Collection;

            libraryItems.AddRange(largerCollection.FindAll(x => !smallerCollection.Contains(x)));

            libraryItems.AddRange(smallerCollection.FindAll(x => !largerCollection.Contains(x)));

            return new MusicLibrary(libraryItems);
        }

        public MusicLibrary GetIntersection(MusicLibrary l1, MusicLibrary l2)
        {
            var largerCollection = l1.Collection.Count > l2.Collection.Count ? l1.Collection : l2.Collection;

            return  new MusicLibrary(largerCollection.FindAll(x => l1.Collection.Contains(x) && l2.Collection.Contains(x)));
        }

        /// <summary>
        /// Represents l1 - l2 (i.e., anything in the intersection of l1 and l2 is not included in the return value).
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public List<ArtistData> GetArtistDiffs(MusicLibrary l1, MusicLibrary l2)
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

        public List<MusicLibraryItem> GetReleaseDiffs(MusicLibrary ld1, MusicLibrary ld2)
        {
            var artistReleaseDiffs = new List<MusicLibraryItem>();

            foreach (MusicLibraryItem li in ld2.Collection)
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
