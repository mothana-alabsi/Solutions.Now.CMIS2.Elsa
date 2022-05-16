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
       DisplayName = "FinalReceiptWorkCommitteeUsers",
       Description = "FinalReceiptWorkCommitteeUsers in WorkflowRules Table",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class FinalReceiptWorkCommitteeUsers : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public FinalReceiptWorkCommitteeUsers(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            IList<string> userNameDB = new List<string>();
            List<CommitteeMember_FinalReceiptWork> committeeMember_FinalReceiptWorks = _cmis2DbContext.CommitteeMember_FinalReceiptWork.AsQueryable().Where(s => s.finalReceiptWorkSerial == RequestSerial).ToList<CommitteeMember_FinalReceiptWork>();
            TblUsers users;
            try
            {
                for (int i = 0; i < committeeMember_FinalReceiptWorks.Count; i++)
                {
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == committeeMember_FinalReceiptWorks[i].section && u.Position == 469);
                    userNameDB.Add(users.Username);
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == committeeMember_FinalReceiptWorks[i].directorate && u.Position == 468);
                    userNameDB.Add(users.Username);
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Administration == committeeMember_FinalReceiptWorks[i].administration && u.Position == 467);
                    userNameDB.Add(users.Username);
                    userNameDB.Add(committeeMember_FinalReceiptWorks[i].userName);
                }
            }catch(Exception ex)
            {
                ex.Message.ToString();
            }
            OutputActivityData infoX = new OutputActivityData
            {
                names = userNameDB
            };
            context.Output = infoX;
            return Done();
        }
    }
}
