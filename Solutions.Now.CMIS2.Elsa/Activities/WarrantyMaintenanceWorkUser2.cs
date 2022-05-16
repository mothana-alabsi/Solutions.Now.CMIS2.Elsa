using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services;
using Elsa.Services.Models;
using Solutions.Now.CMIS2.Elsa.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa.Expressions;
using System;

namespace Solutions.Now.CMIS2.Elsa.Activities
{
    [Activity(
         Category = "Approval",
         DisplayName = "Warranty maintenance work2",
         Description = "Add Warranty maintenance work2",
         Outcomes = new[] { OutcomeNames.Done }
     )]
    public class WarrantyMaintenanceWorkUser2 : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public WarrantyMaintenanceWorkUser2(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            const int warrantyMaintenanceWorkSerial = 3092;
            const int typeAction = 441;
            const int adminstrationPosition = 467;
            const int directoratePosition = 468;
            const int sectionPosition = 469;
            const int qualitySection = 2861;
            const int adminstrationFinancial = 127;
            const int directorateFinancial = 154;
            const int financeSectorBuildings = 91;
            const int financeSectorRoad = 92;
            const int financeSectorProvincialAffairs = 90;
            const int buildingAdministration = 124;
            const int roadAdministration = 125;
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
                WarrantyMaintenanceWork warrantymaintenanceWork = await _cmis2DbContext.WarrantyMaintenanceWork.FirstOrDefaultAsync(r => r.maintenanceRequestSerial == RequestSerial);
                //  TblUsers users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3114 && u.Position == 3115);
                Tender tender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(s => s.tenderSerial.Equals(warrantymaintenanceWork.tenderSerial));
   
               
                CommitteeMemberForWarranty captain = await _cmis2DbContext.CommitteeMemberForWarranty.FirstOrDefaultAsync(c => c.captain == 1 && c.masterSerial == RequestSerial);
                userNameDB[1] = captain.userName;
                //users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Position == secretaryGeneralPosition);
                //userNameDB[6] = users.Username;
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

