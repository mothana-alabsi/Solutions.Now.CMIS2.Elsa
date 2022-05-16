using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class InsurancePolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? policySerial { get; set; }
        public int? tenderSerial { get; set; }
        public DateTime? policyDate { get; set; }
        public int? status { get; set; }

    }
}
