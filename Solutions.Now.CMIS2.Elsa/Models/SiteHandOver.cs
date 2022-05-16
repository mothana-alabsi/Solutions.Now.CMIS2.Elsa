using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class SiteHandOver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? siteHandoverSerial { get; set; }
        public int? tenderSerial { get; set; }
        public int? projectSerial { get; set; }
        public DateTime? siteHandOverDate { get; set; }
        public int? status { get; set; }


    }
}
