using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class Tender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? tenderSerial { get; set; }
        public string? tenderNumber { get; set; }
        public DateTime? tenderYear { get; set; }
        public string? tenderName { get; set; }
        public int? tenderType { get; set; }
        public int? adminstration { get; set; }
        public int? directorate { get; set; }
        public int? section { get; set; }
        public int? subSection { get; set; }
        public int? tenderRef { get; set; }
        public int? tenderSupervisor { get; set; }
        public int? tenderConsultant1 { get; set; }
        public int? tenderContracter1 { get; set; }
        public int? designer { get; set; }
        public string? communicationEng { get; set; }
        public int? TenderConsultType { get; set; }
        public string? Accountant { get; set; }

    }
    }
