using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class WarrantyMaintenanceWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int maintenanceRequestSerial { get; set; }

        public int? tenderSerial { get; set; }

        public string? maintenanceEngineer { get; set; }

        public string? ministryEngineer { get; set; }



    }
}
