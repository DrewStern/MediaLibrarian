using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalArchivesLibraryDiffTool
{
    public class MetalArchivesHttpRequest
    {
        private string _artistName;

        public string ArtistName
        {
            get { return _artistName; }
            private set
            {
                // facilitates bands which have spaces in their name
                _artistName = "\"" + value + "\"";
            }
        }

        public MetalArchivesHttpRequest(string artistName)
        {
            ArtistName = artistName;
        }
    }
}
