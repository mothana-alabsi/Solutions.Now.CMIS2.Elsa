using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class CommitteeMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? serial { get; set; }
        public int? masterSerial { get; set; }
        public int? administration { get; set; }
        public int? directorate { get; set; }
        public int? section { get; set; }
        public string? userName { get; set; }
        public int? captain { get; set; }

        public int? type { get; set; }

    }
}
