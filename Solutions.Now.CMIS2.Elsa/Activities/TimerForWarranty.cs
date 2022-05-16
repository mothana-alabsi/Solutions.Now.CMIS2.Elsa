using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using Solutions.Now.CMIS2.Elsa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Activities
{
    [Activity(
       Category = "Approval",
       DisplayName = "TimerForWarranty",
       Description = "TimerForWarranty",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class TimerForWarranty : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public TimerForWarranty(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            //HashSet<string> userNameDB = new HashSet<string>();




            //try
            //{
            //    const int warrantyMaintenanceWorkSerial = 3092;
            //    const int typeAction = 441;
            //    WarrantyMaintenanceWork warrantyMaintenanceWork = await _cmis2DbContext.WarrantyMaintenanceWork.FirstOrDefaultAsync(s => s.maintenanceRequestSerial == RequestSerial);
            //    DateTime d2 = Convert.ToDateTime(warrantyMaintenanceWork.dateForConsultant).AddDays(Convert.ToDouble(warrantyMaintenanceWork.durationContractor));
            //    if (((DateTime.Now) - d2).TotalDays <= 0) {
            //        return Done();
            //    }else
            //    {
            //        return Fault("not invalide");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //   
            //}

            return Done();

        }
    }
}

