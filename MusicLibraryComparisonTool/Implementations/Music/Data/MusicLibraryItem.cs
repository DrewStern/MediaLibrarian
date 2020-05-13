using MediaLibraryCompareTool.Interfaces;
using System;

namespace MediaLibraryCompareTool
{
    /// <summary>
    /// Represents a release from an artist.
    /// </summary>
    public struct MusicLibraryItem : IMusicLibraryItem
    {
        public ArtistData ArtistData { get; }

        public ReleaseData ReleaseData { get; }

        public MusicLibraryItem(ArtistData ad, ReleaseData rd)
        {
            ArtistData = ad;
            ReleaseData = rd;
        }

        public MusicLibraryItem(string artistName, string releaseName)
            : this(artistName, String.Empty, releaseName)
        {
            // intentionally empty
        }

        public MusicLibraryItem(string artistName, string country, string releaseName)
            : this(artistName, country, releaseName, "Full-Length")
        {
            // intentionally empty
        }

        public MusicLibraryItem(string artistName, string country, string releaseName, string releaseType)
        {
            ArtistData = new ArtistData(artistName, country);
            ReleaseData = new ReleaseData(releaseName, releaseType);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MusicLibraryItem))
            {
                return false;
            }

            MusicLibraryItem other = (MusicLibraryItem)obj;

            return
                this.ArtistData.Equals(other.ArtistData) &&
                this.ReleaseData.Equals(other.ReleaseData);
        }

        public override string ToString()
        {
            return ArtistData.ToString() + " - " + ReleaseData.ToString();
        }
    }
}
