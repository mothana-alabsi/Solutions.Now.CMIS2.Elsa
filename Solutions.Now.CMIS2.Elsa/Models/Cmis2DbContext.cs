using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Models
{
    public class Cmis2DbContext : DbContext
    {
        public Cmis2DbContext(DbContextOptions<Cmis2DbContext> options) : base(options)
        {

        }
        public DbSet<ApprovalHistory> ApprovalHistory { get; set; }
        public DbSet<WorkFlowRules> WorkFlowRules { get; set; }
        public DbSet<Tender> Tender { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<MasterData> MasterData { get; set; }
        public DbSet<TblUsers> TblUsers { get; set; }
        public DbSet<DirectOrderToTheConsultant> DirectOrderToTheConsultant { get; set; }
        public DbSet<CommunicationEng> CommunicationEng { get; set; }
        public DbSet<AssignmentBook> AssignmentBook { get; set; }
        public DbSet<SiteHandOver> SiteHandOver { get; set; }
        public DbSet<RaiseSurveyors> RaiseSurveyors { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicy { get; set; }
        public DbSet<TenderAdvancePaymentRequest> TenderAdvancePaymentRequest { get; set; }
        public DbSet<Delegates> Delegates { get; set; }
        public DbSet<ReserveRatio> ReserveRatio { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<FinancingExpenses> FinancingExpenses { get; set; }
        public DbSet<InitialReceiptWork> InitialReceiptWork { get; set; }
        public DbSet<FinalReceiptWork> FinalReceiptWork { get; set; }
        public DbSet<ApprovalOfDesignMixtures> ApprovalOfDesignMixtures { get; set; }
        public DbSet<DirectOrderToTheContractor> DirectOrderToTheContractor { get; set; }
        public DbSet<FinalEvaluationImplementation> FinalEvaluationImplementation { get; set; }
        public DbSet<FinalEvaluationSupervisor> FinalEvaluationSupervisor { get; set; }
        public DbSet<CommitteeMember_InitialReceiptWork> CommitteeMember_InitialReceiptWork { get; set; }
        public DbSet<CommitteeMember_FinalReceiptWork> CommitteeMember_FinalReceiptWork { get; set; }
        public DbSet<AdditionalWork> AdditionalWork { get; set; }
        public DbSet<ReducedOfficeSupport> ReducedOfficeSupport { get; set; }
        public DbSet<SiteVisit> SiteVisit { get; set; }
        public DbSet<FinancialAndTimeExtension> FinancialAndTimeExtension { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<QualityProjects> QualityProjects { get; set; }
        public DbSet<QualityProjectsConcrete> QualityProjectsConcrete { get; set; }
        public DbSet<DailyWorkReport> DailyWorkReport { get; set; }
        public DbSet<ChangeOrder> ChangeOrder { get; set; }
        public DbSet<AccidentReport> AccidentReport { get; set; }
        public DbSet<NCReport> NCReport { get; set; }
        public DbSet<SampleApprovalForm> SampleApprovalForm { get; set; }
        public DbSet<ShopDrawingApprovalRequest> ShopdrawingApprovalRequest { get; set; }
        public DbSet<SiteMemo> SiteMemo { get; set; }
        public DbSet<ApproveSomeWork> ApproveSomeWork { get; set; }
        public DbSet<CommitteeMember> CommitteeMember { get; set; }
        public DbSet<LabApproval> LabApproval { get; set; }
        public DbSet<RaiseSurveyorsEngineers> RaiseSurveyorsEngineers { get; set; }
        public DbSet<WarrantyMaintenanceWork> WarrantyMaintenanceWork { get; set; }
        public DbSet<SiteVisitForWarranty> SiteVisitForWarranty { get; set; }
        public DbSet<CommitteeMemberForWarranty> CommitteeMemberForWarranty { get; set; }

    }
}
