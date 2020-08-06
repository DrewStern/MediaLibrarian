using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;

namespace MediaLibrarian
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        #region TODO: inject these

        private static MetalArchivesServiceClient _metalArchivesServiceClient;
        private static MetalArchivesServiceProvider _metalArchivesServiceProvider;
        private static MetalArchivesResponseParser _metalArchivesResponseParser;
        private static MetalArchivesResponseFilterer _metalArchivesResponseFilterer;

        private static MetalArchivesServiceProvider MetalArchivesServiceProvider
        {
            get { return _metalArchivesServiceProvider ?? (_metalArchivesServiceProvider = new MetalArchivesServiceProvider()); }
        }

        private static MetalArchivesResponseParser MetalArchivesResponseParser
        {
            get { return _metalArchivesResponseParser ?? (_metalArchivesResponseParser = new MetalArchivesResponseParser()); }
        }

        private static MetalArchivesResponseFilterer MetalArchivesResponseFilterer
        {
            get { return _metalArchivesResponseFilterer ?? (_metalArchivesResponseFilterer = new MetalArchivesResponseFilterer()); }
        }

        private static MetalArchivesServiceClient MetalArchivesServiceClient
        {
            get { return _metalArchivesServiceClient ?? (_metalArchivesServiceClient = new MetalArchivesServiceClient(MetalArchivesServiceProvider, MetalArchivesResponseParser, MetalArchivesResponseFilterer)); }
        }

        #endregion

        private static CliArgRegistrar<MediaLibraryCliArgKey> _argRegistrar;

        private static CliHandler _cliHandler;

        static void Main(string[] args)
        {
            _cliHandler = new CliHandler();
            var _argRegistrar = _cliHandler.GetCliArgRegistrar<MediaLibraryCliArgKey>(args);
            MusicLibrary differences = null;

            try
            {
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
            var localLibraryPath = _argRegistrar.Registry[MediaLibraryCliArgKey.INPUT];
            var localMusicLibrary = new MusicLibrary(new DirectoryInfo(localLibraryPath));
            Console.WriteLine($"Discovered {localMusicLibrary.Collection.Count} items on disk");
            return localMusicLibrary;
        }

        private static MusicLibrary GetRemoteMusicLibrary(MusicLibrary musicLibrary)
        {
            var remoteMusicLibrary = new MusicLibrary(new List<MusicLibraryItem>());

            foreach (string artistName in musicLibrary.Artists.Select(ad => ad.ArtistName))
            {
                var request = BuildMetalArchivesRequest(artistName);

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
            DirectoryInfo resultDirectory = new DirectoryInfo(_argRegistrar.Registry[MediaLibraryCliArgKey.OUTPUT]);

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