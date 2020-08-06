using System;

namespace MediaLibrarian
{
    public class MetalArchivesServiceClient
    {
        private MetalArchivesServiceProvider _service { get; }

        private MetalArchivesResponseParser _parser { get; }

        private MetalArchivesResponseFilterer _filterer { get; }

        public MetalArchivesServiceClient(MetalArchivesServiceProvider service, MetalArchivesResponseParser parser, MetalArchivesResponseFilterer filterer)
        {
            _service = service;
            _parser = parser;
            _filterer = filterer;
        }

        public MusicLibrary Submit(MetalArchivesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} may not be null");
            }

            var response = _service.Process(request);

            var musicLibrary = new MusicLibrary(_parser.Parse(response).Collection);

            return _filterer.Filter(musicLibrary, request);
        }
    }
}
