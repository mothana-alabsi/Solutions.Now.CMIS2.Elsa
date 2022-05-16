using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class RaiseSurveyors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? surveySerial { get; set; }
        public int? tenderSerial { get; set; }
        public int? projectSerial { get; set; }
        public DateTime? surveyDate { get; set; }
        public int? status { get; set; }

    }
}
