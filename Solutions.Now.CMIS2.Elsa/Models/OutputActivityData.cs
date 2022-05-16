using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class OutputActivityData
    {
        [Display(Name = "requestSerial")]
        public int? requestSerial { get; set; }
        [Display(Name = "refRequestSerial")]
        public int? refRequestSerial { get; set; }
        [Display(Name = "steps")]
        public IList<int?> steps { get; set; }
        [Display(Name = "names")]
        public IList<string?> names { get; set; }
        [Display(Name = "screen")]
        public IList<string?> screens { get; set; }
    }
}
