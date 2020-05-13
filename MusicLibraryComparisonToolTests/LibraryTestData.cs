using MusicLibraryCompareTool;
using System.Collections.Generic;

namespace MusicLibraryCompareTool.UnitTests
{
    public static class LibraryTestData
    {
        private static Library _emptyLibrary;
        private static Library _duplicatedDataLibrary;
        private static Library _oneToManyLibrary;
        private static Library _simpleLibrary;
        private static Library _disjointLibrary;
        private static Library _randomLibraryOne;
        private static Library _randomLibraryTwo;
        private static Library _randomLibraryOneSubtractTwo;
        private static Library _multipleArtistsWithSameNameDifferentCountry1Library;
        private static Library _multipleArtistsWithSameNameDifferentCountry2Library;

        public static Library EmptyLibrary
        {
            get
            {
                if (_emptyLibrary == null)
                {
                    List<LibraryItem> ardList = new List<LibraryItem> { };
                    _emptyLibrary = new Library(ardList);
                }

                return _emptyLibrary;
            }
        }

        public static Library DuplicatedDataLibrary
        {
            get
            {
                if (_duplicatedDataLibrary == null)
                {
                    _duplicatedDataLibrary = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist1", "release1"),
                        new LibraryItem("artist1", "release1"),
                    });
                }

                return _duplicatedDataLibrary;
            }
        }

        public static Library OneArtistToManyReleasesLibrary
        {
            get
            {
                if (_oneToManyLibrary == null)
                {
                    _oneToManyLibrary = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist1", "release1"),
                        new LibraryItem("artist1", "release2"),
                    });
                }

                return _oneToManyLibrary;
            }
        }

        public static Library MultipleArtistsWithSameNameDifferentCountry1Library
        {
            get
            {
                if (_multipleArtistsWithSameNameDifferentCountry1Library == null)
                {
                    _multipleArtistsWithSameNameDifferentCountry1Library = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist1 (US)", "release1"),
                        new LibraryItem("artist1 (AU)", "release1"),
                        new LibraryItem("artist1 (BR)", "release1"),
                        new LibraryItem("artist1 (RU)", "release1"),
                    });
                }

                return _multipleArtistsWithSameNameDifferentCountry1Library;
            }
        }

        public static Library MultipleArtistsWithSameNameDifferentCountry2Library
        {
            get
            {
                if (_multipleArtistsWithSameNameDifferentCountry2Library == null)
                {
                    _multipleArtistsWithSameNameDifferentCountry2Library = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist1 (US)", "release1"),
                        new LibraryItem("artist1 (JP)", "release1"),
                        new LibraryItem("artist1 (FR)", "release1"),
                        new LibraryItem("artist1 (RU)", "release1"),
                    });
                }

                return _multipleArtistsWithSameNameDifferentCountry2Library;
            }
        }

        public static Library ManyArtistsToManyRelasesLibrary
        {
            get
            {
                if (_simpleLibrary == null)
                {
                    _simpleLibrary = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist1", "release1"),
                        new LibraryItem("artist1", "release2"),
                        new LibraryItem("artist1", "release3"),
                        new LibraryItem("artist2", "release1"),
                        new LibraryItem("artist2", "release2"),
                        new LibraryItem("artist2", "release3"),
                        new LibraryItem("artist3", "release1"),
                        new LibraryItem("artist3", "release2"),
                        new LibraryItem("artist3", "release3")
                    });
                }

                return _simpleLibrary;
            }
        }

        public static Library DisjointSimpleLibrary
        {
            get
            {
                if (_disjointLibrary == null)
                {
                    _disjointLibrary = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist4", "release4"),
                        new LibraryItem("artist5", "release5"),
                        new LibraryItem("artist6", "release6")
                    });
                }

                return _disjointLibrary;
            }
        }

        public static Library RandomLibraryOne
        {
            get
            {
                if (_randomLibraryOne == null)
                {
                    _randomLibraryOne = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist1", "release1"),
                        new LibraryItem("artist2", "release2")
                    });
                }

                return _randomLibraryOne;
            }
        }

        public static Library RandomLibraryTwo
        {
            get
            {
                if (_randomLibraryTwo == null)
                {
                    _randomLibraryTwo = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist1", "release1"),
                        new LibraryItem("artist1", "release2"),
                        new LibraryItem("artist2", "release1"),
                        new LibraryItem("artist2", "release2"),
                    });
                }

                return _randomLibraryTwo;
            }
        }

        public static Library RandomLibraryOneSubtractTwo
        {
            get
            {
                if (_randomLibraryOneSubtractTwo == null)
                {
                    _randomLibraryOneSubtractTwo = new Library(new List<LibraryItem>
                    {
                        new LibraryItem("artist1", "release2"),
                        new LibraryItem("artist2", "release1"),
                    });
                }

                return _randomLibraryOneSubtractTwo;
            }
        }
    }
}
