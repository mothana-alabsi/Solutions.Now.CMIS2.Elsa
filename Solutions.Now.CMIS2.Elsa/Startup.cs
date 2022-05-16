using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solutions.Now.CMIS2.Elsa.Models;
using Solutions.Now.CMIS2.Elsa.Activities;
using Solutions.Now.CMIS2.Elsa.Handlers;
using Solutions.Now.CMIS2.Elsa.Data;
using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Elsa.Scripting.Liquid.Messages;

namespace Solutions.Now.CMIS2.Elsa
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            //Elsa
            var elsaSection = Configuration.GetSection("Elsa");
            services.AddRazorPages();
            services
                 // Required services for Elsa to work. Registers things like `IWorkflowInvoker`.
                 .AddElsa(options => options.UseEntityFrameworkPersistence(ef => ef.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                            .AddConsoleActivities()
                            .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                            .AddQuartzTemporalActivities()
                            .AddEmailActivities()
                            .AddJavaScriptActivities()
                            .AddWorkflowsFrom<Startup>()
                            .AddActivity<AddApproval>()
                            .AddActivity<SupervisionTenderUsers>()
                            .AddActivity<ExecutionTenderUsers>()
                            .AddActivity<CommunicationEngineerDesignationUsers>()
                            .AddActivity<AssignmentBookUsers>()
                            .AddActivity<DirectOrderToTheContractorUsers>()
                            .AddActivity<DirectOrderToTheEngineerOrConsultantUsers>()
                            .AddActivity<GovernoratesSiteDeliveryUsers>()
                            .AddActivity<SiteDeliveryUsers>()
                            .AddActivity<RaiseSurveyorsUsers>()
                            .AddActivity<InsurancePoliciesUsers>()
                            .AddActivity<ContractorStaffUsers>()
                            .AddActivity<CompulsoryLaborStaffUsers>()
                            .AddActivity<ConsultantStaffUsers>()
                            .AddActivity<TheFirstInstallmentUsers>()
                            .AddActivity<TheSecondInstallmentUsers>()
                            .AddActivity<LaboratoryAccreditationUsers>()
                            .AddActivity<AdoptionOfDesignMixturesUsers>()
                            .AddActivity<WorkScheduleUsers>()
                            .AddActivity<ModifiedWorkScheduleUsers>()
                            .AddActivity<DailyWorkflowModelsUsers>()
                            .AddActivity<FinalPaymentUsers>()
                            .AddActivity<ConsultantInterimPaymentUsers>()
                            .AddActivity<NotifictionInterval>()
                            .AddActivity<ChangeOrderUsers>()
                            .AddActivity<DisbursementOfTheFullValueOfTheHoldingsUsers>()
                            .AddActivity<FinalEvaluationImplementationUsers>()
                            .AddActivity<FinalReceiptUsers>()
                            .AddActivity<LatePaymentsDisbursedUsers>()
                            .AddActivity<ProjectQualityControlUsers>()
                            .AddActivity<QualityControlOfConcreteProjectsUsers>()
                            .AddActivity<InitialReceiptUsers>()
                            .AddActivity<ReducingTheHoldingRateUsers>()
                            .AddActivity<CurrnetUser>()
                            .AddActivity<SiteDeliveryNotifictionUsers>()
                            .AddActivity<ContractorInterimPaymentUsers>()
                            .AddActivity<PaymentForCompletionUsers>()
                            .AddActivity<LatePaymentsDisbursedForConsultantUsers>()
                            .AddActivity<FinalEvaluationSupervisorUsers>()
                            .AddActivity<CommitteeMemberUsers>()
                            .AddActivity<FinalReceiptWorkCommitteeUsers>()
                            .AddActivity<AdditionalWorkUsers>()
                            .AddActivity<ReducedOfficeSupportUsers>()
                            .AddActivity<ConsultantExtensionUsers>()
                            .AddActivity<ContractorExtentionUsers>()
                            .AddActivity<SiteMemoUsers>()
                            .AddActivity<LetterOfficialUsers>()
                            .AddActivity<DailyReportUsers>()
                            .AddActivity<RoyalScientificSocietySamplesUsers>()
                            .AddActivity<RoyalScientificSocietySamplesConcreteUsers>()
                            .AddActivity<AccidentReportUsers>()
                            .AddActivity<NCReportUsers>()
                            .AddActivity<SampleApprovalOfficeFormUsers>()
                            .AddActivity<ShopDrawingApprovalRequestUsers>()
                            .AddActivity<SiteMemo1Users>()
                            .AddActivity<ApproveSomeWorkUsers>()
                            .AddActivity<SampleApprovalWorksDirectoratesFormUsers>()
                            .AddActivity<CaptainCommitteeUser>()
                            .AddActivity<ConsultantStaffDesignation>()
                            .AddActivity<CaptainRaiseSurveyorsUser>()
                            .AddActivity<WarrantyMaintenanceWorkUser>()
                            .AddActivity<AddEngineerForWarrantyUser>()
                            .AddActivity<SiteVisitForWarrantyUsers>()
                            .AddActivity<TimerForWarranty>()
                            .AddActivity<WarrantyMaintenanceWorkUserAddMinistryEng>()
                            .AddActivity<CommitteeMemberWarrantyUsers>()
                            .AddActivity<WarrantyMaintenanceWorkUser2>()

                            );


            // Elsa API endpoints.
            services.AddElsaApiEndpoints();
            services.Configure<ApiVersioningOptions>(options => options.UseApiBehavior = false);

            // Allow arbitrary client browser apps to access the API.
            // In a production environment, make sure to allow only origins you trust.
            services.AddCors(cors => cors.AddDefaultPolicy(policy => policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .WithExposedHeaders("Content-Disposition"))
            );

            // Liquid Exeprations
            services.AddNotificationHandler<EvaluatingLiquidExpression, ConfigureLiquidEngine>();

            // Conncation CMIS2 Database.
            services.AddDbContext<Cmis2DbContext>(options => options.UseSqlServer("Server=10.71.20.71;Uid=sa;Pwd=P@ssw0rd321;Database=CMIS2"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpActivities();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                // Attribute-based routing stuff.
                endpoints.MapControllers();
            });
        }
    }
}
