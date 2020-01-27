using System.Collections.Generic;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryItemEqualityComparer : IEqualityComparer<LibraryItem>
    {
        public bool Equals(LibraryItem x, LibraryItem y)
        {
            return
                x.ArtistData.Equals(y.ArtistData) &&
                x.ReleaseData.Equals(y.ReleaseData);
        }

        public int GetHashCode(LibraryItem ard)
        {
            return base.GetHashCode();
        }
    }
}
