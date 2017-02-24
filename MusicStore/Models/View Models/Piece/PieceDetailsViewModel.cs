using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace MusicStore.Models.View_Models.Piece
{
    public class PieceDetailsViewModel
    {
        public int PieceId { get; set; }

        public string Name { get; set; }

        public int ComposerId { get; set; }

        public Models.Composer Composer { get; set; }

        public int AlbumsCount { get; set; }

        public List<Models.Album> Albums { get; set; }

        public int PerformersCount { get; set; }

        public List<Models.Performer> Performers { get; set; }
    }
}