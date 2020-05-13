using System;
using System.Linq;

namespace MediaLibraryCompareTool
{
    public class MetalArchivesServiceClient
    {
        private MetalArchivesServiceProvider _service { get; }

        private MetalArchivesResponseParser _parser { get; }

        #region Constructors

        public MetalArchivesServiceClient(MetalArchivesServiceProvider service, MetalArchivesResponseParser parser)
        {
            _service = service;
            _parser = parser;
        }

        #endregion Constructors

        #region Methods

        public MusicLibrary FindByArtist(string artistName)
        {
            if (String.IsNullOrWhiteSpace(artistName))
            {
                throw new ArgumentException($"{nameof(artistName)} may not be null or empty");
            }

            var request = new MetalArchivesRequest(new ArtistData(artistName));

            var response = _service.Submit(request);

            var parsedResponse = _parser.Parse(response);

            // TODO: I don't like doing this filtration here.
            // TODO: Deprecate these terrible variable names in favor of a singular LINQ filtration chain.
            var parsedResponseFilteredByDesiredArtist = parsedResponse.Collection.Where(x => x.ArtistData.ArtistName.StartsWith(artistName)).ToList();

            var parsedResponseFilteredByDesiredArtistFilterToOnlyFullLengths = parsedResponseFilteredByDesiredArtist.FindAll(x => x.ReleaseData.IsFullLength);

            return new MusicLibrary(parsedResponseFilteredByDesiredArtistFilterToOnlyFullLengths);
        }

        // TODO: may want to implement FindByArtistAndCountry, FindBetweenReleaseDates, FindNewerThan, FindOlderThan

        #endregion
    }
}
