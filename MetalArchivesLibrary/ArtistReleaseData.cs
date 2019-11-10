namespace MetalArchivesLibrary
{
    /// <summary>
    /// Represents a release from an artist.
    /// </summary>
    public struct ArtistReleaseData
    {
        public string ArtistName { get; }

        public string ReleaseName { get; }

        public string ReleaseType { get; }

        public ArtistReleaseData(string artistName, string releaseName)
            : this(artistName, releaseName, "Full-Length")
        {

        }

        public ArtistReleaseData(string artistName, string releaseName, string releaseType)
        {
            ArtistName = artistName;
            ReleaseName = releaseName;
            ReleaseType = releaseType;
        }

        public override string ToString()
        {
            return $"{ArtistName} - {ReleaseName}";
        }
    }
}
