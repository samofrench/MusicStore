using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class RecordLabel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Enums.Country Country { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}