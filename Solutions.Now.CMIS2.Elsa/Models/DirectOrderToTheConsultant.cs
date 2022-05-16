using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class DirectOrderToTheConsultant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? DirectOrderToConsultantSerial { get; set; }
        public int? tenderSerial { get; set; }
        public int? projectSerial { get; set; }
        public DateTime? directOrderDate { get; set; }


    }
}
