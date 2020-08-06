using System;
using System.Collections.Generic;

namespace MediaLibrarian
{
    public class ReleaseDataEqualityComparer : IEqualityComparer<ReleaseData>
    {
        public bool Equals(ReleaseData x, ReleaseData y)
        {
            return
                x.ReleaseName.Equals(y.ReleaseName, StringComparison.InvariantCultureIgnoreCase) &&
                x.ReleaseType.Equals(y.ReleaseType, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(ReleaseData rd)
        {
            return base.GetHashCode();
        }
    }
}
