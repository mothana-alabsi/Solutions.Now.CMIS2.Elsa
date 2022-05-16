using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class TblUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? EngineerID { get; set; }
        public int? Permission { get; set; }
        public int? GISApproval { get; set; }
        public int? ConsultantApproval { get; set; }
        public int? Administration { get; set; }
        public int? Directorate { get; set; }
        public int? Section { get; set; }
        public int? UserType { get; set; }
        public int? Position { get; set; }
        public int? Major { get; set; }
        public int? Consultant { get; set; }
        public int? Contractor { get; set; }

        public int? designer { get; set; }
    }
}
