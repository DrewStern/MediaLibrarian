namespace MetalArchivesLibraryDiffTool
{
    public class MetalArchivesHttpRequest
    {
        private string _artistName;

        public string ArtistName
        {
            // facilitates bands which have spaces in their name
            get { return "\"" + _artistName + "\""; }
        }

        public ArtistData ArtistData { get; }

        public ReleaseData ReleaseData { get; }

        public MetalArchivesHttpRequest(ArtistData ad)
            : this(ad, null)
        {
        }

        public MetalArchivesHttpRequest(ArtistData ad, ReleaseData rd)
        {
            ArtistData = ad;
            ReleaseData = rd;
        }

        public MetalArchivesHttpRequest(string artistName)
        {
            _artistName = artistName;
        }
    }
}
