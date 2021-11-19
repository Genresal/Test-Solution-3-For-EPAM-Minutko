using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electronic_Medical_Record.Models;
using System.Text.Json.Serialization;
using Electronic_Medical_Record.Repositories;

namespace Electronic_Medical_Record
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string conectionString = @"Data Source =.; Database=ElectronicMedicalRecord;Integrated Security = True";
            services.AddTransient(provider => new RecordsRepository(conectionString));
            services.AddTransient<IRepository<Doctor>, DoctorsRepository>(provider => new DoctorsRepository(conectionString));
            services.AddTransient<IRepository<Patient>, PatientsRepository>(provider => new PatientsRepository(conectionString));
            services.AddTransient<IRepository<SickLeave>, SickLeavesRepository>(provider => new SickLeavesRepository(conectionString));
            services.AddTransient<IRepository<Treatment>, TreatmentsRepository>(provider => new TreatmentsRepository(conectionString));

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

                //endpoints.MapRazorPages();
            });
        }
    }
}
