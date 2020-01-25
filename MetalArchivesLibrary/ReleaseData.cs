using System;

namespace MetalArchivesLibraryDiffTool
{
    public class ReleaseData
    {
        public string ReleaseName { get; }

        public string ReleaseType { get; }

        public bool IsFullLength
        {
            get { return ReleaseType.ToUpperInvariant().Equals("FULL-LENGTH"); }
        }

        public ReleaseData(string releaseName, string releaseType)
        {
            ReleaseName = releaseName;
            ReleaseType = releaseType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReleaseData))
            {
                return false;
            }

            ReleaseData other = (ReleaseData)obj;

            return
                this.ReleaseName.Equals(other.ReleaseName, StringComparison.InvariantCultureIgnoreCase) &&
                this.ReleaseType.Equals(other.ReleaseType, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
