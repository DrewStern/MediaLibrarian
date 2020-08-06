namespace MediaLibrarian
{
    public class MetalArchivesRequest
    {
        public string ArtistName
        {
            // facilitates bands which have spaces in their name
            get { return "\"" + ArtistData.ArtistName + "\""; }
        }

        public ArtistData ArtistData { get; }

        public MetalArchivesRequest(ArtistData ad)
        {
            ArtistData = ad;
        }
    }
}
