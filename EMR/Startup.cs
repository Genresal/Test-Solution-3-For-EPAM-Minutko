using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Business.Services;
using EMR.Data.Repositories;
using EMR.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Text.Json.Serialization;

namespace EMR
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string conectionString = Configuration.GetConnectionString("EMR");
            string mainConectionString = Configuration.GetConnectionString("MASTER");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("Logs\\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            services.AddSingleton(Log.Logger);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
                });
            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddRazorPages();

            services.AddTransient<IDbRepository, DbRepository>(provider => new DbRepository(mainConectionString));

            services.AddTransient<IRepository<SickLeave>, SickLeavesRepository>(provider => new SickLeavesRepository(conectionString));
            services.AddTransient<IRepository<Diagnosis>, DiagnosisRepository>(provider => new DiagnosisRepository(conectionString));
            services.AddTransient<IRepository<Drug>, DrugRepository>(provider => new DrugRepository(conectionString));
            services.AddTransient<IRepository<Procedure>, ProcedureRepository>(provider => new ProcedureRepository(conectionString));
            services.AddTransient<IRepository<Role>, RoleRepository>(provider => new RoleRepository(conectionString));
            services.AddTransient<IRepository<User>, UserRepository>(provider => new UserRepository(conectionString));
            services.AddTransient<IRepository<Position>, PositionRepository>(provider => new PositionRepository(conectionString));
            services.AddTransient<IRepository<Doctor>, DoctorsRepository>(provider => new DoctorsRepository(conectionString));
            services.AddTransient<IPatientInfoRepository, PatientInfoRepository>(provider => new PatientInfoRepository(conectionString));
            services.AddTransient<IPatientRepository, PatientsRepository>(provider => new PatientsRepository(conectionString));
            services.AddTransient<IRepository<Record>, RecordsRepository>(provider => new RecordsRepository(conectionString));
            services.AddTransient<IRepository<RecordTreatment>, RecordTreatmentsRepository>(provider => new RecordTreatmentsRepository(conectionString));

            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IBusinessService<Role>, RoleService>();
            services.AddTransient<ISickLeaveService, SickLeaveService>();
            services.AddTransient<IBusinessService<Record>, RecordService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<ITreatmentService, RecordTreatmentService>();
            services.AddTransient<IDrugService, DrugService>();
            services.AddTransient<IProcedureService, ProcedureService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IRecordPageService, RecordPageService>();
            services.AddTransient<IPatientPageService, PatientPageService>();
            services.AddTransient<IDoctorPageService, DoctorPageService>();
            services.AddTransient<IUserPageService, UserPageService>();
            services.AddTransient<IHomePageService, HomePageService>();
            services.AddTransient<IDrugPageService, DrugPageService>();
            services.AddTransient<IProcedurePageService, ProcedurePageService>();
            services.AddTransient<IPositionPageService, PositionPageService>();
            services.AddTransient<IAccountPageService, AccountPageService>();
            services.AddTransient<ISickLeavePageService, SickLeavePageService>();

            services.AddSingleton<IDbService, DbService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
