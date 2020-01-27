using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class Library
    {
        private List<LibraryItem> _collection;
        private LibraryItemEqualityComparer _libraryItemEqualityComparer;
        private ArtistDataEqualityComparer _artistDataEqualityComparer;

        public List<LibraryItem> Collection
        {
            get { return _collection ?? (_collection = new List<LibraryItem>()); }
        }

        public List<ArtistData> Artists
        {
            get{ return Collection.Select(x => x.ArtistData).Distinct(ArtistDataEqualityComparer).ToList(); }
        }

        public List<ReleaseData> Releases
        {
            get { return Collection.Select(x => x.ReleaseData).Distinct(/*ReleaseDataEqualityComparer not necessary yet*/).ToList(); }
        }

        private LibraryItemEqualityComparer LibraryItemEqualityComparer
        {
            get { return _libraryItemEqualityComparer ?? (_libraryItemEqualityComparer = new LibraryItemEqualityComparer()); }
        }

        private ArtistDataEqualityComparer ArtistDataEqualityComparer
        {
            get { return _artistDataEqualityComparer ?? (_artistDataEqualityComparer = new ArtistDataEqualityComparer()); }
        }

        public Library(List<LibraryItem> items)
        {
            Collection.AddRange(items.Distinct(LibraryItemEqualityComparer));
        }

        public Library(DirectoryInfo fromPath)
        {
            if (!fromPath.Exists)
            {
                throw new ArgumentException("The given path must exist on disk.");
            }

            foreach (DirectoryInfo artistLayer in fromPath.GetDirectories())
            {
                foreach (DirectoryInfo albumLayer in artistLayer.GetDirectories())
                {
                    Collection.Add(new LibraryItem(artistLayer.Name, albumLayer.Name));
                }
            }
        }

        public void AddToCollection(Library l)
        {
            AddToCollection(l.Collection);
        }

        public void AddToCollection(List<LibraryItem> lli)
        {
            lli.ForEach(x => AddToCollection(x));
        }

        public void AddToCollection(LibraryItem li)
        {
            Collection.Add(li);
        }
    }
}
