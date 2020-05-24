using System;

namespace MediaLibraryCompareTool
{
    public class ArtistData
    {
        public string ArtistName { get; }

        public string Country { get; }

        public ArtistData(string artistName)
            : this(artistName, string.Empty)
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

        public override string ToString()
        {
            string optionalCountry = String.IsNullOrWhiteSpace(Country) ? string.Empty : $" ({Country})";
            return $"{ArtistName}{optionalCountry}";
        }
    }
}
