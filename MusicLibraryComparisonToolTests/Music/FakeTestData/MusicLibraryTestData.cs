using MediaLibraryCompareTool;
using System.Collections.Generic;

namespace MediaLibraryCompareTool.UnitTests
{
    public class MusicLibraryTestData
    {
        public MusicLibrary GetEmptyLibrary()
        {
            return new MusicLibrary(new List<MusicLibraryItem> { });
        }

        public MusicLibrary GetDuplicatedDataLibrary()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
            {
                new MusicLibraryItem("artist1", "release1"),
                new MusicLibraryItem("artist1", "release1"),
            });
        }

        public MusicLibrary GetOneArtistToManyReleasesLibrary()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
            {
                new MusicLibraryItem("artist1", "release1"),
                new MusicLibraryItem("artist1", "release2"),
            });
        }

        public MusicLibrary GetMultipleArtistsWithSameNameDifferentCountry1Library()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
            {
                new MusicLibraryItem("artist1 (US)", "release1"),
                new MusicLibraryItem("artist1 (AU)", "release1"),
                new MusicLibraryItem("artist1 (BR)", "release1"),
                new MusicLibraryItem("artist1 (RU)", "release1"),
            });
        }

        public MusicLibrary GetMultipleArtistsWithSameNameDifferentCountry2Library()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
            {
                new MusicLibraryItem("artist1 (US)", "release1"),
                new MusicLibraryItem("artist1 (JP)", "release1"),
                new MusicLibraryItem("artist1 (FR)", "release1"),
                new MusicLibraryItem("artist1 (RU)", "release1"),
            });
        }

        public MusicLibrary GetManyArtistsToManyReleasesLibrary()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
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

        public MusicLibrary GetDisjointSimpleLibrary()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
            {
                new MusicLibraryItem("artist4", "release4"),
                new MusicLibraryItem("artist5", "release5"),
                new MusicLibraryItem("artist6", "release6")
            });
        }

        public MusicLibrary GetRandomLibraryOne()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
            {
                new MusicLibraryItem("artist1", "release1"),
                new MusicLibraryItem("artist2", "release2")
            });
        }

        public MusicLibrary GetRandomLibraryTwo()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
            {
                new MusicLibraryItem("artist1", "release1"),
                new MusicLibraryItem("artist1", "release2"),
                new MusicLibraryItem("artist2", "release1"),
                new MusicLibraryItem("artist2", "release2"),
            });
        }

        public MusicLibrary GetRandomLibraryOneSubtractTwo()
        {
            return new MusicLibrary(new List<MusicLibraryItem>
            {
                new MusicLibraryItem("artist1", "release2"),
                new MusicLibraryItem("artist2", "release1"),
            });
        }
    }
}
