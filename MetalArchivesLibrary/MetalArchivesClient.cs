using System;
using System.Linq;

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

            var request = new MetalArchivesRequest(new ArtistData(artistName));

            var response = Service.Submit(request);

            var parsedResponse = Parser.Parse(response);

            // TODO: I don't like doing this filtration here.
            return new Library(parsedResponse.Collection.Where(x => x.ArtistData.ArtistName.StartsWith(artistName)).ToList());
        }

        // TODO: may want to implement FindByArtistAndCountry or FindBetweenReleaseDates

        #endregion
    }
}
