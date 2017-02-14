using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Composer
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public int? BirthYear { get; set; }


        [DisplayFormat(NullDisplayText = "*")]
        public int? DeathYear { get; set; }

        public virtual ICollection<Piece> Pieces { get; set; }

        public Composer() {}

        public Composer(string fName, string lName, int bYear, int dYear)
        {
            FirstName = fName;
            LastName = lName;
            BirthYear = bYear;
            DeathYear = dYear;
        }
    }
}