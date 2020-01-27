﻿using System;
using System.Net.Http;
using System.Threading;
using System.Web.Script.Serialization;

namespace MetalArchivesLibraryDiffTool
{
    public class MetalArchivesHttpService
    {
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

        private HttpClient _metalArchivesService { get; }

        public MetalArchivesHttpService()
        {
            _metalArchivesService = new HttpClient { BaseAddress = new Uri(_queryEndpoint) };
        }

        public MetalArchivesHttpResponse Submit(MetalArchivesHttpRequest request)
        {
            _retryCount = 0;

            if (request.Equals(null))
            {
                throw new ArgumentNullException($"{nameof(request)} may not be null");
            }

            // only need to expose http GET method, others are irrelevant
            return GetResponseAsync(new Uri(string.Format(_queryEndpoint, request.ArtistName)));
        }

        private MetalArchivesHttpResponse GetResponseAsync(Uri request)
        {
            try
            {
                var response = _metalArchivesService.GetStringAsync(request);
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
    }
}