using System;

namespace MusicStore.Models.View_Models.Album
{
    public class AddPerformerViewModel
    {
        public Guid PieceId { get; set; }
        public bool NewPerformer { get; set; }
        public int PerformerId { get; set; }
        public string PerformerName { get; set; }
        public string Role { get; set; }
    }
}