using MediaLibraryCompareTool.Interfaces;

namespace MediaLibraryCompareTool
{
    public abstract class BaseLibraryComparer<TLibraryItem, TLibrary, TLibraryCompareResult> 
        : ILibraryComparer<TLibraryItem, TLibrary, TLibraryCompareResult>
        where TLibraryItem : ILibraryItem
        where TLibrary : ILibrary<TLibraryItem>, new()
        where TLibraryCompareResult : ILibraryCompareResult<TLibraryItem, TLibrary>
    {
        public abstract TLibraryCompareResult Compare(TLibrary left, TLibrary right);

        protected TLibrary GetSum(TLibrary l1, TLibrary l2)
        {
            var sum = new TLibrary();
            sum.AddToCollection(l1);
            sum.AddToCollection(l2);
            return sum;
        }

        protected TLibrary GetIntersection(TLibrary left, TLibrary right)
        {
            var largerCollection = left.Collection.Count > right.Collection.Count ? left.Collection : right.Collection;
            var intersection = new TLibrary();
            intersection.AddToCollection(largerCollection.FindAll(x => left.Collection.Contains(x) && right.Collection.Contains(x)));
            return intersection;
        }

        protected TLibrary GetLeftOutersection(TLibrary left, TLibrary right)
        {
            var leftOutersection = new TLibrary();
            leftOutersection.AddToCollection(left.Collection.FindAll(x => !right.Collection.Contains(x)));
            return leftOutersection;
        }

        protected TLibrary GetRightOutersection(TLibrary left, TLibrary right)
        {
            var rightOutersection = new TLibrary();
            rightOutersection.AddToCollection(right.Collection.FindAll(x => !left.Collection.Contains(x)));
            return rightOutersection;
        }

        protected TLibrary GetFullOutersection(TLibrary left, TLibrary right)
        {
            var leftOutersection = GetLeftOutersection(left, right);
            var rightOutersection = GetRightOutersection(left, right);

            var fullOutersection = new TLibrary();
            fullOutersection.AddToCollection(leftOutersection);
            fullOutersection.AddToCollection(rightOutersection);
            return fullOutersection;
        }
    }
}
