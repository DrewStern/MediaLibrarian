using MetalArchivesLibraryDiffTool;
using System.Collections.Generic;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryDiff
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
                    var libraryItems = new List<LibraryItem>();

                    for (var i = 0; i < Left.Collection.Count; i++)
                    {
                        var itemFromLargerCollection = Left.Collection[i];

                        if (!Right.Collection.Contains(itemFromLargerCollection))
                        {
                            libraryItems.Add(itemFromLargerCollection);
                        }
                    }

                    _leftOutersection = new Library(libraryItems);
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
                    var libraryItems = new List<LibraryItem>();

                    for (var i = 0; i < Right.Collection.Count; i++)
                    {
                        var itemFromLargerCollection = Right.Collection[i];

                        if (!Left.Collection.Contains(itemFromLargerCollection))
                        {
                            libraryItems.Add(itemFromLargerCollection);
                        }
                    }

                    _rightOutersection = new Library(libraryItems);
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

                    // TODO: refactor this - need to avoid the case where the collections are the same size, but because the ternary
                    // below defaults to the Right.Collection, then we weren't including the outersection from the Left.Collection
                    var largerCollection = Left.Collection.Count > Right.Collection.Count ? Left.Collection : Right.Collection;
                    var smallerCollection = Left.Collection.Count > Right.Collection.Count ? Right.Collection : Left.Collection;

                    foreach (LibraryItem li in largerCollection)
                    {
                        if (smallerCollection.Contains(li))
                        {
                            continue;
                        }

                        libraryItems.Add(li);
                    }

                    foreach (LibraryItem li in smallerCollection)
                    {
                        if (largerCollection.Contains(li))
                        {
                            continue;
                        }

                        libraryItems.Add(li);
                    }

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
                    var libaryItems = new List<LibraryItem>();

                    var largerCollection = Left.Collection.Count > Right.Collection.Count ? Left.Collection : Right.Collection;

                    for (var i = 0; i < largerCollection.Count; i++)
                    {
                        var itemFromLargerCollection = largerCollection[i];

                        if (Left.Collection.Contains(itemFromLargerCollection) && Right.Collection.Contains(itemFromLargerCollection))
                        {
                            libaryItems.Add(itemFromLargerCollection);
                        }
                    }

                    _intersection = new Library(libaryItems);
                }

                return _intersection;
            }
        }

        public LibraryDiff(Library l1, Library l2)
        {
            Left = l1;
            Right = l2;
        }
    }
}
