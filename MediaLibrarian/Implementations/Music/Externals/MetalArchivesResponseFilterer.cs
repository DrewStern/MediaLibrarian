namespace MediaLibrarian
{
    public class MetalArchivesResponseFilterer
    {
        public MetalArchivesResponseFilterer()
        {
            // intentionally empty
        }

        public MusicLibrary Filter(MusicLibrary original, MetalArchivesRequest request)
        {
            // looks like without the FindAll below, MA is returning any artists whose names even slightly match our request
            // ensuring that the discovered artistname starts the same as the desired artistname helps prevent this
            return new MusicLibrary(original.Collection
                .FindAll(x => x.ArtistData.ArtistName.StartsWith(request.ArtistName))
                .FindAll(x => x.ReleaseData.IsFullLength));
        }
    }
}

