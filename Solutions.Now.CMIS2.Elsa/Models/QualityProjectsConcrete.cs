using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class QualityProjectsConcrete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? serial { get; set; }
        public int? tenderSerial { get; set; }
        public int? projectSerial { get; set; }
    }
}
