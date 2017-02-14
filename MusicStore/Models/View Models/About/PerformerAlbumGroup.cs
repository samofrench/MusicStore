using System.ComponentModel;

namespace MusicStore.Models.View_Models.About
{
    public class PerformerAlbumGroup
    {
        public Models.Performer Performer { get; set; }

        [DisplayName("Album Count")]
        public int AlbumCount { get; set; }
    }
}