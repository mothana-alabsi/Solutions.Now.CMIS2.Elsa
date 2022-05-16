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
       DisplayName = "Committee Member For Warranty Users",
       Description = "Committee Member For Warranty Users in WorkflowRules Table",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class CommitteeMemberWarrantyUsers : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public CommitteeMemberWarrantyUsers(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }
        [ActivityInput(Hint = "Enter an expression that evaluates to the Type.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int Type { get; set; }
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            IList<string> userNameDB = new List<string>();
            List<CommitteeMemberForWarranty> committeeMember = _cmis2DbContext.CommitteeMemberForWarranty.AsQueryable().Where(s => s.masterSerial == RequestSerial && (s.captain == 0 || s.captain == null)).ToList<CommitteeMemberForWarranty>();
            TblUsers users;
            if (committeeMember == null || committeeMember.Count() == 0)
            {
                userNameDB.Add("none");
                OutputActivityData infoX = new OutputActivityData
                {
                    names = userNameDB.ToList<string>()
                };
                context.Output = infoX;
                return Done();
            }
              else {
                try
                {

                    for (int i = 0; i < committeeMember.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(committeeMember[i].section.ToString()) && committeeMember[i].section != 0)
                        {
                            users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == committeeMember[i].section && u.Position == 3116);
                            userNameDB.Add(users.Username);


                        }
                        userNameDB.Add(committeeMember[i].userName);
                    }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                OutputActivityData infoX = new OutputActivityData
                {
                    names = userNameDB.ToList<string>()
                };
                context.Output = infoX;


                return Done();  }
        }
    }
}
