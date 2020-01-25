using System;

namespace MetalArchivesLibraryDiffTool
{
    /// <summary>
    /// Represents a release from an artist.
    /// </summary>
    public struct LibraryItem
    {
        public ArtistData ArtistData { get; }

        public ReleaseData ReleaseData { get; }

        public LibraryItem(ArtistData ad, ReleaseData rd)
        {
            ArtistData = ad;
            ReleaseData = rd;
        }

        public LibraryItem(string artistName, string releaseName)
            : this(artistName, String.Empty, releaseName)
        {
            // intentionally empty
        }

        public LibraryItem(string artistName, string country, string releaseName)
            : this(artistName, country, releaseName, "Full-Length")
        {
            // intentionally empty
        }

        public LibraryItem(string artistName, string country, string releaseName, string releaseType)
        {
            ArtistData = new ArtistData(artistName, country);
            ReleaseData = new ReleaseData(releaseName, releaseType);
        }

        public override string ToString()
        {
            string optionalCountry = String.IsNullOrWhiteSpace(ArtistData.Country) ? String.Empty : "(ArtistData.Country)";
            return $"{ArtistData.ArtistName} {optionalCountry} - {ReleaseData.ReleaseName}";
        }
    }
}
