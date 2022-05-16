using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class TenderAdvancePaymentRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Serial { get; set; }
        public int? tenderSerial { get; set; }
        public int? PaymentNumber { get; set; }
        public int? ClaimStatus { get; set; }


    }
}
