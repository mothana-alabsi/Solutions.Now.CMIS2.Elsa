using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? projecSerial { get; set; }
        public int? tenderSerial { get; set; }
        public string? projectName { get; set; }
        public int? projectStatus { get; set; }
        public int? projectType { get; set; }
    }
}
