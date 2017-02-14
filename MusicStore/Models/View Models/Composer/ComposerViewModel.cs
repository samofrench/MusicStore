using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models.View_Models.Composer
{
    public class ComposerViewModel
    {
        public int ComposerId { get; set; }

        public string FullName { get; set; }

        public int? BirthYear { get; set; }

        [DisplayFormat(NullDisplayText = "*")]
        public int? DeathYear { get; set; }

        public List<Models.Piece> Pieces { get; set; }

        public int PiecesCount { get; set; }

        public List<Models.Album> Albums { get; set; }

        public int AlbumsCount { get; set; }

        public List<Models.Performer> Performers { get; set; }

        public int PerformersCount { get; set; }
    }
}