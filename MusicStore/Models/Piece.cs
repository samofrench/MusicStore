using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Piece
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Composer")]
        public int ComposerId { get; set; }

        public virtual Composer Composer { get; set; }

        [DisplayName("Piece")]
        public string Name { get; set; }

        public virtual ICollection<Recording> Recordings { get; set; }

        public Piece() {}

        public Piece(Composer composer, int id, string name)
        {
            Composer = composer;
            ComposerId = id;
            Name = name;
        }
    }
}