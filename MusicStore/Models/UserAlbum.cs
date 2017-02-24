using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class UserAlbum
    {
        [Key]
        public int UserAlbumId { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual Album Album { get; set; }

        public string Sound { get; set; }

        public string Condition { get; set; }

        public string Eq { get; set; }

        public string Notes { get; set; }

        public bool Clean { get; set; }

        public bool Sell { get; set; }

        public bool Donate { get; set; }

        public bool Needs { get; set; }

    }
}