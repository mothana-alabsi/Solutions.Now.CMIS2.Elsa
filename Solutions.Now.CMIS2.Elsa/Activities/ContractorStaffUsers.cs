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
          DisplayName = "Contractor Staff approvals",
          Description = "Add Users of Contractor Staff approvals",
          Outcomes = new[] { OutcomeNames.Done }
      )]
    public class ContractorStaffUsers : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public ContractorStaffUsers(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            IList<int?> steps = new List<int?>();
            IList<string> userNameDB = new List<string>();
            IList<string> forms = new List<string>();
            List<WorkFlowRules> workFlowRules = _cmis2DbContext.WorkFlowRules.AsQueryable().Where(s => (s.workflow == 497 && s.type == 441)).OrderBy(s => s.step).ToList<WorkFlowRules>();
            
            for (int i = 0; i < workFlowRules.Count; i++)
            {
                userNameDB.Add(workFlowRules[i].username);
                steps.Add(workFlowRules[i].step);
                forms.Add(workFlowRules[i].screen);
            }
            try
            {
                Project project = await _cmis2DbContext.Project.FirstOrDefaultAsync(i => i.projecSerial == RequestSerial);
                Tender tender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(s => s.tenderSerial.ToString().Equals(project.tenderSerial.ToString()));
                TblUsers users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == tender.section && u.Position == 469);
                userNameDB[2] = userNameDB[8] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == tender.directorate && u.Position == 468);
                userNameDB[9] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Administration == tender.adminstration && u.Position == 467);
                userNameDB[10] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Username.Equals(tender.communicationEng));
                userNameDB[1] = userNameDB[7] = users.Username; 
                Tender refTender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(r => r.tenderSerial == tender.tenderRef);
                if (tender.TenderConsultType == 36)
                {
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Consultant == refTender.tenderConsultant1 && u.Position == 424);
                }
                else
                {
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == tender.tenderSupervisor && u.Position == 468);
                }
                userNameDB[0] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == 645 && u.Position == 468);
                userNameDB[3] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == 130 && u.Position == 468);
                userNameDB[4] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == 1724 && u.Position == 468);
                userNameDB[5] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == 488 && u.Position == 468);
                userNameDB[6] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Contractor == tender.tenderContracter1 && u.Position == 425);
                userNameDB[11] = users.Username;
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
