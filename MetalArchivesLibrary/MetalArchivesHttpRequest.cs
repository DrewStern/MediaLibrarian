namespace MetalArchivesLibraryDiffTool
{
    public class MetalArchivesHttpRequest
    {
        public string ArtistName
        {
            // facilitates bands which have spaces in their name
            get { return "\"" + ArtistData.ArtistName + "\""; }
        }

        public ArtistData ArtistData { get; }

        public MetalArchivesHttpRequest(ArtistData ad)
        {
            ArtistData = ad;
        }
    }
}
