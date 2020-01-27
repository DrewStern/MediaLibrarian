using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

namespace MetalArchivesLibraryDiffTool
{
    /// <summary>
    /// The purpose of this application is to compare the content of a given library of metal music against a database of known metal releases.
    /// Primarily to help me find good things I might be missing in my collection. :)
    /// </summary>
    [ExcludeFromCodeCoverage]
    class Program
    {
        private static LibraryDiffService _libraryDiffService;
        private static MetalArchivesHttpClient _metalArchivesHttpClient;
        private static MetalArchivesHttpService _metalArchivesHttpService;
        private static MetalArchivesHttpResponseParser _metalArchivesHttpResponseParser;

        private static DirectoryInfo LibraryLocation { get; set; }

        private static DirectoryInfo LibraryDiffOutputLocation { get; set; }

        private static Library MyLibraryData { get; set; }

        private static Library TheirLibraryData { get; set; }

        private static LibraryDiffService LibraryDiffService
        {
            get { return _libraryDiffService ?? (_libraryDiffService = new LibraryDiffService()); }
        }

        private static MetalArchivesHttpService MetalArchivesHttpService
        {
            get { return _metalArchivesHttpService ?? (_metalArchivesHttpService = new MetalArchivesHttpService()); }
        }

        private static MetalArchivesHttpResponseParser MetalArchivesHttpResponseParser
        {
            get { return _metalArchivesHttpResponseParser ?? (_metalArchivesHttpResponseParser = new MetalArchivesHttpResponseParser()); }
        }

        private static MetalArchivesHttpClient MetalArchivesHttpClient
        {
            get { return _metalArchivesHttpClient ?? (_metalArchivesHttpClient = new MetalArchivesHttpClient(MetalArchivesHttpService, MetalArchivesHttpResponseParser)); }
        }

        /// <summary>
        /// Accepts two parameters:
        ///     "in={PathToYourMusicCollection}", 
        ///     "out={PathToLibraryComparisonResult}"
        /// then writes a text file to the location specified by the out param.
        /// 
        /// The music collection is expected to be organized in the form of "{PathToYourMusicCollection}\ArtistName\AlbumName"
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                ParseArgs(args);

                MyLibraryData = new Library(LibraryLocation);

                Console.WriteLine($"Discovered {MyLibraryData.Collection.Count} items on disk");

                TheirLibraryData = new Library();

                foreach (ArtistData artist in MyLibraryData.Artists)
                {
                    TheirLibraryData.AddToCollection(MetalArchivesHttpClient.FindByArtist(artist.ArtistName));
                    Console.WriteLine($"Added {artist.ArtistName} to library");
                    Thread.Sleep(3000);
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

        private static void WriteResults()
        { 
            string[] text = new string[4];
            text[0] = "----- Artists with no matching results -----";
            text[1] = String.Join(Environment.NewLine, LibraryDiffService.GetArtistDiffs(MyLibraryData, TheirLibraryData));
            text[2] = "----- Releases missing from your collection -----";
            text[3] = String.Join(Environment.NewLine, LibraryDiffService.GetReleaseDiffs(MyLibraryData, TheirLibraryData));

            File.WriteAllLines(LibraryDiffOutputLocation.FullName, text);
        }
    }
}