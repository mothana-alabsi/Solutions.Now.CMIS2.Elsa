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
       DisplayName = "Captain Committee User",
       Description = "Captain Committee User",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class CaptainCommitteeUser : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public CaptainCommitteeUser(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }
        [ActivityInput(Hint = "Enter an expression that evaluates to the Type.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int Type { get; set; }
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            CommitteeMember committeeMember = await _cmis2DbContext.CommitteeMember.FirstOrDefaultAsync(m => m.captain == 1 && m.masterSerial == RequestSerial && m.type == Type);
            context.Output = (string)committeeMember.userName;
            return Done();
        }
    }
}
