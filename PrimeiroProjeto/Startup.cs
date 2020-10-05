using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrimeiroProjeto.Models.Contexto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using PrimeiroProjeto.Controllers;

namespace PrimeiroProjeto
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _env = environment.EnvironmentName;
        }

        public IConfiguration Configuration { get; }
        private string _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<Contexto>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //string conexaoBD = "uXGbWDg7/n963vf+jNSVTjRa1Fz3OcDAShfba4b8afoGhrq3NFy4b2iyJU1g5BKjZ7GkhfMm8jnllAi134WskahvafHpo6HuS8upl7W9TqO5CbUBi8ZYIv1XYt/9Se5JxY/NRq3zY/IL1bmby0jpvuAIpakmQQ1tfUf6aiuWfGlZgVHfDS7rM45Pm5sBIQPodJBmwTcUu8THL+rUGXC/oadBTBV2QvmQmjcEky7WYDI=";
            //var builder = new SqlConnectionStringBuilder(SecurityController.Decrypt(conexaoBD, _env));
            //services.AddDbContext<Contexto>(options => options.UseSqlServer(builder.ConnectionString));/
            services.AddDbContext<Contexto>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<Contexto>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuarios}/{action=Index}/{id?}");
            });
        }
    }
}
