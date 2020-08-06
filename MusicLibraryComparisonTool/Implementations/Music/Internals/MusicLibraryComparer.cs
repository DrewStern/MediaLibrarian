namespace MediaLibrarian
{
    public class MusicLibraryComparer : BaseLibraryComparer<MusicLibraryItem, MusicLibrary, MusicLibraryCompareResult>
    {
        public override MusicLibraryCompareResult Compare(MusicLibrary left, MusicLibrary right)
        {
            return new MusicLibraryCompareResult(
                left, right,
                GetSum(left, right),
                GetIntersection(left, right),
                GetLeftOutersection(left, right),
                GetRightOutersection(left, right),
                GetFullOutersection(left, right));
        }
    }
}
