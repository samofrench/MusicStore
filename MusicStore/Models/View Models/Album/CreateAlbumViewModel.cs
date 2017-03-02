using System.Collections.Generic;
using System.ComponentModel;

namespace MusicStore.Models.View_Models.Album
{
    public class CreateAlbumViewModel
    {
        // Label
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

        [DisplayName("Add album to my collection")]
        public bool AddAlbumToMyCollection { get; set; }

        // Pieces
        public List<AddPieceViewModel> Pieces { get; set; }

        public CreateAlbumViewModel()
        {
            Pieces = new List<AddPieceViewModel>();
        }
    }
}