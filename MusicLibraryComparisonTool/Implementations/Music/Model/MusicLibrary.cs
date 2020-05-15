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
            get { return Collection.Select(x => x.ReleaseData).Distinct().ToList(); }
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

        #region TODO: add all of the below to BaseLibrary

        public void AddToCollection(MusicLibrary l)
        {
            AddToCollection(l.Collection);
        }

        public void AddToCollection(List<MusicLibraryItem> lli)
        {
            lli.ForEach(x => AddToCollection(x));
        }

        public void AddToCollection(MusicLibraryItem li)
        {
            Collection.Add(li);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MusicLibrary))
            {
                return false;
            }

            MusicLibrary other = (MusicLibrary)obj;

            // loop over the larger collection to ensure that we can differentiate between it and any strict subsets of it
            var largerCollection = this.Collection.Count > other.Collection.Count ? this.Collection : other.Collection;
            var smallerCollection = this.Collection.Count > other.Collection.Count ? other.Collection : this.Collection;

            foreach (MusicLibraryItem li in largerCollection)
            {
                if (!smallerCollection.Contains(li))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine, Collection);
        }
    }

    #endregion
}
