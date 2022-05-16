using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class AssignmentBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AssignSerial { get; set; }
        public int? TenderSerial { get; set; }
        public int?  Directorate { get; set; }
        public DateTime?  AssignDate { get; set; }
        public int? status { get; set; }


    }
}
