namespace MediaLibraryCompareTool
{
    public class MusicLibraryCompareResult : BaseLibraryCompareResult<MusicLibraryItem, MusicLibrary>
    {
        public override MusicLibrary Left { get; }

        public override MusicLibrary Right { get; }

        public override MusicLibrary Sum { get; }

        public override MusicLibrary Intersection { get; }

        public override MusicLibrary LeftOutersection { get; }

        public override MusicLibrary RightOutersection { get; }

        public override MusicLibrary FullOutersection { get; }

        public MusicLibraryCompareResult(MusicLibrary left, MusicLibrary right, MusicLibrary sum, MusicLibrary intersection,
            MusicLibrary leftOutersection, MusicLibrary rightOutersection, MusicLibrary fullOutersection)
        {
            Left = left;
            Right = right;
            Sum = sum;
            Intersection = intersection;
            LeftOutersection = leftOutersection;
            RightOutersection = rightOutersection;
            FullOutersection = fullOutersection;
        }
    }
}
