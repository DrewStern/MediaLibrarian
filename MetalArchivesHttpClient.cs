using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Script.Serialization;

namespace MetalArchivesLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class MetalArchivesHttpClient
    {
        #region Fields

        /// <summary>
        /// Represents the most generic possible query to the Metal Archives database. 
        /// Currently we are only interested in querying based on artists, but the other parameters can be specified at a later time.
        /// </summary>
        /*
        private static string _albumQuery =
            "https://www.metal-archives.com/search/ajax-advanced/searching/albums?" +
            "artistName={0}&releaseTitle=&releaseYearFrom=&releaseMonthFrom=&releaseYearTo=&releaseMonthTo=&country=&location=&" +
            "releaseLabelName=&releaseCatalogNumber=&releaseIdentifiers=&releaseRecordingInfo=&releaseDescription=&releaseNotes=&genre=#albums";
        */

        private static string _albumQuery =
            "https://www.metal-archives.com/search/advanced/searching/albums?" +
            "bandName={0}&releaseTitle=&releaseYearFrom=&releaseMonthFrom=&releaseYearTo=&releaseMonthTo=&country=&location=&" +
            "releaseLabelName=&releaseCatalogNumber=&releaseIdentifiers=&releaseRecordingInfo=&releaseDescription=&releaseNotes=&genre=#albums";

        private static HttpClient _client;

        #endregion

        #region Properties

        private static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.BaseAddress = new Uri(_albumQuery);
                }

                return _client;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bandName"></param>
        /// <returns></returns>
        public static List<string> FindAlbums(string bandName)
        {
            if (String.IsNullOrWhiteSpace(bandName))
            {
                throw new ArgumentException($"{nameof(bandName)} may not be null or empty");
            }

            // we use these to determine the begin/end of the html wrapping the album name
            const string endOfOpenHtmlTag = "\">";
            const string startOfCloseHtmlTag = "</a>";

            // facilitates bands which have spaces in their name
            string cleanedBandName = "\"" + bandName + "\"";

            var albums = new List<string>();

            var maHttpResponse = GetResponseAsync(new Uri(string.Format(_albumQuery, cleanedBandName)));

            if (maHttpResponse == null)
            {
                return albums;
            }

            // Each entry has three components - the first represents the band name, the second the album name, and third the release type. Example:
            // [0] == <a href="https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265" title="!T.O.O.H.! (CZ)">!T.O.O.H.!</a>
            // [1] == <a href="https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622">Democratic Solution</a> <!-- 7.792132 -->
            // [2] == Full-length
            foreach (string[] entry in maHttpResponse.aaData)
            {
                ArtistReleaseData data = new ArtistReleaseData(entry[0], entry[1], entry[2]);

                // gather these indices so we can use substring below
                int endOfOpenHtmlIndex = data.ReleaseName.IndexOf(endOfOpenHtmlTag);
                int startOfCloseHtmlIndex = data.ReleaseName.IndexOf(startOfCloseHtmlTag);

                // take the substring in between the html wrapping
                string albumName = data.ReleaseName.Substring(endOfOpenHtmlIndex + endOfOpenHtmlTag.Length, startOfCloseHtmlIndex - endOfOpenHtmlIndex - endOfOpenHtmlTag.Length);

                // only care about full-lengths for now
                if (!data.ReleaseType.Equals("FULL-LENGTH", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                albums.Add(albumName);
            }

            // returns the albums in alphabetical order
            return albums;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                Thread.Sleep(1000);
                return GetResponseAsync(request);
            }
        }

        #endregion
    }
}
