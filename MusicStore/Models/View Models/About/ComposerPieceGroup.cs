using System.ComponentModel;

namespace MusicStore.Models.View_Models.About
{
    public class ComposerPieceGroup
    {
        public Models.Composer Composer { get; set; }

        [DisplayName("Pieces Count")]
        public int PieceCount { get; set; }
    }
}