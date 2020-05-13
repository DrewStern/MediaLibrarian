using MusicLibraryCompareTool;
using System.Collections.Generic;

namespace MusicLibraryCompareTool.UnitTests
{
    public static class LibraryTestData
    {
        private static MusicLibrary _emptyLibrary;
        private static MusicLibrary _duplicatedDataLibrary;
        private static MusicLibrary _oneToManyLibrary;
        private static MusicLibrary _simpleLibrary;
        private static MusicLibrary _disjointLibrary;
        private static MusicLibrary _randomLibraryOne;
        private static MusicLibrary _randomLibraryTwo;
        private static MusicLibrary _randomLibraryOneSubtractTwo;
        private static MusicLibrary _multipleArtistsWithSameNameDifferentCountry1Library;
        private static MusicLibrary _multipleArtistsWithSameNameDifferentCountry2Library;

        public static MusicLibrary EmptyLibrary
        {
            get
            {
                if (_emptyLibrary == null)
                {
                    List<MusicLibraryItem> ardList = new List<MusicLibraryItem> { };
                    _emptyLibrary = new MusicLibrary(ardList);
                }

                return _emptyLibrary;
            }
        }

        public static MusicLibrary DuplicatedDataLibrary
        {
            get
            {
                if (_duplicatedDataLibrary == null)
                {
                    _duplicatedDataLibrary = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist1", "release1"),
                        new MusicLibraryItem("artist1", "release1"),
                    });
                }

                return _duplicatedDataLibrary;
            }
        }

        public static MusicLibrary OneArtistToManyReleasesLibrary
        {
            get
            {
                if (_oneToManyLibrary == null)
                {
                    _oneToManyLibrary = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist1", "release1"),
                        new MusicLibraryItem("artist1", "release2"),
                    });
                }

                return _oneToManyLibrary;
            }
        }

        public static MusicLibrary MultipleArtistsWithSameNameDifferentCountry1Library
        {
            get
            {
                if (_multipleArtistsWithSameNameDifferentCountry1Library == null)
                {
                    _multipleArtistsWithSameNameDifferentCountry1Library = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist1 (US)", "release1"),
                        new MusicLibraryItem("artist1 (AU)", "release1"),
                        new MusicLibraryItem("artist1 (BR)", "release1"),
                        new MusicLibraryItem("artist1 (RU)", "release1"),
                    });
                }

                return _multipleArtistsWithSameNameDifferentCountry1Library;
            }
        }

        public static MusicLibrary MultipleArtistsWithSameNameDifferentCountry2Library
        {
            get
            {
                if (_multipleArtistsWithSameNameDifferentCountry2Library == null)
                {
                    _multipleArtistsWithSameNameDifferentCountry2Library = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist1 (US)", "release1"),
                        new MusicLibraryItem("artist1 (JP)", "release1"),
                        new MusicLibraryItem("artist1 (FR)", "release1"),
                        new MusicLibraryItem("artist1 (RU)", "release1"),
                    });
                }

                return _multipleArtistsWithSameNameDifferentCountry2Library;
            }
        }

        public static MusicLibrary ManyArtistsToManyReleasesLibrary
        {
            get
            {
                if (_simpleLibrary == null)
                {
                    _simpleLibrary = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist1", "release1"),
                        new MusicLibraryItem("artist1", "release2"),
                        new MusicLibraryItem("artist1", "release3"),
                        new MusicLibraryItem("artist2", "release1"),
                        new MusicLibraryItem("artist2", "release2"),
                        new MusicLibraryItem("artist2", "release3"),
                        new MusicLibraryItem("artist3", "release1"),
                        new MusicLibraryItem("artist3", "release2"),
                        new MusicLibraryItem("artist3", "release3")
                    });
                }

                return _simpleLibrary;
            }
        }

        public static MusicLibrary DisjointSimpleLibrary
        {
            get
            {
                if (_disjointLibrary == null)
                {
                    _disjointLibrary = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist4", "release4"),
                        new MusicLibraryItem("artist5", "release5"),
                        new MusicLibraryItem("artist6", "release6")
                    });
                }

                return _disjointLibrary;
            }
        }

        public static MusicLibrary RandomLibraryOne
        {
            get
            {
                if (_randomLibraryOne == null)
                {
                    _randomLibraryOne = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist1", "release1"),
                        new MusicLibraryItem("artist2", "release2")
                    });
                }

                return _randomLibraryOne;
            }
        }

        public static MusicLibrary RandomLibraryTwo
        {
            get
            {
                if (_randomLibraryTwo == null)
                {
                    _randomLibraryTwo = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist1", "release1"),
                        new MusicLibraryItem("artist1", "release2"),
                        new MusicLibraryItem("artist2", "release1"),
                        new MusicLibraryItem("artist2", "release2"),
                    });
                }

                return _randomLibraryTwo;
            }
        }

        public static MusicLibrary RandomLibraryOneSubtractTwo
        {
            get
            {
                if (_randomLibraryOneSubtractTwo == null)
                {
                    _randomLibraryOneSubtractTwo = new MusicLibrary(new List<MusicLibraryItem>
                    {
                        new MusicLibraryItem("artist1", "release2"),
                        new MusicLibraryItem("artist2", "release1"),
                    });
                }

                return _randomLibraryOneSubtractTwo;
            }
        }
    }
}
