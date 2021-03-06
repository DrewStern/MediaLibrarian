﻿namespace MediaLibrarian
{
    /// <summary>
    /// Represents the full JSON object body received back from Metal Archives.
    /// </summary>
    public sealed class MetalArchivesResponse
    {
        public string error { get; set; }

        public int iTotalRecords { get; set; }

        public int iTotalDisplayRecords { get; set; }

        public int sEcho { get; set; }

        public string[][] aaData { get; set; }

        public MetalArchivesResponse()
        {
            // intentionally empty - used only for deserialization of http response
        }
    }
}
