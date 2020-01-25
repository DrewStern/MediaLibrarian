using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    /// <summary>
    /// The purpose of this application is to compare the content of a given library of metal music against a database of known metal releases.
    /// Primarily to help me find good things I might be missing in my collection. :)
    /// </summary>
    class Program
    {
        private static DirectoryInfo LibraryLocation { get; set; }

        private static DirectoryInfo LibraryDiffOutputLocation { get; set; }

        private static Library MyLibraryData { get; set; }

        private static Library TheirLibraryData { get; set; }

        private static LibraryDiffService LibraryDiffService { get; set; }

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

                foreach (ArtistData artist in MyLibraryData.Artists)
                {
                    TheirLibraryData = new Library(MetalArchivesHttpClient.FindReleases(artist.ArtistName));
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
            string[] diffText2 = new string[4];
            diffText2[0] = "----- Artists with no matching results -----";
            diffText2[1] = String.Join(Environment.NewLine, LibraryDiffService.GetArtistDiffs(MyLibraryData, TheirLibraryData));
            diffText2[2] = "----- Releases missing from your collection -----";
            diffText2[3] = String.Join(Environment.NewLine, LibraryDiffService.GetArtistReleaseDiffs(MyLibraryData, TheirLibraryData));

            File.WriteAllLines(LibraryDiffOutputLocation.FullName, diffText2);
        }
    }
}