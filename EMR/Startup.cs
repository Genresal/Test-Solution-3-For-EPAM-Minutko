using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Business.Services;
using EMR.Data.Repositories;
using EMR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Serialization;

namespace EMR
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //string conectionString = @"Data Source =.; Database=EMR;Integrated Security = True";

            string conectionString = Configuration.GetConnectionString("EMR");
            string mainConectionString = Configuration.GetConnectionString("MASTER");

            //services.AddSingleton<IConfiguration>(Configuration);

            services.AddTransient<IDbRepository, DbRepository>(provider => new DbRepository(mainConectionString));

            services.AddTransient<IRepository<SickLeave>, SickLeavesRepository>(provider => new SickLeavesRepository(conectionString));
            services.AddTransient<IRepository<Diagnosis>, DiagnosisRepository>(provider => new DiagnosisRepository(conectionString));
            services.AddTransient<IRepository<Drug>, DrugRepository>(provider => new DrugRepository(conectionString));
            services.AddTransient<IRepository<Procedure>, ProcedureRepository>(provider => new ProcedureRepository(conectionString));
            services.AddTransient<IRepository<Role>, RoleRepository>(provider => new RoleRepository(conectionString));
            services.AddTransient<IRepository<User>, UserRepository>(provider => new UserRepository(conectionString));
            services.AddTransient<IRepository<Position>, PositionRepository>(provider => new PositionRepository(conectionString));
            services.AddTransient<IRepository<Doctor>, DoctorsRepository>(provider => new DoctorsRepository(conectionString));
            services.AddTransient<IRepository<Patient>, PatientsRepository>(provider => new PatientsRepository(conectionString));
            services.AddTransient<IRepository<Record>, RecordsRepository>(provider => new RecordsRepository(conectionString));
            services.AddTransient<IRepository<RecordTreatment>, RecordTreatmentsRepository>(provider => new RecordTreatmentsRepository(conectionString));

            services.AddTransient<IBusinessService<Position>, PositionService>();
            services.AddTransient<IBusinessService<Record>, RecordService>();
            services.AddTransient<IBusinessService<Doctor>, DoctorService>();
            services.AddTransient<IBusinessService<Patient>, PatientService>();
            services.AddTransient<ITreatmentService, RecordTreatmentService>();
            services.AddTransient<IBusinessService<Drug>, DrugService>();
            services.AddTransient<IBusinessService<Procedure>, ProcedureService>();

            services.AddTransient<IRecordPageService, RecordPageService>();
            services.AddTransient<IHomePageService, HomePageService>();

            services.AddSingleton<IDbService, DbService>();

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)//, IDbService db)
        {
            var path = AppContext.BaseDirectory;
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");
            });

            //db.CheckDb();
        }
    }
}
