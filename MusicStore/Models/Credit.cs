using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Credit
    {
        [Key]
        public int CreditId { get; set; }

        [ForeignKey("Performer")]
        public int PerformerId { get; set; }

        [ForeignKey("Recording")]
        public int RecordingId { get; set; }

        public string Role { get; set; }

        public virtual Performer Performer { get; set; }

        public virtual Recording Recording { get; set; }

        public Credit()
        {
        }

        public Credit(int perfId, Performer perf, int recId, Recording rec, string role)
        {
            PerformerId = perfId;
            Performer = perf;
            RecordingId = recId;
            Recording = rec;
            Role = role;
        }

    }
}