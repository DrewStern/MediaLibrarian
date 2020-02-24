using System;
using System.Linq;

namespace MusicLibraryCompareTool
{
    public class MetalArchivesClient
    {
        private MetalArchivesService _service { get; }

        private MetalArchivesResponseParser _parser { get; }

        #region Constructors

        public MetalArchivesClient(MetalArchivesService service, MetalArchivesResponseParser parser)
        {
            _service = service;
            _parser = parser;
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

            var response = _service.Submit(request);

            var parsedResponse = _parser.Parse(response);

            // TODO: I don't like doing this filtration here.
            return new Library(parsedResponse.Collection.Where(x => x.ArtistData.ArtistName.StartsWith(artistName)).ToList());
        }

        // TODO: may want to implement FindByArtistAndCountry, FindBetweenReleaseDates, FindNewerThan, FindOlderThan

        #endregion
    }
}
