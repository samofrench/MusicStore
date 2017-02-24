using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Models.View_Models.Album
{
    public class AddPieceViewModel
    {
        public bool NewPiece { get; set; }
        public bool NewComposer { get; set; }
        public int PieceId { get; set; }
        public string PieceName { get; set; }
        public int ComposerId { get; set; }
        public string ComposerName { get; set; }
        public Guid TempId { get; set; }
    }
}