using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services;
using Elsa.Services.Models;
using Solutions.Now.CMIS2.Elsa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa.Expressions;

namespace Solutions.Now.CMIS2.Elsa.Activities
{
    [Activity(
          Category = "Approval",
          DisplayName = "Governorates site delivery approvals",
          Description = "Add Users of Governorates site delivery approvals",
          Outcomes = new[] { OutcomeNames.Done }
      )]
    public class GovernoratesSiteDeliveryUsers : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public GovernoratesSiteDeliveryUsers(Cmis2DbContext cmis2DbContext)
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
            List<WorkFlowRules> workFlowRules = _cmis2DbContext.WorkFlowRules.AsQueryable().Where(s => (s.workflow == 438 && s.type == 441)).OrderBy(s => s.step).ToList<WorkFlowRules>();
            
            for (int i = 0; i < workFlowRules.Count; i++)
            {
                userNameDB.Add(workFlowRules[i].username);
                steps.Add(workFlowRules[i].step);
                forms.Add(workFlowRules[i].screen);
            }
            try
            {
                SiteHandOver siteHandOver = await _cmis2DbContext.SiteHandOver.FirstOrDefaultAsync(s => s.siteHandoverSerial == RequestSerial);
                Tender tender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(s => s.tenderSerial.ToString().Equals(siteHandOver.tenderSerial.ToString()));
                TblUsers users;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == tender.tenderSupervisor && u.Position == 468);
                userNameDB[2] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Contractor == tender.tenderContracter1 && u.Position == 425);
                userNameDB[1] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.designer == tender.designer);
                userNameDB[0] = users.Username;
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
