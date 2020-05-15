using System.Collections.Generic;
using System.Linq;

namespace MediaLibraryCompareTool
{
    public class MusicLibraryCompareService
    {
        public MusicLibrary GetSum(MusicLibrary l1, MusicLibrary l2)
        {
            var sum = new MusicLibrary(l1.Collection);
            sum.AddToCollection(l2);
            return sum;
        }

        public MusicLibrary GetLeftOutersection(MusicLibrary left, MusicLibrary right)
        {
            return new MusicLibrary(left.Collection.FindAll(x => !right.Collection.Contains(x)));
        }

        public MusicLibrary GetRightOutersection(MusicLibrary left, MusicLibrary right)
        {
            return new MusicLibrary(right.Collection.FindAll(x => !left.Collection.Contains(x)));
        }

        public MusicLibrary GetFullOutersection(MusicLibrary left, MusicLibrary right)
        {
            var libraryItems = new List<MusicLibraryItem>();

            var largerCollection = left.Collection.Count > right.Collection.Count ? left.Collection : right.Collection;
            var smallerCollection = left.Collection.Count > right.Collection.Count ? right.Collection : left.Collection;

            libraryItems.AddRange(largerCollection.FindAll(x => !smallerCollection.Contains(x)));

            libraryItems.AddRange(smallerCollection.FindAll(x => !largerCollection.Contains(x)));

            return new MusicLibrary(libraryItems);
        }

        public MusicLibrary GetIntersection(MusicLibrary left, MusicLibrary right)
        {
            var largerCollection = left.Collection.Count > right.Collection.Count ? left.Collection : right.Collection;

            return  new MusicLibrary(largerCollection.FindAll(x => left.Collection.Contains(x) && right.Collection.Contains(x)));
        }

        public MusicLibrary GetReleaseDiffs(MusicLibrary ld1, MusicLibrary ld2)
        {
            var artistReleaseDiffs = new List<MusicLibraryItem>();

            foreach (MusicLibraryItem li in ld2.Collection)
            {
                if (!ld1.Collection.Any(x => x.Equals(li)))
                {
                    artistReleaseDiffs.Add(li);
                }
            }

            return new MusicLibrary(artistReleaseDiffs);
        }
    }
}
