using System;
using System.Collections.Generic;
using System.Linq;

namespace MetalArchivesLibraryDiffTool
{
    public class LibraryDiff
    {
        private LibraryDiffService _libraryDiffService;
        private Library _mine;
        private Library _theirs;

        public LibraryDiff(Library mine, Library theirs, LibraryDiffService libraryDiffService)
        {
            _mine = mine;
            _theirs = theirs;
            _libraryDiffService = libraryDiffService;
        }

        public List<string> GetUnrecognizedArtists()
        {
            return _libraryDiffService.GetArtistDiffs(_mine, _theirs).Select(x => x.ArtistName).ToList();
        }

        public List<LibraryItem> GetMissingAlbums()
        {
            return _libraryDiffService.GetArtistReleaseDiffs(_mine, _theirs).ToList();
        }
    }
}
