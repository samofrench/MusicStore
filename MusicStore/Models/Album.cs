using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("RecordLabel")]
        public int RecordLabelId { get; set; }

        [DisplayName("Record Label")]
        public virtual RecordLabel RecordLabel { get; set; }

        [DisplayName("Catalogue Number")]
        public string CatNo { get; set; }

        public string ArtworkUrl { get; set; }

        public string Notes { get; set; }

        public Enums.Country Country { get; set; }

        public int? Year { get; set; }

        public int Discs { get; set; }

        public Enums.Audio Audio { get; set; }

        public virtual ICollection<Recording> Recordings { get; set; }
    }
}