using System;

namespace MediaLibrarian
{
    public class ReleaseData
    {
        public string ReleaseName { get; }

        public string ReleaseType { get; }

        public bool IsFullLength
        {
            get { return "FULL-LENGTH".Equals(ReleaseType.ToUpperInvariant()); }
        }

        public ReleaseData(string releaseName)
            : this(releaseName, "Full-Length")
        {
            // intentionally empty
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

        public override string ToString()
        {
            string optionalReleaseType = IsFullLength ? string.Empty : $"({ReleaseType})";
            return $"{ReleaseName} {optionalReleaseType}";
        }
    }
}
