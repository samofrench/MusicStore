using System.ComponentModel;

namespace MusicStore.Models.View_Models.About
{
    public class ComposerAlbumGroup
    {
        public Models.Composer Composer { get; set; }

        [DisplayName("Album Count")]
        public int AlbumCount { get; set; }
    }
}