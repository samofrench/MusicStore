using System.Collections.Generic;

namespace MusicStore.Models.View_Models.Album
{
    public class CreateAlbumViewModel
    {
        // Label
        public bool NewLabel { get; set; }
        public int RecordLabelId { get; set; }
        public string RecordLabelName { get; set; }

        // Album
        public string Name { get; set; }
        public string CatNo { get; set; }
        public string ArtworkUrl { get; set; }
        public string Notes { get; set; }
        public Enums.Country Country { get; set; }
        public int Year { get; set; }
        public int Discs { get; set; }
        public Enums.Audio Audio { get; set; }

        // Pieces
        public List<AddPieceViewModel> Pieces { get; set; }

        // Performers
        public List<AddPerformerViewModel> Credits { get; set; }
    }
}