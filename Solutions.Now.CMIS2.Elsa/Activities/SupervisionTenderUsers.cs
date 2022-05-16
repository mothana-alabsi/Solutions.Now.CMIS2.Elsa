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
          DisplayName = "Supervision tender approvals",
          Description = "Add Users of Supervision tender approvals",
          Outcomes = new[] { OutcomeNames.Done }
      )]
    public class SupervisionTenderUsers : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public SupervisionTenderUsers(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            const int adminstrationPosition = 467;
            const int directoratePosition = 468;
            const int sectionPosition = 469;
            const int adminstrationFinancial = 127;
            const int directorateFinancial = 154;
            const int buildingAdministration = 124;
            const int roadAdministration = 125;
            const int financeSectorBuildings = 91;
            const int financeSectorRoad = 92;
            const int financeSectorProvincialAffairs = 90;
            const int secretaryGeneralPosition = 470;
            const int labAdminstrationPosition = 464;
            int refSerail = 0;
            IList<int?> steps = new List<int?>();
            IList<string> userNameDB = new List<string>();
            IList<string> forms = new List<string>();
            List<WorkFlowRules> workFlowRules = _cmis2DbContext.WorkFlowRules.AsQueryable().Where(s => (s.workflow == 433 && s.type == 441)).OrderBy(s => s.step).ToList<WorkFlowRules>();
            
            for (int i = 0; i < workFlowRules.Count; i++)
            {
                userNameDB.Add(workFlowRules[i].username);
                steps.Add(workFlowRules[i].step);
                forms.Add(workFlowRules[i].screen);
            }
            try
            {
                Tender tender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(s => s.tenderSerial.ToString().Equals(RequestSerial.ToString()));
                refSerail = (int)tender.tenderRef;
                TblUsers users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Consultant == tender.tenderConsultant1 && u.Position == 424);
                userNameDB[1] = userNameDB[9] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Administration == tender.adminstration && u.Position == 467);
                userNameDB[2] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == tender.directorate && u.Position == 468);
                userNameDB[3] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == tender.section && u.Position == 469);
                userNameDB[4] = users.Username; 
                Tender refTender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(r => r.tenderSerial == tender.tenderRef);
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == (refTender.adminstration == buildingAdministration ? financeSectorBuildings : (tender.adminstration == roadAdministration ? financeSectorRoad : financeSectorProvincialAffairs)) && u.Position == sectionPosition);
                userNameDB[7] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == directorateFinancial && u.Position == directoratePosition);
                userNameDB[6] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Administration == adminstrationFinancial && u.Position == adminstrationPosition);
                userNameDB[5] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Username.Equals(refTender.Accountant));
                userNameDB[8] = users.Username;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            OutputActivityData infoX = new OutputActivityData
            {
                requestSerial = RequestSerial,
                refRequestSerial = refSerail,
                steps = steps,
                names = userNameDB,
                screens = forms
            };
            context.Output = infoX;
            return Done();
        }
    }
}
