namespace MediaLibraryCompareTool
{
    public partial class MetalArchivesRequestBuilder
    {
        private string _artistName;
        private bool _findArtistByExactName;
        private bool _findOnlyFullLengths;

        public MetalArchivesRequestBuilder()
        {
        }

        public MetalArchivesRequest Build()
        {
            return new MetalArchivesRequest(new ArtistData(_artistName));
        }

        public MetalArchivesRequestBuilder FindByArtist(string artistName, bool requireExactMatch = true)
        {
            _artistName = artistName;
            _findArtistByExactName = requireExactMatch;
            return this;
        }

        public MetalArchivesRequestBuilder FindOnlyFullLengths(bool findOnlyFullLengths)
        {
            _findOnlyFullLengths = findOnlyFullLengths;
            return this;
        }
    }
}
