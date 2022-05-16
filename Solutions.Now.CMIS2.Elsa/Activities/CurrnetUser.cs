using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.Data.SqlClient;
using Solutions.Now.CMIS2.Elsa.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Activities
{
    [Activity(
       Category = "Approval",
       DisplayName = "CurrnetUser",
       Description = "Currnet in WorkflowRules Table",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class CurrnetUser : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public CurrnetUser(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            Tender tender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(u => u.tenderSerial == RequestSerial);
            TblUsers users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(s => s.Position == 468 && s.Directorate == tender.directorate);
            context.Output = (string)users.Username;
            return Done();
        }
    }
}
