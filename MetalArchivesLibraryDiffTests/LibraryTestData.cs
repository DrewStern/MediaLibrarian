using MetalArchivesLibraryDiffTool;
using System.Collections.Generic;

namespace MetalArchivesLibraryDiffTests
{
    public static class LibraryTestData
    {
        private static LibraryData _emptyLibrary;
        private static LibraryData _duplicatedDataLibrary;
        private static LibraryData _oneToManyLibrary;
        private static LibraryData _simpleLibrary;
        private static LibraryData _disjointLibrary;
        private static LibraryData _randomLibraryOne;
        private static LibraryData _randomLibraryTwo;
        private static LibraryData _randomLibraryOneSubtractTwo;

        public static LibraryData EmptyLibrary
        {
            get
            {
                if (_emptyLibrary == null)
                {
                    List<ArtistReleaseData> ardList = new List<ArtistReleaseData> { };
                    _emptyLibrary = new LibraryData(ardList);
                }

                return _emptyLibrary;
            }
        }

        public static LibraryData DuplicatedDataLibrary
        {
            get
            {
                if (_duplicatedDataLibrary == null)
                {
                    _duplicatedDataLibrary = new LibraryData(new List<ArtistReleaseData>
                    {
                        new ArtistReleaseData("artist1", "release1"),
                        new ArtistReleaseData("artist1", "release1"),
                    });
                }

                return _duplicatedDataLibrary;
            }
        }

        public static LibraryData OneArtistToManyReleasesLibrary
        {
            get
            {
                if (_oneToManyLibrary == null)
                {
                    _oneToManyLibrary = new LibraryData(new List<ArtistReleaseData>
                    {
                        new ArtistReleaseData("artist1", "release1"),
                        new ArtistReleaseData("artist1", "release2"),
                    });
                }

                return _oneToManyLibrary;
            }
        }

        public static LibraryData ManyArtistsToManyRelasesLibrary
        {
            get
            {
                if (_simpleLibrary == null)
                {
                    _simpleLibrary = new LibraryData(new List<ArtistReleaseData>
                    {
                        new ArtistReleaseData("artist1", "release1"),
                        new ArtistReleaseData("artist1", "release2"),
                        new ArtistReleaseData("artist1", "release3"),
                        new ArtistReleaseData("artist2", "release1"),
                        new ArtistReleaseData("artist2", "release2"),
                        new ArtistReleaseData("artist2", "release3"),
                        new ArtistReleaseData("artist3", "release1"),
                        new ArtistReleaseData("artist3", "release2"),
                        new ArtistReleaseData("artist3", "release3")
                    });
                }

                return _simpleLibrary;
            }
        }

        public static LibraryData DisjointSimpleLibrary
        {
            get
            {
                if (_disjointLibrary == null)
                {
                    _disjointLibrary = new LibraryData(new List<ArtistReleaseData>
                    {
                        new ArtistReleaseData("artist4", "release4"),
                        new ArtistReleaseData("artist5", "release5"),
                        new ArtistReleaseData("artist6", "release6")
                    });
                }

                return _disjointLibrary;
            }
        }

        public static LibraryData RandomLibraryOne
        {
            get
            {
                if (_randomLibraryOne == null)
                {
                    _randomLibraryOne = new LibraryData(new List<ArtistReleaseData>
                    {
                        new ArtistReleaseData("artist1", "release1"),
                        new ArtistReleaseData("artist2", "release2")
                    });
                }

                return _randomLibraryOne;
            }
        }

        public static LibraryData RandomLibraryTwo
        {
            get
            {
                if (_randomLibraryTwo == null)
                {
                    _randomLibraryTwo = new LibraryData(new List<ArtistReleaseData>
                    {
                        new ArtistReleaseData("artist1", "release1"),
                        new ArtistReleaseData("artist1", "release2"),
                        new ArtistReleaseData("artist2", "release1"),
                        new ArtistReleaseData("artist2", "release2"),
                    });
                }

                return _randomLibraryTwo;
            }
        }

        public static LibraryData RandomLibraryOneSubtractTwo
        {
            get
            {
                if (_randomLibraryOneSubtractTwo == null)
                {
                    _randomLibraryOneSubtractTwo = new LibraryData(new List<ArtistReleaseData>
                    {
                        new ArtistReleaseData("artist1", "release2"),
                        new ArtistReleaseData("artist2", "release1"),
                    });
                }

                return _randomLibraryOneSubtractTwo;
            }
        }
    }
}
