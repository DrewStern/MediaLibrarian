namespace MediaLibrarian
{
    public partial class MetalArchivesRequestBuilder
    {
        //private List<SearchCriteria<>>
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

        public MetalArchivesRequestBuilder AddArtistCriteria(string name, string country = "")
        {
            // TODO: _artistCriteria = new SearchCriteria<ArtistData>();
            return this;
        }

        public MetalArchivesRequestBuilder AddReleaseCriteria()
        {
            // TODO: _artistCriteria = new SearchCriteria<ReleaseData>();
            return this;
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

    public class SearchCriteria<T> where T : ArtistData//, ReleaseData
    {


        public SearchCriteria(string name, string country = "")
        {
        }
    }
}
