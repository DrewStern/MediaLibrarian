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
        private static List<ArtistReleaseData> _entireCollection;
        private static List<ArtistReleaseData> _missingAlbums;
        private static List<string> _unrecognizedArtists;

        private static List<ArtistReleaseData> EntireCollection
        {
            get { return _entireCollection ?? (_entireCollection = new List<ArtistReleaseData>()); }
            set { _entireCollection = value; }
        }

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
        /// Accepts a single parameter of the form "in=C:\YourMusicCollectionHere", and writes two text files to that directory:
        ///     1) MissingAlbums.txt which contains any releases missing from your collection.
        ///     2) UnrecognizedArtists.txt which contains artists not found in the database.
        /// The music collection is expected to be organized in the form of "C:\YourMusicCollectionHere\ArtistName\AlbumName"
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                ParseArgs(args);

                EntireCollection = FindKnownArtistReleaseData();

                var artistsInCollection = EntireCollection.OrderBy(x => x.ArtistName).Select(x => x.ArtistName).Distinct();

                foreach (string artistName in artistsInCollection)
                {
                    var allAlbumsReleasedByArtist = MetalArchivesHttpClient.FindAlbums(artistName);

                    if (!allAlbumsReleasedByArtist.Any())
                    {
                        UnrecognizedArtists.Add(artistName);
                        continue;
                    }

                    MissingAlbums.AddRange(GetMissingAlbums(artistName, allAlbumsReleasedByArtist));
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Exception: {exc.Message}");
            }
            finally
            {
                WriteResults();
            }
        }

        private static void ParseArgs(string[] args)
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static List<ArtistReleaseData> GetMissingAlbums(string artistName, List<string> allAlbumsByArtist)
        {
            var missingAlbums = new List<ArtistReleaseData>();

            foreach (string album in allAlbumsByArtist)
            {
                if (!EntireCollection.Any(x => x.ArtistName.Equals(artistName) && x.ReleaseName.Equals(album, StringComparison.InvariantCultureIgnoreCase)))
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

        private static void WriteResults()
        {
            string missingAlbumsSaveLocation = Path.Combine(_libraryLocation.FullName, "MissingAlbums.txt");
            File.WriteAllLines(missingAlbumsSaveLocation, MissingAlbums.Select(ard => ard.ArtistName + " - " + ard.ReleaseName).ToArray());

            string unrecognizedArtistsSaveLocation = Path.Combine(_libraryLocation.FullName, "UnrecognizedArtists.txt");
            File.WriteAllLines(unrecognizedArtistsSaveLocation, UnrecognizedArtists.ToArray());
        }
    }
}