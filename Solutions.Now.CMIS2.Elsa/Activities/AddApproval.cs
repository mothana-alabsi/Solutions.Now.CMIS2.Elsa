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
       DisplayName = "Add approval",
       Description = "Add approval in ApprovalHistory Table",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class AddApproval : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public AddApproval(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Type.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestType { get; set; }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Step.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int Step { get; set; }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Name.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public string ActionBy { get; set; }

        [ActivityInput(Hint = "Enter an expression that evaluates to URL.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public string URL { get; set; }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Form.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public string Form { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var approvalHistory = new ApprovalHistory
            {
                requestSerial = RequestSerial,
                requestType = RequestType,
                createdDate = DateTime.Now,
                actionBy = ActionBy,
                expireDate = DateTime.Today.AddDays(10),
                step = Step,
                URL = URL,
                Form = Form + RequestSerial.ToString(),
                status = 448
            };
            try
            {
                //await _cmis2DbContext.ApprovalHistory.AddAsync(approvalHistory);
                // await _cmis2DbContext.SaveChangesAsync();
                var @connectionString = "Server=10.71.20.71;Uid=sa;Pwd=P@ssw0rd321;Database=CMIS2";
                SqlConnection connection = new SqlConnection(@connectionString);
                string query = "INSERT INTO [CMIS2].[dbo].[ApprovalHistory] ([requestSerial] ,[requestType] ,[createdDate],[actionBy],[expireDate],[status],[URL],[Form],[step]) ";
                query = query + " values (" + approvalHistory.requestSerial + ", " + approvalHistory.requestType + ",  GETDATE(), '" + approvalHistory.actionBy + "', GETDATE()+10 , " + approvalHistory.status + ", '" + approvalHistory.URL + "', '" + approvalHistory.Form + "', " + approvalHistory.step + ");";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Records Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message.ToString());
            }
            return Done();
        }
    }
}
