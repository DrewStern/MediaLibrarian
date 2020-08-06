using System.Collections.Generic;

namespace MediaLibrarian
{
    public class MusicLibraryItemEqualityComparer : IEqualityComparer<MusicLibraryItem>
    {
        public bool Equals(MusicLibraryItem x, MusicLibraryItem y)
        {
            return
                x.ArtistData.Equals(y.ArtistData) &&
                x.ReleaseData.Equals(y.ReleaseData);
        }

        public int GetHashCode(MusicLibraryItem ard)
        {
            return base.GetHashCode();
        }
    }
}
