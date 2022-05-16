using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class ReserveRatio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? serial { get; set; }
        public int? tenderSerial { get; set; }
        public int? projectSerial { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? status { get; set; }

    }
}
