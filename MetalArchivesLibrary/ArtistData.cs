using System;

namespace MetalArchivesLibraryDiffTool
{
    public class ArtistData
    {
        public string ArtistName { get; }

        public string Country { get; }

        public ArtistData(string artistName)
            : this(artistName, String.Empty)
        {
        }

        public ArtistData(string artistName, string country)
        {
            ArtistName = artistName;
            Country = country;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ArtistData))
            {
                return false;
            }

            ArtistData other = (ArtistData)obj;

            return
                this.ArtistName.Equals(other.ArtistName, StringComparison.InvariantCultureIgnoreCase) &&
                this.Country.Equals(other.Country, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string optionalCountry = String.IsNullOrWhiteSpace(Country) ? String.Empty : $" ({Country})";
            return $"{ArtistName}{optionalCountry}";
        }
    }
}
