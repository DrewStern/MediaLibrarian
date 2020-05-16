using System;
using System.Collections.Generic;
using MediaLibraryCompareTool.Interfaces;

namespace MediaLibraryCompareTool
{
    public abstract class BaseLibrary<TLibraryItem> 
        : ILibrary<TLibraryItem>
        where TLibraryItem : ILibraryItem
    {
        public abstract List<TLibraryItem> Collection { get; }

        #region TODO: add all of the below to BaseLibrary

        public void AddToCollection(ILibrary<TLibraryItem> l)
        {
            AddToCollection(l.Collection);
        }

        public void AddToCollection(List<TLibraryItem> lli)
        {
            lli.ForEach(x => AddToCollection(x));
        }

        public void AddToCollection(TLibraryItem li)
        {
            Collection.Add(li);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ILibrary<TLibraryItem>))
            {
                return false;
            }

            var other = (ILibrary<TLibraryItem>)obj;

            // loop over the larger collection to ensure that we can differentiate between it and any strict subsets of it
            var largerCollection = this.Collection.Count > other.Collection.Count ? this.Collection : other.Collection;
            var smallerCollection = this.Collection.Count > other.Collection.Count ? other.Collection : this.Collection;

            foreach (TLibraryItem li in largerCollection)
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
