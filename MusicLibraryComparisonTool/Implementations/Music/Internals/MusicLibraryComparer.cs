using System.Collections.Generic;

namespace MediaLibraryCompareTool
{
    public class MusicLibraryComparer : BaseLibraryComparer<MusicLibraryItem, MusicLibrary, MusicLibraryCompareResult>
    {
        #region Public methods

        public override MusicLibraryCompareResult Compare(MusicLibrary left, MusicLibrary right)
        {
            return new MusicLibraryCompareResult(
                left, right,
                GetSum(left, right),
                GetIntersection(left, right),
                GetLeftOutersection(left, right),
                GetRightOutersection(left, right),
                GetFullOutersection(left, right));
        }

        #endregion

        #region Private methods

        private MusicLibrary GetSum(MusicLibrary l1, MusicLibrary l2)
        {
            var sum = new MusicLibrary(l1.Collection);
            sum.AddToCollection(l2);
            return sum;
        }

        private MusicLibrary GetIntersection(MusicLibrary left, MusicLibrary right)
        {
            var largerCollection = left.Collection.Count > right.Collection.Count ? left.Collection : right.Collection;
            return new MusicLibrary(largerCollection.FindAll(x => left.Collection.Contains(x) && right.Collection.Contains(x)));
        }

        private MusicLibrary GetLeftOutersection(MusicLibrary left, MusicLibrary right)
        {
            return new MusicLibrary(left.Collection.FindAll(x => !right.Collection.Contains(x)));
        }

        private MusicLibrary GetRightOutersection(MusicLibrary left, MusicLibrary right)
        {
            return new MusicLibrary(right.Collection.FindAll(x => !left.Collection.Contains(x)));
        }

        private MusicLibrary GetFullOutersection(MusicLibrary left, MusicLibrary right)
        {
            var libraryItems = new List<MusicLibraryItem>();

            var largerCollection = left.Collection.Count > right.Collection.Count ? left.Collection : right.Collection;
            var smallerCollection = left.Collection.Count > right.Collection.Count ? right.Collection : left.Collection;

            libraryItems.AddRange(largerCollection.FindAll(x => !smallerCollection.Contains(x)));
            libraryItems.AddRange(smallerCollection.FindAll(x => !largerCollection.Contains(x)));

            return new MusicLibrary(libraryItems);
        }

        #endregion
    }
}
