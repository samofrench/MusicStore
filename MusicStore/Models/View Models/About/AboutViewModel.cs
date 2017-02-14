using System.Collections.Generic;

namespace MusicStore.Models.View_Models.About
{
    public class AboutViewModel
    {
        public List<LabelAlbumGroup> LabelAlbumData { get; set; }

        public List<ComposerPieceGroup> ComposerPieceData { get; set; }

        public List<ComposerAlbumGroup> ComposerAlbumData { get; set; }

        public List<PerformerAlbumGroup> PerformerAlbumData { get; set; }
    }
}