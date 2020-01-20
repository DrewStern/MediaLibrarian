using System;

namespace MetalArchivesLibraryDiffTool
{
    /// <summary>
    /// Represents a release from an artist.
    /// </summary>
    public struct ArtistReleaseData
    {
        public string ArtistName { get; }

        public string ReleaseName { get; }

        public string ReleaseType { get; }

        public string Country { get; }

        public ArtistReleaseData(string artistName, string releaseName)
            : this(artistName, releaseName, String.Empty)
        {
            // intentionally empty
        }

        public ArtistReleaseData(string artistName, string releaseName, string country)
            : this(artistName, releaseName, "Full-Length", country)
        {
            // intentionally empty
        }

        public ArtistReleaseData(string artistName, string releaseName, string releaseType, string country)
        {
            ArtistName = artistName;
            ReleaseName = releaseName;
            ReleaseType = releaseType;
            Country = country;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ArtistReleaseData))
            {
                return false;
            }

            ArtistReleaseData other = (ArtistReleaseData)obj;

            return 
                this.ArtistName.Equals(other.ArtistName, StringComparison.InvariantCultureIgnoreCase) &&
                this.ReleaseName.Equals(other.ReleaseName, StringComparison.InvariantCultureIgnoreCase) &&
                this.Country.Equals(other.Country, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return $"{ArtistName} - {ReleaseName}";
        }
    }
}
