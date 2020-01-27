using System;

namespace MetalArchivesLibraryDiffTool
{
    public class MetalArchivesHttpClient
    {
        private MetalArchivesHttpService Service { get; }

        private MetalArchivesHttpResponseParser Parser { get; }

        #region Constructors

        public MetalArchivesHttpClient(MetalArchivesHttpService service, MetalArchivesHttpResponseParser parser)
        {
            Service = service;
            Parser = parser;
        }

        #endregion Constructors

        #region Methods

        public Library FindByArtist(string artistName)
        {
            if (String.IsNullOrWhiteSpace(artistName))
            {
                throw new ArgumentException($"{nameof(artistName)} may not be null or empty");
            }

            return Parser.Parse(Service.Submit(new MetalArchivesHttpRequest(new ArtistData(artistName))));
        }

        // TODO: may want to implement FindByArtistAndCountry

        #endregion
    }
}
