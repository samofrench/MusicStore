using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Performer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Credit> Credits { get; set; }

        public Performer(){}

        public Performer(string name)
        {
            Name = name;
        }
    }
}