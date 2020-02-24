using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

namespace MusicLibraryCompareTool
{
    /// <summary>
    /// The purpose of this application is to compare the content of a given library of metal music against a database of known metal releases.
    /// Primarily to help me find good things I might be missing in my collection. :)
    /// 
    /// TODO: provide search results from youtube for missing albums from my list?
    /// TODO: or bandcamp
    /// </summary>
    [ExcludeFromCodeCoverage]
    class Program
    {
        private static LibraryComparisonService _libraryDiffService;
        private static MetalArchivesClient _metalArchivesClient;
        private static MetalArchivesService _metalArchivesService;
        private static MetalArchivesResponseParser _metalArchivesResponseParser;

        private static DirectoryInfo LibraryLocation { get; set; }

        private static DirectoryInfo LibraryDiffOutputLocation { get; set; }

        private static Library MyLibraryData { get; set; }

        private static Library TheirLibraryData { get; set; }

        private static LibraryComparisonService LibraryDiffService
        {
            get { return _libraryDiffService ?? (_libraryDiffService = new LibraryComparisonService()); }
        }

        private static MetalArchivesService MetalArchivesService
        {
            get { return _metalArchivesService ?? (_metalArchivesService = new MetalArchivesService()); }
        }

        private static MetalArchivesResponseParser MetalArchivesResponseParser
        {
            get { return _metalArchivesResponseParser ?? (_metalArchivesResponseParser = new MetalArchivesResponseParser()); }
        }

        private static MetalArchivesClient MetalArchivesClient
        {
            get { return _metalArchivesClient ?? (_metalArchivesClient = new MetalArchivesClient(MetalArchivesService, MetalArchivesResponseParser)); }
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
                TheirLibraryData = new Library(new List<LibraryItem>());

                Console.WriteLine($"Discovered {MyLibraryData.Collection.Count} items on disk");

                foreach (ArtistData artist in MyLibraryData.Artists)
                {
                    TheirLibraryData.AddToCollection(MetalArchivesClient.FindByArtist(artist.ArtistName));
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
            string[] text = new string[1];
            text[0] = String.Join(Environment.NewLine, LibraryDiffService.GetReleaseDiffs(MyLibraryData, TheirLibraryData));

            File.WriteAllLines(LibraryDiffOutputLocation.FullName, text);
        }
    }
}