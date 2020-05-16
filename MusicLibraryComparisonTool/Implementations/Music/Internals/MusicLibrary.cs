using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaLibraryCompareTool
{
    public class MusicLibrary : BaseLibrary<MusicLibraryItem>
    {
        #region Fields

        private List<MusicLibraryItem> _collection;
        private MusicLibraryItemEqualityComparer _libraryItemEqualityComparer;
        private ArtistDataEqualityComparer _artistDataEqualityComparer;

        #endregion

        #region Properties

        public override List<MusicLibraryItem> Collection
        {
            get { return _collection ?? (_collection = new List<MusicLibraryItem>()); }
        }

        public List<ArtistData> Artists
        {
            get { return Collection.Select(x => x.ArtistData).Distinct(ArtistDataEqualityComparer).ToList(); }
        }

        public List<ReleaseData> Releases
        {
            get { return Collection.Select(x => x.ReleaseData).Distinct(/*TODO: create ReleaseDataEqualityComparer? */).ToList(); }
        }

        private MusicLibraryItemEqualityComparer LibraryItemEqualityComparer
        {
            get { return _libraryItemEqualityComparer ?? (_libraryItemEqualityComparer = new MusicLibraryItemEqualityComparer()); }
        }

        private ArtistDataEqualityComparer ArtistDataEqualityComparer
        {
            get { return _artistDataEqualityComparer ?? (_artistDataEqualityComparer = new ArtistDataEqualityComparer()); }
        }

        #endregion

        #region Constructor(s)

        public MusicLibrary()
        {
            // intentionally empty in order to support inheritance hierarchy
        }

        public MusicLibrary(List<MusicLibraryItem> items)
        {
            Collection.AddRange(items.Distinct(LibraryItemEqualityComparer));
        }

        public MusicLibrary(DirectoryInfo fromPath)
        {
            if (!fromPath.Exists)
            {
                throw new ArgumentException("The given path must exist on disk.");
            }

            foreach (DirectoryInfo artistLayer in fromPath.GetDirectories())
            {
                foreach (DirectoryInfo albumLayer in artistLayer.GetDirectories())
                {
                    Collection.Add(new MusicLibraryItem(artistLayer.Name, albumLayer.Name));
                }
            }
        }

        #endregion
    }
}
