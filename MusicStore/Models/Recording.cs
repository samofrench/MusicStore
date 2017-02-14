using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Models
{
    public class Recording
    {
        [Key]
        public int RecordingId { get; set; }

        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        [ForeignKey("Piece")]
        public int PieceId { get; set; }

        public virtual ICollection<Credit> Credits { get; set; }

        public virtual Album Album { get; set; }

        public virtual Piece Piece { get; set; }

        public Recording()
        {
        }

        public Recording(int albumId, Album album, int pieceId, Piece piece)
        {
            AlbumId = albumId;
            Album = album;
            PieceId = pieceId;
            Piece = piece;
        }
    }
}