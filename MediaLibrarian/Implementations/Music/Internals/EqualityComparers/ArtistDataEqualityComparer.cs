using System;
using System.Collections.Generic;

namespace MediaLibrarian
{
    public class ArtistDataEqualityComparer : IEqualityComparer<ArtistData>
    {
        public bool Equals(ArtistData ad1, ArtistData ad2)
        {
            return
                ad1.ArtistName.Equals(ad2.ArtistName, StringComparison.InvariantCultureIgnoreCase) &&
                ad1.Country.Equals(ad2.Country, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(ArtistData ad)
        {
            return base.GetHashCode();
        }
    }
}
