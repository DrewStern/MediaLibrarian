using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

namespace MediaLibraryCompareTool
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
        private static MusicLibraryCompareService _musicLibraryCompareService;
        private static MetalArchivesServiceClient _metalArchivesClient;
        private static MetalArchivesServiceProvider _metalArchivesService;
        private static MetalArchivesResponseParser _metalArchivesResponseParser;

        private static DirectoryInfo LibraryLocation { get; set; }

        private static DirectoryInfo LibraryDiffOutputLocation { get; set; }

        private static MusicLibrary MyMusicLibraryData { get; set; }

        private static MusicLibrary TheirMusicLibraryData { get; set; }

        private static MusicLibraryCompareService MusicLibraryCompareService
        {
            get { return _musicLibraryCompareService ?? (_musicLibraryCompareService = new MusicLibraryCompareService()); }
        }

        private static MetalArchivesServiceProvider MetalArchivesService
        {
            get { return _metalArchivesService ?? (_metalArchivesService = new MetalArchivesServiceProvider()); }
        }

        private static MetalArchivesResponseParser MetalArchivesResponseParser
        {
            get { return _metalArchivesResponseParser ?? (_metalArchivesResponseParser = new MetalArchivesResponseParser()); }
        }

        private static MetalArchivesServiceClient MetalArchivesClient
        {
            get { return _metalArchivesClient ?? (_metalArchivesClient = new MetalArchivesServiceClient(MetalArchivesService, MetalArchivesResponseParser)); }
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

                MyMusicLibraryData = new MusicLibrary(LibraryLocation);
                TheirMusicLibraryData = new MusicLibrary(new List<MusicLibraryItem>());

                Console.WriteLine($"Discovered {MyMusicLibraryData.Collection.Count} items on disk");

                foreach (ArtistData artist in MyMusicLibraryData.Artists)
                {
                    TheirMusicLibraryData.AddToCollection(MetalArchivesClient.FindByArtist(artist.ArtistName));
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
            text[0] = String.Join(Environment.NewLine, MusicLibraryCompareService.GetReleaseDiffs(MyMusicLibraryData, TheirMusicLibraryData));

            if (!Directory.Exists(LibraryDiffOutputLocation.FullName))
            {
                Directory.CreateDirectory(LibraryDiffOutputLocation.FullName);
            }

            string timestampedFileName = LibraryDiffOutputLocation.FullName + "_" + DateTime.Now.ToString();
            File.WriteAllLines(timestampedFileName, text);
        }
    }
}