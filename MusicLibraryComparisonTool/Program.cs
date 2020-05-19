using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
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
        #region TODO: inject these

        private static MetalArchivesServiceClient _metalArchivesServiceClient;
        private static MetalArchivesServiceProvider _metalArchivesServiceProvider;
        private static MetalArchivesResponseParser _metalArchivesResponseParser;

        private static MetalArchivesServiceProvider MetalArchivesServiceProvider
        {
            get { return _metalArchivesServiceProvider ?? (_metalArchivesServiceProvider = new MetalArchivesServiceProvider()); }
        }

        private static MetalArchivesResponseParser MetalArchivesResponseParser
        {
            get { return _metalArchivesResponseParser ?? (_metalArchivesResponseParser = new MetalArchivesResponseParser()); }
        }

        private static MetalArchivesServiceClient MetalArchivesServiceClient
        {
            get { return _metalArchivesServiceClient ?? (_metalArchivesServiceClient = new MetalArchivesServiceClient(MetalArchivesServiceProvider, MetalArchivesResponseParser)); }
        }

        #endregion

        private static Dictionary<KnownArg, string> _argMap;
        private static Dictionary<KnownArg, string> ArgMap
        {
            get
            {
                return _argMap ?? (_argMap = new Dictionary<KnownArg, string>());
            }
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
            InitializeArgs();
            MusicLibrary differences = null;

            try
            {
                ParseArgs(args);

                // TODO: stop assuming that I'm going to do MusicLibrary comparisons - need to further parse args to see what's being asked for
                var localMusicLibrary = GetLocalMusicLibrary();
                var remoteMusicLibrary = GetRemoteMusicLibrary(localMusicLibrary);
                differences = FindItemsMissingFromLocal(localMusicLibrary, remoteMusicLibrary);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Exception: {exc.Message}");
            }
            finally
            {
                WriteResults(differences);
            }
        }

        private static void InitializeArgs()
        {
            foreach (KnownArg arg in Enum.GetValues(typeof(KnownArg)))
            {
                ArgMap.Add(arg, String.Empty);
            }
        }

        private static void ParseArgs(string[] args)
        {
            foreach (string arg in args)
            {
                var argPair = StripArgPrefix(arg);

                var argKey = argPair.Split('=')[0];
                var argValue = argPair.Split('=')[1];

                if (Enum.TryParse(argKey.ToUpperInvariant(), out KnownArg parsedArg))
                {
                    ArgMap[parsedArg] = argValue;
                }
                else
                {
                    Console.WriteLine("Unknown arg key, value ignored: " + argKey);
                }
            }
        }

        private static string StripArgPrefix(string arg)
        {
            return
                arg.StartsWith("--") ? arg.Replace("--", String.Empty) :
                arg.StartsWith("-") ? arg.Replace("-", String.Empty) :
                arg.StartsWith("/") ? arg.Replace("/", String.Empty) : 
                arg;
        }

        private static MusicLibrary FindItemsMissingFromLocal(MusicLibrary local, MusicLibrary remote)
        {
            return (new MusicLibraryComparer()).Compare(local, remote).RightOutersection;
        }

        private static MusicLibrary GetLocalMusicLibrary()
        {
            var localMusicLibrary = new MusicLibrary(new DirectoryInfo(ArgMap[KnownArg.INPUT]));
            Console.WriteLine($"Discovered {localMusicLibrary.Collection.Count} items on disk");
            return localMusicLibrary;
        }

        private static MusicLibrary GetRemoteMusicLibrary(MusicLibrary musicLibrary)
        {
            var remoteMusicLibrary = new MusicLibrary(new List<MusicLibraryItem>());

            foreach (string artistName in musicLibrary.Collection.Select(mli => mli.ArtistData).Select(ad => ad.ArtistName).Distinct())
            {
                // TODO: refactor so that this method is supplied a MetalArchivesRequestBuilder, which then get handed into the client?
                remoteMusicLibrary.AddToCollection(/*findByCountry ?
                    MetalArchivesServiceClient.FindByCountry(artistName) :*/
                    MetalArchivesServiceClient.FindByArtist(artistName));

                Console.WriteLine($"Added {artistName} to library");
                Thread.Sleep(3000);
            }

            return remoteMusicLibrary;
        }

        private static void WriteResults(MusicLibrary differences)
        {
            DirectoryInfo resultDirectory = new DirectoryInfo(ArgMap[KnownArg.OUTPUT]);

            if (!Directory.Exists(resultDirectory.Parent.FullName))
            {
                Directory.CreateDirectory(resultDirectory.Parent.FullName);
            }

            // TODO: cleanup
            string timestampedFileName =
                resultDirectory.FullName.Replace(resultDirectory.Extension, "") + 
                "_" + DateTime.Now.ToLongTimeString().Replace(":", "_").Replace(" ", "_") +
                resultDirectory.Extension;

            string text = String.Join(Environment.NewLine, differences?.Collection);
            File.WriteAllText(timestampedFileName, text);
        }
    }
}