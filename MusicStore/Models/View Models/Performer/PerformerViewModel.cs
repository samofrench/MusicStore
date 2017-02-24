using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MusicStore.Models.View_Models.Performer
{
    public class PerformerViewModel
    {
        public int PerformerId { get; set; }

        public string Name { get; set; }

        [DisplayName("Roles(s)")]
        public List<string> Roles { get; set; }

        public int AlbumsCount { get; set; }

        public List<Models.Album> Albums { get; set; }

        public int ComposersCount { get; set; }

        public List<Models.Composer> Composers { get; set; }

        public string RolesFormatted
        {
            get { return string.Join(",", Roles); }
        }
    }
}