using CatsyCatStudio.Data;
using CatsyCatStudio.Interface;
using CatsyCatStudio.Models;
using CatsyCatStudio.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CatsyCatStudio
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options=> 
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<StoreUser, IdentityRole>(cfg=> {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication().AddCookie().AddJwtBearer(cfg => {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _configuration["Tokens:Issuer"],
                    ValidAudience = _configuration["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]))
                };
            });

            services.AddHttpContextAccessor();
            services.AddSession();

            services.AddScoped<ShoppingCart>(sp=> ShoppingCart.GetCart(sp));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<Seeder>();
            services.AddControllersWithViews();
            //.AddJsonOptions(o => o.JsonSerializerOptions
               // .ReferenceHandler = ReferenceHandler.Preserve);
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
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default", 
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
