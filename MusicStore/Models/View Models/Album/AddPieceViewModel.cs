using System.Collections.Generic;

namespace MusicStore.Models.View_Models.Album
{
    public class AddPieceViewModel
    {
        public int PieceId { get; set; }
        public string PieceName { get; set; }
        public int ComposerId { get; set; }
        public string ComposerName { get; set; }

        // Credits
        public List<AddPerformerViewModel> Credits { get; set; }

        public AddPieceViewModel()
        {
            Credits = new List<AddPerformerViewModel>();        
        }

    }
}