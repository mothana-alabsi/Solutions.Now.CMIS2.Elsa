using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class CommunicationEng
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? serial { get; set; }
        public int? tenderSerial { get; set; }
        public string? usernameEng { get; set; }
        public int? status { get; set; }


    }
}
