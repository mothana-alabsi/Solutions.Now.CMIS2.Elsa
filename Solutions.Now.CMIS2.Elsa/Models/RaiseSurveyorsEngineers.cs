using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class RaiseSurveyorsEngineers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Serial { get; set; }
        public int? TenderSerial { get; set; }
        public int? ProjectSerial { get; set; }
        public int? RaiseSurveyorsSerial { get; set; }
        public string? AssignedEngineer { get; set; }

    }
}
