using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibrary
{
    /// <summary>
    /// The purpose of this application is to compare the content of a given library of metal music against a database of known metal releases.
    /// Primarily to help me find good things I might be missing in my collection. :)
    /// </summary>
    class Program
    {
        private static DirectoryInfo _libraryLocation;
        private static List<ArtistReleaseData> _missingAlbums;
        private static List<string> _unrecognizedArtists;

        private static List<ArtistReleaseData> MissingAlbums
        {
            get { return _missingAlbums ?? (_missingAlbums = new List<ArtistReleaseData>()); }
            set { _missingAlbums = value; }
        }

        private static List<string> UnrecognizedArtists
        {
            get { return _unrecognizedArtists ?? (_unrecognizedArtists = new List<string>()); }
            set { _unrecognizedArtists = value; }
        }

        /// <summary>
        /// Accepts a single parameter of the form "in=C:\YourMusicCollectionHere", and produces two text files:
        ///     1) Missing.txt which contains any releases missing from your collection.
        ///     2) Unrecognized.txt which contains artists/releases not found in the database.
        /// The music collection is expected to be organized in the form of "C:\YourMusicCollectionHere\ArtistName\AlbumName"
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                foreach (string arg in args)
                {
                    var argKey = arg.Split('=')[0];
                    var argValue = arg.Split('=')[1];

                    switch (argKey.ToUpperInvariant())
                    {
                        case "IN":
                        case "/IN":
                            _libraryLocation = new DirectoryInfo(argValue);
                            break;
                        default:
                            break;
                    }
                }

                var alreadyInCollection = FindKnownArtistReleaseData();

                var artistsInCollection = alreadyInCollection.Select(x => x.ArtistName).Distinct();

                foreach (string artistName in artistsInCollection)
                {
                    var allAlbumsReleasedByArtist = MetalArchivesHttpClient.FindAlbums(artistName);

                    if (!allAlbumsReleasedByArtist.Any())
                    {
                        UnrecognizedArtists.Add(artistName);
                        continue;
                    }

                    MissingAlbums.AddRange(GetMissingAlbums(alreadyInCollection, allAlbumsReleasedByArtist, artistName));
                }
            }
            catch (Exception exc)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static List<ArtistReleaseData> GetMissingAlbums(List<ArtistReleaseData> inCollection, List<string> allExisting, string artistName)
        {
            var missingAlbums = new List<ArtistReleaseData>();

            foreach (string album in allExisting)
            {
                if (!inCollection.Any(x => x.ArtistName.Equals(artistName) && x.ReleaseName.Equals(album)))
                {
                    missingAlbums.Add(new ArtistReleaseData(artistName, album));
                }
            }

            return missingAlbums;
        }

        /// <summary>
        /// Searches the specified directory for known artist release data.
        /// </summary>
        /// <returns></returns>
        private static List<ArtistReleaseData> FindKnownArtistReleaseData()
        {
            var knownReleases = new List<ArtistReleaseData>();

            foreach (DirectoryInfo artistLayer in _libraryLocation.GetDirectories())
            {
                foreach (DirectoryInfo albumLayer in artistLayer.GetDirectories())
                {
                    knownReleases.Add(new ArtistReleaseData(artistLayer.Name, albumLayer.Name));
                }
            }

            return knownReleases;
        }
    }
}