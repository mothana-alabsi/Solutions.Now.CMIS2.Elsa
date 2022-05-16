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
          DisplayName = "Consultant Extension approvals",
          Description = "Add Users of Consultant Extension approvals",
          Outcomes = new[] { OutcomeNames.Done }
      )]
    public class ConsultantExtensionUsers : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public ConsultantExtensionUsers(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            const int consultantExtensionWF = 2910;
            const int typeAction = 441;
            const int directoratePosition = 468;
            const int sectionPosition = 469;
            const int adminstrationPosition = 467;
            const int secretaryGeneralPosition = 470;
            const int ministryPosition = 479;
            IList<int?> steps = new List<int?>();
            IList<string> userNameDB = new List<string>();
            IList<string> forms = new List<string>();
            List<WorkFlowRules> workFlowRules = _cmis2DbContext.WorkFlowRules.AsQueryable().Where(s => (s.workflow == consultantExtensionWF && s.type == typeAction)).OrderBy(s => s.step).ToList<WorkFlowRules>();
            
            for (int i = 0; i < workFlowRules.Count; i++)
            {
                userNameDB.Add(workFlowRules[i].username);
                steps.Add(workFlowRules[i].step);
                forms.Add(workFlowRules[i].screen);
            }
            try
            {
                FinancialAndTimeExtension financialAndTimeExtension = await _cmis2DbContext.FinancialAndTimeExtension.FirstOrDefaultAsync(f => f.serial == RequestSerial);
                Tender tender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(s => s.tenderRef.ToString().Equals(financialAndTimeExtension.tenderSerial.ToString()));
                TblUsers users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == tender.section && u.Position == sectionPosition);
                userNameDB[2] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == tender.directorate && u.Position == directoratePosition);
                userNameDB[3] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Administration == tender.adminstration && u.Position == adminstrationPosition);
                userNameDB[4] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Username.Equals(tender.communicationEng));
                userNameDB[1] = users.Username;
                Tender refTender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(r => r.tenderSerial == tender.tenderRef);
                if (tender.TenderConsultType == 36)
                {
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Consultant == refTender.tenderConsultant1 && u.Position == 424);
                }
                else
                {
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == tender.tenderSupervisor && u.Position == directoratePosition);
                }
                userNameDB[0] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Position == ministryPosition);
                userNameDB[6] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Position == secretaryGeneralPosition);
                userNameDB[5] = users.Username;
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
