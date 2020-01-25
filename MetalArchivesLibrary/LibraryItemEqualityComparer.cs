using System.Collections.Generic;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryItemEqualityComparer : IEqualityComparer<LibraryItem>
    {
        public bool Equals(LibraryItem ard1, LibraryItem ard2)
        {
            return
                ard1.ArtistData.Equals(ard2.ArtistData) &&
                ard1.ReleaseData.Equals(ard2.ReleaseData);
        }

        public int GetHashCode(LibraryItem ard)
        {
            return base.GetHashCode();
        }
    }
}
