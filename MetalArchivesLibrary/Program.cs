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

        private static LibraryData MyLibraryData { get; set; }

        private static LibraryData TheirLibraryData { get; set; }

        private static LibraryDiff LibraryDiff { get; set; }

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

                MyLibraryData = new LibraryData(LibraryLocation);

                foreach (string artistName in MyLibraryData.Artists)
                {
                    TheirLibraryData = new LibraryData(MetalArchivesHttpClient.FindReleases(artistName));

                    LibraryDiff = new LibraryDiff(MyLibraryData, TheirLibraryData);
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
            diffText2[1] = String.Join(Environment.NewLine, LibraryDiff.GetUnrecognizedArtists());
            diffText2[2] = "----- Releases missing from your collection -----";
            diffText2[3] = String.Join(Environment.NewLine, LibraryDiff.GetMissingAlbums().Select(x => x.ToString()));

            File.WriteAllLines(LibraryDiffOutputLocation.FullName, diffText2);
        }
    }
}