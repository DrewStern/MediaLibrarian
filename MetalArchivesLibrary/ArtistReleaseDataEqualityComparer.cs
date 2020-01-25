using System.Collections.Generic;

namespace MetalArchivesLibraryDiffTool
{
    public class ArtistReleaseDataEqualityComparer : IEqualityComparer<ArtistReleaseData>
    {
        public bool Equals(ArtistReleaseData ard1, ArtistReleaseData ard2)
        {
            return
                ard1.ArtistData.Equals(ard2.ArtistData) &&
                ard1.ReleaseData.Equals(ard2.ReleaseData);
        }

        public int GetHashCode(ArtistReleaseData ard)
        {
            return base.GetHashCode();
        }
    }
}
