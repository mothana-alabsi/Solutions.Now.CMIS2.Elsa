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
       DisplayName = "Add Engineer Warranty Users",
       Description = "Add Engineer Warranty Users",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class AddEngineerForWarrantyUser : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public AddEngineerForWarrantyUser(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            //HashSet<string> userNameDB = new HashSet<string>();
            WarrantyMaintenanceWork warrantyMaintenanceWork = await _cmis2DbContext.WarrantyMaintenanceWork.FirstOrDefaultAsync(s => s.maintenanceRequestSerial == RequestSerial);
            TblUsers users;
            const int warrantyMaintenanceWorkSerial = 3092;
            const int typeAction = 441;           
            IList<int?> steps = new List<int?>();
            IList<string> userNameDB = new List<string>();
            IList<string> forms = new List<string>();
            List<WorkFlowRules> workFlowRules = _cmis2DbContext.WorkFlowRules.AsQueryable().Where(s => (s.workflow == warrantyMaintenanceWorkSerial)).OrderBy(s => s.step).ToList<WorkFlowRules>();

            for (int i = 0; i < workFlowRules.Count; i++)
            {
                userNameDB.Add(workFlowRules[i].username);
                steps.Add(workFlowRules[i].step);
                forms.Add(workFlowRules[i].screen);
            }
            try
            {         
                        users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Username.Equals(warrantyMaintenanceWork.maintenanceEngineer) );
                        userNameDB[7]=users.Username;                  
                
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            OutputActivityData infoX = new OutputActivityData
            {
                requestSerial = RequestSerial,
                steps = steps,
                names = userNameDB,
                screens = forms
            };
            context.Output = infoX;
            return Done();
        }
    }
}

