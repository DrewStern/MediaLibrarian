using System.Collections.Generic;
using System.Linq;

namespace MusicLibraryCompareTool
{
    public class LibraryComparisonService
    {
        public LibraryComparisonService()
        {
            // intentionally empty
        }

        public Library GetSum(Library l1, Library l2)
        {
            var sum = new Library(l1.Collection);
            sum.AddToCollection(l2);
            return sum;
        }

        public Library GetLeftOutersection(Library l1, Library l2)
        {
            return new Library(l1.Collection.FindAll(x => !l2.Collection.Contains(x)));
        }

        public Library GetRightOutersection(Library l1, Library l2)
        {
            return new Library(l2.Collection.FindAll(x => !l1.Collection.Contains(x)));
        }

        public Library GetFullOutersection(Library l1, Library l2)
        {
            var libraryItems = new List<LibraryItem>();

            var largerCollection = l1.Collection.Count > l2.Collection.Count ? l1.Collection : l2.Collection;
            var smallerCollection = l1.Collection.Count > l2.Collection.Count ? l2.Collection : l1.Collection;

            libraryItems.AddRange(largerCollection.FindAll(x => !smallerCollection.Contains(x)));

            libraryItems.AddRange(smallerCollection.FindAll(x => !largerCollection.Contains(x)));

            return new Library(libraryItems);
        }

        public Library GetIntersection(Library l1, Library l2)
        {
            var largerCollection = l1.Collection.Count > l2.Collection.Count ? l1.Collection : l2.Collection;

            return  new Library(largerCollection.FindAll(x => l1.Collection.Contains(x) && l2.Collection.Contains(x)));
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
