using System;

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

            return new MusicLibrary(_parser.Parse(response).Collection
                // looks like without the FindAll below, MA is returning any artists whose names even slightly match our request
                // ensuring that the discovered artistname starts the same as the desired artistname helps prevent this
                .FindAll(x => x.ArtistData.ArtistName.StartsWith(artistName))
                .FindAll(x => x.ReleaseData.IsFullLength));
        }

        // TODO: may want to implement FindByArtistAndCountry, FindBetweenReleaseDates, FindNewerThan, FindOlderThan

        #endregion
    }
}
