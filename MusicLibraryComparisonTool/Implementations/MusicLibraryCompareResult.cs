using System.Collections.Generic;

namespace MusicLibraryCompareTool
{
    public class MusicLibraryCompareResult
    {
        private MusicLibrary _sum;
        private MusicLibrary _intersection;
        private MusicLibrary _leftOutersection;
        private MusicLibrary _rightOutersection;
        private MusicLibrary _fullOutersection;

        public MusicLibrary Left { get; }

        public MusicLibrary Right { get; }

        public MusicLibrary Sum
        {
            get
            {
                if (_sum == null)
                {
                    _sum = new MusicLibrary(Left.Collection);
                    _sum.AddToCollection(Right);
                }

                return _sum;
            }
        }

        public MusicLibrary LeftOutersection
        {
            get
            {
                if (_leftOutersection == null)
                {
                    _leftOutersection = new MusicLibrary(Left.Collection.FindAll(x => !Right.Collection.Contains(x)));
                }

                return _leftOutersection;
            }
        }

        public MusicLibrary RightOutersection
        {
            get
            {
                if (_rightOutersection == null)
                {
                    _rightOutersection = new MusicLibrary(Right.Collection.FindAll(x => !Left.Collection.Contains(x)));
                }

                return _rightOutersection;
            }
        }

        public MusicLibrary FullOutersection
        {
            get
            {
                if (_fullOutersection == null)
                {
                    var libraryItems = new List<MusicLibraryItem>();

                    var largerCollection = Left.Collection.Count > Right.Collection.Count ? Left.Collection : Right.Collection;
                    var smallerCollection = Left.Collection.Count > Right.Collection.Count ? Right.Collection : Left.Collection;

                    libraryItems.AddRange(largerCollection.FindAll(x => !smallerCollection.Contains(x)));

                    libraryItems.AddRange(smallerCollection.FindAll(x => !largerCollection.Contains(x)));

                    _fullOutersection = new MusicLibrary(libraryItems);
                }

                return _fullOutersection;
            }
        }

        public MusicLibrary Intersection
        {
            get
            {
                if (_intersection == null)
                {
                    var largerCollection = Left.Collection.Count > Right.Collection.Count ? Left.Collection : Right.Collection;

                    _intersection = new MusicLibrary(largerCollection.FindAll(x => Left.Collection.Contains(x) && Right.Collection.Contains(x)));
                }

                return _intersection;
            }
        }

        public MusicLibraryCompareResult(MusicLibrary l1, MusicLibrary l2)
        {
            Left = l1;
            Right = l2;
        }
    }
}
