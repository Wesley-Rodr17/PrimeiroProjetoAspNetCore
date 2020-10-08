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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            

        }

        public IConfiguration Configuration { get; }
        //private string _env { get; }

        //EnvironmentVariableTarget.Machine
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            Este é um explemplo de como chamar a variavel de ambiente do seu computador para a aplicação 
            ASPNETCORE_ConnectionStrings__DefaultConnection => este é o nome dado a variavel de ambiente
            EnvironmentVariableTarget.Machine => indica que ele deve procurar a variavel na maquina
            */

            string conexao = Environment.GetEnvironmentVariable("ASPNETCORE_ConnectionStrings__DefaultConnection", EnvironmentVariableTarget.Machine);
            services.AddDbContext<Contexto>(options =>
              options.UseSqlServer(Configuration.GetConnectionString(conexao)));
            

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
