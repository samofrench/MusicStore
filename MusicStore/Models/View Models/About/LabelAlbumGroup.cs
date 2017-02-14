using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace MusicStore.Models.View_Models
{
    public class LabelAlbumGroup
    {
        [DisplayName("Record Label")]
        public RecordLabel RecordLabel { get; set; }

        [DisplayName("Albums Count")]
        public int AlbumCount { get; set; }
    }
}