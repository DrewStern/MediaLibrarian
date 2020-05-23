using System;

namespace MediaLibraryCompareTool
{
    public class MetalArchivesServiceClient
    {
        private MetalArchivesServiceProvider _service { get; }

        private MetalArchivesResponseParser _parser { get; }

        public MetalArchivesServiceClient(MetalArchivesServiceProvider service, MetalArchivesResponseParser parser)
        {
            _service = service;
            _parser = parser;
        }

        public MusicLibrary Submit(MetalArchivesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} may not be null");
            }

            var response = _service.Process(request);

            return new MusicLibrary(_parser.Parse(response).Collection
                // looks like without the FindAll below, MA is returning any artists whose names even slightly match our request
                // ensuring that the discovered artistname starts the same as the desired artistname helps prevent this
                .FindAll(x => x.ArtistData.ArtistName.StartsWith(request.ArtistName))
                .FindAll(x => x.ReleaseData.IsFullLength));
        }
    }
}
