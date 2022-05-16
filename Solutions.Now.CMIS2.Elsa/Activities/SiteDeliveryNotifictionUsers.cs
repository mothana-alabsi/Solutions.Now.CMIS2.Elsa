using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.Data.SqlClient;
using Solutions.Now.CMIS2.Elsa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Activities
{
    [Activity(
       Category = "Approval",
       DisplayName = "SiteDeliveryNotifictionUsers",
       Description = "SiteDeliveryNotifictionUsers in WorkflowRules Table",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class SiteDeliveryNotifictionUsers : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public SiteDeliveryNotifictionUsers(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            HashSet<string> userNameDB = new HashSet<string>();
            List<Delegates> delegates = _cmis2DbContext.Delegates.AsQueryable().Where(s => s.siteHandoverSerial == RequestSerial).ToList<Delegates>();
            TblUsers users;
            try
            {
                for (int i = 0; i < delegates.Count; i++)
                {
                    if (!String.IsNullOrEmpty(delegates[i].section.ToString()))
                    {
                        users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == delegates[i].section && u.Position == 469);
                        userNameDB.Add(users.Username);
                        users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == delegates[i].directorate && u.Position == 468);
                        userNameDB.Add(users.Username);
                        users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Administration == delegates[i].administration && u.Position == 467);
                        userNameDB.Add(users.Username);
                    }
                    userNameDB.Add(delegates[i].username);
                }
            }catch(Exception ex)
            {
                ex.Message.ToString();
            }
            OutputActivityData infoX = new OutputActivityData
            {
                names = userNameDB.ToList<string>()
            };
            context.Output = infoX;
            return Done();
        }
    }
}
