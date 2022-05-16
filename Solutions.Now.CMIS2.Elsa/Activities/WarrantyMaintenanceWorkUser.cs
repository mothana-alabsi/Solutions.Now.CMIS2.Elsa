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
         DisplayName = "Warranty maintenance work",
         Description = "Add Warranty maintenance work",
         Outcomes = new[] { OutcomeNames.Done }
     )]
    public class WarrantyMaintenanceWorkUser : Activity
    {
        private readonly Cmis2DbContext _cmis2DbContext;

        public WarrantyMaintenanceWorkUser(Cmis2DbContext cmis2DbContext)
        {
            _cmis2DbContext = cmis2DbContext;
        }

        [ActivityInput(Hint = "Enter an expression that evaluates to the Request Serial.", DefaultSyntax = SyntaxNames.Literal, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public int RequestSerial { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            const int warrantyMaintenanceWorkSerial = 3092;
            const int typeAction = 441;
            const int adminstrationPosition = 467;
            const int directoratePosition = 468;
            const int sectionPosition = 469;
            const int qualitySection = 2861;
            const int adminstrationFinancial = 127;
            const int directorateFinancial = 154;
            const int financeSectorBuildings = 91;
            const int financeSectorRoad = 92;
            const int financeSectorProvincialAffairs = 90;
            const int buildingAdministration = 124;
            const int roadAdministration = 125;
            IList<int?> steps = new List<int?>();
            IList<string> userNameDB = new List<string>();
            IList<string> forms = new List<string>();
            List<WorkFlowRules> workFlowRules = _cmis2DbContext.WorkFlowRules.AsQueryable().Where(s => (s.workflow == warrantyMaintenanceWorkSerial)).OrderBy(s => s.step).ToList<WorkFlowRules>();

            for (int i = 0; i < workFlowRules.Count; i++)
            {
                userNameDB.Add(workFlowRules[i].username);
                steps.Add(workFlowRules[i].step);
                forms.Add(workFlowRules[i].screen);
            }
            try
            {
                WarrantyMaintenanceWork warrantymaintenanceWork = await _cmis2DbContext.WarrantyMaintenanceWork.FirstOrDefaultAsync(r => r.maintenanceRequestSerial == RequestSerial);
              //  TblUsers users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3114 && u.Position == 3115);
                Tender tender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(s => s.tenderSerial.Equals(warrantymaintenanceWork.tenderSerial));
                TblUsers users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == tender.section && u.Position == sectionPosition);
                userNameDB[29]= users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == tender.directorate && u.Position == directoratePosition);
                userNameDB[9] = userNameDB[15] = userNameDB[19] = userNameDB[30] = userNameDB[39] = userNameDB[45] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Administration == tender.adminstration && u.Position == adminstrationPosition);
                userNameDB[10] = userNameDB[14] = userNameDB[20] = userNameDB[31] = userNameDB[40] = userNameDB[46] = users.Username;
                Tender refTender = await _cmis2DbContext.Tender.FirstOrDefaultAsync(r => r.tenderSerial == tender.tenderRef);
                if (tender.TenderConsultType == 36)
                {
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Consultant == refTender.tenderConsultant1 && u.Position == 424);
                }
                else
                {
                    users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == tender.tenderSupervisor && u.Position == directoratePosition);
                }
                userNameDB[11] = userNameDB[13] = userNameDB[23] = userNameDB[24] = userNameDB[26]= userNameDB[32] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Contractor == tender.tenderContracter1 && u.Position == 425);
                userNameDB[12] = userNameDB[25] = userNameDB[42]= userNameDB[48] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == (tender.adminstration == buildingAdministration ? financeSectorBuildings : (tender.adminstration == roadAdministration ? financeSectorRoad : financeSectorProvincialAffairs)) && u.Position == sectionPosition);
                userNameDB[35] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == directorateFinancial && u.Position == directoratePosition);
                userNameDB[33] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Administration == adminstrationFinancial && u.Position == adminstrationPosition);
                userNameDB[34] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Username.Equals(tender.Accountant));
                userNameDB[36] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3128 && u.Position == 469);
                userNameDB[0]= userNameDB[2] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3118 && u.Position == 469);
                userNameDB[5] = userNameDB[3] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3118 && u.Position == 3131);
                userNameDB[4] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3114 && u.Position == 469);
                userNameDB[6] = userNameDB[8] = userNameDB[16]=userNameDB[18] = userNameDB[28] = userNameDB[38] = userNameDB[44] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3114 && u.Position == 3121);
                userNameDB[7] = userNameDB[17] = userNameDB[27] = userNameDB[43] = userNameDB[47]= userNameDB[49] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3118 && u.Position == 468);
                userNameDB[21] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Section == 3118 && u.Position == 467);
                userNameDB[22] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.Directorate == 3112 && u.Position == 468 && u.Section == null);
                userNameDB[37] = users.Username;
                users = await _cmis2DbContext.TblUsers.FirstOrDefaultAsync(u => u.ID == 3448);
                userNameDB[41] = users.Username;

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

