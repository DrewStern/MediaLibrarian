using System.Collections.Generic;

namespace MetalArchivesLibraryDiffTool
{
    public class ArtistDataEqualityComparer : IEqualityComparer<ArtistData>
    {
        public bool Equals(ArtistData ad1, ArtistData ad2)
        {
            return
                ad1.ArtistName.Equals(ad2.ArtistName) &&
                ad1.Country.Equals(ad2.Country);
        }

        public int GetHashCode(ArtistData ad)
        {
            return base.GetHashCode();
        }
    }
}
