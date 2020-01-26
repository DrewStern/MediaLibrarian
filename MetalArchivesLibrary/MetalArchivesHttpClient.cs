using System;
using System.Net.Http;
using System.Threading;
using System.Web.Script.Serialization;

namespace MetalArchivesLibraryDiffTool
{
    public partial class MetalArchivesHttpClient
    {
        #region Fields

        private static int _retryLimit = 5;
        private static int _retryCount = 0;

        /// <summary>
        /// Represents the most generic possible query to the Metal Archives database. 
        /// Currently we are only interested in querying based on artists, but the other parameters can be specified at a later time.
        /// </summary>
        private static string _queryEndpoint =
            "https://www.metal-archives.com/search/ajax-advanced/searching/albums?" +
            "bandName={0}&releaseTitle=&releaseYearFrom=&releaseMonthFrom=&releaseYearTo=&releaseMonthTo=&country=&location=&" +
            "releaseLabelName=&releaseCatalogNumber=&releaseIdentifiers=&releaseRecordingInfo=&releaseDescription=&releaseNotes=&genre=#albums";

        #endregion

        #region Properties

        private static HttpClient Client { get; }
        private static MetalArchivesHttpResponseParser Parser { get; }

        #endregion

        #region Constructors

        static MetalArchivesHttpClient()
        {
            Client = new HttpClient { BaseAddress = new Uri(_queryEndpoint) };
            Parser = new MetalArchivesHttpResponseParser();
        }

        #endregion Constructors

        #region Methods

        public Library Find(MetalArchivesHttpRequest request)
        {
            _retryCount = 0;

            if (request.Equals(null))
            {
                throw new ArgumentNullException($"{nameof(request)} may not be null");
            }

            var maHttpResponse = GetResponseAsync(new Uri(string.Format(_queryEndpoint, request.ArtistName)));

            return Parser.Parse(maHttpResponse);
        }

        public Library FindByArtist(string artistName)
        {
            _retryCount = 0;

            if (String.IsNullOrWhiteSpace(artistName))
            {
                throw new ArgumentException($"{nameof(artistName)} may not be null or empty");
            }

            // facilitates bands which have spaces in their name
            string cleanedBandName = "\"" + artistName + "\"";

            var maHttpResponse = GetResponseAsync(new Uri(string.Format(_queryEndpoint, cleanedBandName)));

            return Parser.Parse(maHttpResponse);
        }

        private MetalArchivesHttpResponse GetResponseAsync(Uri request)
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

        #endregion
    }
}
