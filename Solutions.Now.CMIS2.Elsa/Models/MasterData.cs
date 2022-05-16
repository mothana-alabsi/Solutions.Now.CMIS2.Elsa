using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class MasterData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? serial { get; set; }
        public int? masterSerial { get; set; }
        public string? descAR { get; set; }
        public string? descEN { get; set; }
        public int? refSerial { get; set; }
        public int? masterRefSerial { get; set; }
        public string? screen { get; set; }
    }
}
