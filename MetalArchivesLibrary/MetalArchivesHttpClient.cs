using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Script.Serialization;

namespace MetalArchivesLibrary
{
    public static partial class MetalArchivesHttpClient
    {
        #region Fields

        private static int _retryLimit = 5;
        private static int _retryCount = 0;

        /// <summary>
        /// Represents the most generic possible query to the Metal Archives database. 
        /// Currently we are only interested in querying based on artists, but the other parameters can be specified at a later time.
        /// </summary>
        private static string _albumQuery =
            "https://www.metal-archives.com/search/ajax-advanced/searching/albums?" +
            "bandName={0}&releaseTitle=&releaseYearFrom=&releaseMonthFrom=&releaseYearTo=&releaseMonthTo=&country=&location=&" +
            "releaseLabelName=&releaseCatalogNumber=&releaseIdentifiers=&releaseRecordingInfo=&releaseDescription=&releaseNotes=&genre=#albums";

        private static HttpClient _client;

        #endregion

        #region Properties

        private static HttpClient Client
        {
            get { return (_client ?? (_client = new HttpClient { BaseAddress = new Uri(_albumQuery)})); }
        }

        #endregion

        #region Methods

        public static List<ArtistReleaseData> FindReleases(string bandName)
        {
            _retryCount = 0;

            if (String.IsNullOrWhiteSpace(bandName))
            {
                throw new ArgumentException($"{nameof(bandName)} may not be null or empty");
            }

            // facilitates bands which have spaces in their name
            string cleanedBandName = "\"" + bandName + "\"";

            var maHttpResponse = GetResponseAsync(new Uri(string.Format(_albumQuery, cleanedBandName)));

            return maHttpResponse == null ? new List<ArtistReleaseData>() : Listify(maHttpResponse);
        }

        private static List<ArtistReleaseData> Listify(MetalArchivesHttpResponse maHttpResponse)
        {
            var albums = new List<ArtistReleaseData>();

            // Each entry has three components - the first represents the band name, the second the album name, and third the release type. Example:
            // [0] == <a href="https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265" title="!T.O.O.H.! (CZ)">!T.O.O.H.!</a>
            // [1] == <a href="https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622">Democratic Solution</a> <!-- 7.792132 -->
            // [2] == Full-length
            foreach (string[] entry in maHttpResponse.aaData)
            {
                string artistName = ExtractArtistName(entry[0]);
                string country = ExtractCountry(artistName);
                string releaseName = ExtractReleaseName(entry[1]);
                string releaseType = entry[2];

                ArtistReleaseData data = new ArtistReleaseData(artistName, releaseName, releaseType, country);

                // only care about full-lengths for now
                if (!data.ReleaseType.Equals("FULL-LENGTH", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                albums.Add(data);
            }

            return albums;
        }

        private static MetalArchivesHttpResponse GetResponseAsync(Uri request)
        {
            try
            {
                var response = Client.GetStringAsync(request);
                return new JavaScriptSerializer().Deserialize<MetalArchivesHttpResponse>(response.Result);
            }
            catch (Exception e)
            {
                // sleep for a bit so that Metal Archives doesn't get mad that we're sending too many requests, then just retry
                Console.WriteLine($"Exception: {e.Message + (e.InnerException != null ? " " + e.InnerException.Message : String.Empty)}");
                Thread.Sleep(5000);
                return (_retryCount++ < _retryLimit ? GetResponseAsync(request) : null);
            }
        }

        /// <remarks>
        /// There might be two bands with the same name, but from different countries. 
        /// This is used to differentiate in those cases.
        /// </remarks>
        private static string ExtractArtistName(string dirtiedArtistName)
        {
            const string startOfTitleAttribute = " title=\"";
            const string endOfTitleAttribute = "\">";

            int startOfTitleAttributeIndex = dirtiedArtistName.IndexOf(startOfTitleAttribute);
            int endOfTitleAttributeIndex = dirtiedArtistName.IndexOf(endOfTitleAttribute);

            return dirtiedArtistName.Substring(startOfTitleAttributeIndex + startOfTitleAttribute.Length, endOfTitleAttributeIndex - startOfTitleAttributeIndex - startOfTitleAttribute.Length);
        }

        /// <remarks>
        /// The http response wraps the release name in html. This function strips the html away.
        /// </remarks>
        private static string ExtractReleaseName(string dirtiedReleaseName)
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

        private static string ExtractCountry(string artistNameWithCountry)
        {
            const string startOfCountryId = "(";
            const string endOfCountryId = ")";

            int startOfCountryIndex = artistNameWithCountry.IndexOf(startOfCountryId);
            int endOfCountryIndex = artistNameWithCountry.IndexOf(endOfCountryId);

            return artistNameWithCountry.Substring(startOfCountryIndex + startOfCountryId.Length, endOfCountryIndex - startOfCountryIndex - startOfCountryId.Length);
        }

        #endregion
    }
}
