using System;
using System.Collections.Generic;

namespace MediaLibraryCompareTool
{
    public class MetalArchivesResponseParser
    {
        public MusicLibrary Parse(MetalArchivesResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException($"{nameof(response)} may not be null");
            }

            var libraryItems = new List<MusicLibraryItem>();

            // Each entry has three components - the first represents the band name, the second the album name, and third the release type. Example:
            // [0] == <a href="https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265" title="!T.O.O.H.! (CZ)">!T.O.O.H.!</a>
            // [1] == <a href="https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622">Democratic Solution</a> <!-- 7.792132 -->
            // [2] == Full-length
            foreach (string[] entry in response.aaData)
            {
                ArtistData artistData = ExtractArtistData(entry[0]);
                ReleaseData releaseData = ExtractReleaseData(entry[1], entry[2]);

                MusicLibraryItem data = new MusicLibraryItem(artistData, releaseData);

                // only care about full-lengths for now
                if (!data.ReleaseData.IsFullLength)
                {
                    continue;
                }

                libraryItems.Add(data);
            }

            return new MusicLibrary(libraryItems);
        }

        public ArtistData ExtractArtistData(string htmlArtistData)
        {
            var artistName = ExtractArtistName(htmlArtistData);
            var country = ExtractCountry(htmlArtistData);
            return new ArtistData(artistName, country);
        }

        public ReleaseData ExtractReleaseData(string htmlReleaseName, string htmlReleaseType)
        {
            var artistName = ExtractReleaseName(htmlReleaseName);
            var releaseType = htmlReleaseType;
            return new ReleaseData(artistName, releaseType);
        }

        /// <remarks>
        /// There might be two bands with the same name, but from different countries. 
        /// This is used to differentiate in those cases.
        /// </remarks>
        public string ExtractArtistName(string htmlArtistData)
        {
            string artistData = StripOutHtml(htmlArtistData);

            // strip out the country identifier 
            // TODO: just determine the country at this point and simplify all calls made to this class?
            string artistName = artistData.Replace("(" + ExtractCountry(artistData) + ")", string.Empty).Trim();

            return artistName;
        }

        public string ExtractCountry(string artistNameWithCountry)
        {
            const string startOfCountryId = "(";
            const string endOfCountryId = ")";

            int startOfCountryIndex = artistNameWithCountry.IndexOf(startOfCountryId);
            int endOfCountryIndex = artistNameWithCountry.IndexOf(endOfCountryId);

            return artistNameWithCountry.Substring(startOfCountryIndex + startOfCountryId.Length, endOfCountryIndex - startOfCountryIndex - startOfCountryId.Length);
        }

        /// <remarks>
        /// The http response wraps the release name in html. This function strips the html away.
        /// </remarks>
        public string ExtractReleaseName(string dirtiedReleaseName)
        {
            // we use these to determine the begin/end of the html wrapping the album name
            const string endOfOpenHtmlTag = "\">";
            const string startOfCloseHtmlTag = "</a>";

            // gather these indices so we can use substring below
            int endOfOpenHtmlIndex = dirtiedReleaseName.IndexOf(endOfOpenHtmlTag);
            int startOfCloseHtmlIndex = dirtiedReleaseName.IndexOf(startOfCloseHtmlTag);

            // take the substring in between the html wrapping
            return dirtiedReleaseName.Substring(endOfOpenHtmlIndex + endOfOpenHtmlTag.Length, startOfCloseHtmlIndex - endOfOpenHtmlIndex - endOfOpenHtmlTag.Length);
        }

        private string StripOutHtml(string htmlArtistData)
        {
            const string startOfTitleAttribute = " title=\"";
            const string endOfTitleAttribute = "\">";

            int startOfTitleAttributeIndex = htmlArtistData.IndexOf(startOfTitleAttribute);
            int endOfTitleAttributeIndex = htmlArtistData.IndexOf(endOfTitleAttribute);

            return htmlArtistData.Substring(
                startOfTitleAttributeIndex + startOfTitleAttribute.Length,
                endOfTitleAttributeIndex - startOfTitleAttributeIndex - startOfTitleAttribute.Length);
        }
    }
}