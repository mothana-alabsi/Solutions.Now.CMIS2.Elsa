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
       DisplayName = "Captain RaiseSurveyors User",
       Description = "Captain RaiseSurveyors User",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class CaptainRaiseSurveyorsUser : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public CaptainRaiseSurveyorsUser(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            RaiseSurveyorsEngineers engineer = await _cmis2DbContext.RaiseSurveyorsEngineers.FirstOrDefaultAsync(m => m.RaiseSurveyorsSerial == RequestSerial);
            context.Output = (string)engineer.AssignedEngineer;
            return Done();
        }
    }
}
