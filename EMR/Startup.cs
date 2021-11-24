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
using System.Text.Json.Serialization;

namespace EMR
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfigurationRoot Configuration { get; set; }

        public Startup(Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //string conectionString = @"Data Source =.; Database=EMR;Integrated Security = True";

            string conectionString = Configuration.GetConnectionString("Database");

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddTransient<IDbRepository, DbRepository>(provider => new DbRepository(conectionString));

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

            services.AddTransient<IRecordService, RecordService>();
            services.AddTransient<IDoctorService, DoctorService>();

            services.AddTransient<IRecordPageService, RecordPageService>();

            services.AddSingleton<IDbService, DbService>();

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbService db)
        {
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

            db.CheckDb();
        }
    }
}
