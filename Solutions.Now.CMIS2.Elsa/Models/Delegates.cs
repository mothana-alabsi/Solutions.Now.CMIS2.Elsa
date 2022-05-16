using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class Delegates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? serial { get; set; }
        public int? siteHandoverSerial { get; set; }
        public int? administration { get; set; }
        public int? directorate { get; set; }
        public int? section { get; set; }
        public string? username { get; set; }

    }
}
