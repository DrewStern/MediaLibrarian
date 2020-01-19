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
        private static List<ArtistReleaseData> _missingAlbums;
        private static List<string> _unrecognizedArtists;

        private static DirectoryInfo LibraryLocation { get; set; }
        private static DirectoryInfo LibraryDiffOutputLocation { get; set; }

        private static LibraryData LibraryData { get; }

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
        /// Accepts a single parameter of the form "in={PathToYourMusicCollection}", and writes two text files to that directory:
        ///     1) MissingAlbums.txt which contains any releases missing from your collection.
        ///     2) UnrecognizedArtists.txt which contains artists not found in the database.
        /// The music collection is expected to be organized in the form of "{PathToYourMusicCollection}\ArtistName\AlbumName"
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                ParseArgs(args);

                LibraryData libraryData = new LibraryData(LibraryLocation);

                foreach (string artistName in libraryData.Artists)
                {
                    var allAlbumsByArtist = MetalArchivesHttpClient.FindReleases(artistName);

                    if (!allAlbumsByArtist.Any())
                    {
                        UnrecognizedArtists.Add(artistName);
                        continue;
                    }

                    var missingAlbums = GetMissingAlbums(libraryData.EntireCollection.FindAll(x => x.ArtistName.Equals(artistName)), allAlbumsByArtist);

                    MissingAlbums.AddRange(missingAlbums);
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
                        LibraryLocation = new DirectoryInfo(argValue);
                        break;
                    case "OUT":
                    case "/OUT":
                        LibraryDiffOutputLocation = new DirectoryInfo(argValue);
                        break;
                    default:
                        break;
                }
            }
        }

        private static List<ArtistReleaseData> GetMissingAlbums(List<ArtistReleaseData> albumsInCollection, List<ArtistReleaseData> allAlbumsByArtist)
        {
            var missingAlbums = new List<ArtistReleaseData>();

            string knownAlbumName = albumsInCollection.First().ReleaseName;

            string country = allAlbumsByArtist.First(x => x.ReleaseName.Equals(knownAlbumName)).Country;

            foreach (ArtistReleaseData ard in allAlbumsByArtist)
            {
                if (!albumsInCollection.Any(x => 
                    x.ArtistName.Equals(ard.ArtistName, StringComparison.InvariantCultureIgnoreCase) && 
                    x.ReleaseName.Equals(ard.ReleaseName, StringComparison.InvariantCultureIgnoreCase)) &&
                    country.Equals(ard.Country, StringComparison.InvariantCultureIgnoreCase))
                {
                    missingAlbums.Add(ard);
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

            foreach (DirectoryInfo artistLayer in LibraryLocation.GetDirectories())
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
            string missingAlbumsSaveLocation = Path.Combine(LibraryLocation.FullName, "MissingAlbums.txt");
            File.WriteAllLines(missingAlbumsSaveLocation, MissingAlbums.Select(x => x.ToString()).ToArray());

            string unrecognizedArtistsSaveLocation = Path.Combine(LibraryLocation.FullName, "UnrecognizedArtists.txt");
            File.WriteAllLines(unrecognizedArtistsSaveLocation, UnrecognizedArtists.ToArray());
        }
    }
}