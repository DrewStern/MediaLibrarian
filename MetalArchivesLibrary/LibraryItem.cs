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

        public LibraryItem(string artistName, string releaseName)
            : this(artistName, releaseName, String.Empty)
        {
            // intentionally empty
        }

        public LibraryItem(string artistName, string releaseName, string country)
            : this(artistName, releaseName, "Full-Length", country)
        {
            // intentionally empty
        }

        public LibraryItem(string artistName, string releaseName, string releaseType, string country)
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
