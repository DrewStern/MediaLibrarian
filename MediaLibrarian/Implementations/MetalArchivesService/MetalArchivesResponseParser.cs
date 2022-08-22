using System;
using System.Collections.Generic;

namespace MediaLibrarian
{
    public class MetalArchivesResponseParser
    {
        #region Public methods

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
                var artistData = GetArtistData(entry[0]);
                var releaseData = GetReleaseData(entry[1], entry[2]);
                var musicLibraryItem = new MusicLibraryItem(artistData, releaseData);
                libraryItems.Add(musicLibraryItem);
            }

            return new MusicLibrary(libraryItems);
        }

        #endregion

        #region Internal methods

        /// <remarks>Marked as internal to allow for testing.</remarks>
        internal ArtistData GetArtistData(string htmlArtistData)
        {
            var artistData = ExtractArtistData(htmlArtistData);
            var artistName = ExtractArtistName(artistData);
            var country = ExtractCountry(artistData);
            return new ArtistData(artistName, country);
        }

        /// <remarks>Marked as internal to allow for testing.</remarks>
        internal ReleaseData GetReleaseData(string htmlReleaseData, string htmlReleaseType)
        {
            var releaseType = htmlReleaseType;
            var artistName = ExtractReleaseType(ExtractReleaseData(htmlReleaseData), releaseType);
            return new ReleaseData(artistName, releaseType);
        }

        #endregion

        #region Private methods

        private string ExtractArtistData(string htmlArtistData)
        {
            const string startOfTitleAttribute = " title=\"";
            const string endOfTitleAttribute = "\">";

            int startOfTitleAttributeIndex = htmlArtistData.IndexOf(startOfTitleAttribute);
            int endOfTitleAttributeIndex = htmlArtistData.IndexOf(endOfTitleAttribute);

            return htmlArtistData.Substring(
                startOfTitleAttributeIndex + startOfTitleAttribute.Length,
                endOfTitleAttributeIndex - startOfTitleAttributeIndex - startOfTitleAttribute.Length);
        }

        private string ExtractArtistName(string combinedArtistData)
        {
            return combinedArtistData.Substring(0, combinedArtistData.IndexOf("(")).Trim();
        }

        private string ExtractCountry(string combinedArtistData)
        {
            const string startOfCountryId = "(";
            const string endOfCountryId = ")";

            int startOfCountryIndex = combinedArtistData.IndexOf(startOfCountryId);
            int endOfCountryIndex = combinedArtistData.IndexOf(endOfCountryId);

            return combinedArtistData.Substring(startOfCountryIndex + startOfCountryId.Length, endOfCountryIndex - startOfCountryIndex - startOfCountryId.Length);
        }

        private string ExtractReleaseData(string dirtiedReleaseName)
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

        private string ExtractReleaseType(string releaseData, string releaseType)
        {
            return releaseData.Replace("(" + releaseType + ")", string.Empty).Trim();
        }

        #endregion
    }
}