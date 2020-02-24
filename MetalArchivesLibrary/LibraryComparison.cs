using System.Collections.Generic;

namespace MusicLibraryCompareTool
{
    public class LibraryComparison
    {
        private Library _sum;
        private Library _intersection;
        private Library _leftOutersection;
        private Library _rightOutersection;
        private Library _fullOutersection;

        public Library Left { get; }

        public Library Right { get; }

        public Library Sum
        {
            get
            {
                if (_sum == null)
                {
                    _sum = new Library(Left.Collection);
                    _sum.AddToCollection(Right);
                }

                return _sum;
            }
        }

        public Library LeftOutersection
        {
            get
            {
                if (_leftOutersection == null)
                {
                    _leftOutersection = new Library(Left.Collection.FindAll(x => !Right.Collection.Contains(x)));
                }

                return _leftOutersection;
            }
        }

        public Library RightOutersection
        {
            get
            {
                if (_rightOutersection == null)
                {
                    _rightOutersection = new Library(Right.Collection.FindAll(x => !Left.Collection.Contains(x)));
                }

                return _rightOutersection;
            }
        }

        public Library FullOutersection
        {
            get
            {
                if (_fullOutersection == null)
                {
                    var libraryItems = new List<LibraryItem>();

                    var largerCollection = Left.Collection.Count > Right.Collection.Count ? Left.Collection : Right.Collection;
                    var smallerCollection = Left.Collection.Count > Right.Collection.Count ? Right.Collection : Left.Collection;

                    libraryItems.AddRange(largerCollection.FindAll(x => !smallerCollection.Contains(x)));

                    libraryItems.AddRange(smallerCollection.FindAll(x => !largerCollection.Contains(x)));

                    _fullOutersection = new Library(libraryItems);
                }

                return _fullOutersection;
            }
        }

        public Library Intersection
        {
            get
            {
                if (_intersection == null)
                {
                    var largerCollection = Left.Collection.Count > Right.Collection.Count ? Left.Collection : Right.Collection;

                    _intersection = new Library(largerCollection.FindAll(x => Left.Collection.Contains(x) && Right.Collection.Contains(x)));
                }

                return _intersection;
            }
        }

        public LibraryComparison(Library l1, Library l2)
        {
            Left = l1;
            Right = l2;
        }
    }
}
