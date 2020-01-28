using System;

namespace MetalArchivesLibraryDiffTool
{
    public class MetalArchivesClient
    {
        private MetalArchivesService Service { get; }

        private MetalArchivesResponseParser Parser { get; }

        #region Constructors

        public MetalArchivesClient(MetalArchivesService service, MetalArchivesResponseParser parser)
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

            return Parser.Parse(Service.Submit(new MetalArchivesRequest(new ArtistData(artistName))));
        }

        // TODO: may want to implement FindByArtistAndCountry

        #endregion
    }
}
