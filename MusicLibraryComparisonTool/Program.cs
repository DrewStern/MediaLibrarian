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


        private static ArgRegistrar<MediaLibraryArgRegistry> _argRegistrar;

        private static MediaLibraryCompareToolCliHandler _cliHandler;

        private static Dictionary<MediaLibraryArgRegistry, string> _args;

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
            _argRegistrar = new ArgRegistrar<MediaLibraryArgRegistry>();
            _cliHandler = new MediaLibraryCompareToolCliHandler(_argRegistrar);
            MusicLibrary differences = null;

            try
            {
                _args = _cliHandler.ParseArgs(args);

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

        private static MusicLibrary FindItemsMissingFromLocal(MusicLibrary local, MusicLibrary remote)
        {
            return (new MusicLibraryComparer()).Compare(local, remote).RightOutersection;
        }

        private static MusicLibrary GetLocalMusicLibrary()
        {
            var localMusicLibrary = new MusicLibrary(new DirectoryInfo(_args[MediaLibraryArgRegistry.INPUT]));
            Console.WriteLine($"Discovered {localMusicLibrary.Collection.Count} items on disk");
            return localMusicLibrary;
        }

        private static MusicLibrary GetRemoteMusicLibrary(MusicLibrary musicLibrary)
        {
            var remoteMusicLibrary = new MusicLibrary(new List<MusicLibraryItem>());

            foreach (string artistName in musicLibrary.Collection.Select(mli => mli.ArtistData).Select(ad => ad.ArtistName).Distinct())
            {
                var request = BuildMetalArchivesRequest(artistName);

                // TODO: refactor so that this method is supplied a MetalArchivesRequestBuilder, which then get handed into the client?
                remoteMusicLibrary.AddToCollection(MetalArchivesServiceClient.Submit(request));

                Console.WriteLine($"Added {artistName} to library");
                Thread.Sleep(3000);
            }

            return remoteMusicLibrary;
        }

        private static MetalArchivesRequest BuildMetalArchivesRequest(string artistName)
        {
            return
                new MetalArchivesRequestBuilder()
                    .FindByArtist(artistName)
                    .FindOnlyFullLengths(true)
                    .Build();
        }

        private static void WriteResults(MusicLibrary differences)
        {
            DirectoryInfo resultDirectory = new DirectoryInfo(_args[MediaLibraryArgRegistry.OUTPUT]);

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