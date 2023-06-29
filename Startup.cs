using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAnnotations;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using vcar.Core;
using vcar.Persistance;
using vcar.Core.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
namespace vcar
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<VcarContext>(o => o.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Vcar;Trusted_Connection=True;"));
            // services.AddDbContext<VcarContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Local")));
            // services.AddDbContext<VcarContext>(o => o.UseMySQL(Configuration.GetConnectionString("Remote")));

            services.Configure<PhotoSettings>(Configuration.GetSection("PhotosSettings"));
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IFileStorage, FileStorage>();
            services.AddAutoMapper();
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(options =>
                  {
                      options.AddDefaultPolicy(builder =>
                                      {
                                          builder.WithHeaders("authorization");
                                          builder.AllowAnyMethod();
                                          builder.AllowAnyOrigin();
                                      });
                  });

            services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer(options =>
           {
               options.Authority = Configuration["AuthCredentials:Authority"];
               options.Audience = Configuration["AuthCredentials:Audience"];
           });



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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();
            app.UseCors();


            app.Use((context, next) =>
            {
                if (context.Request.Headers.Any(k => k.Key.Contains("Origin")) && context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                    return context.Response.WriteAsync("handled");
                }

                return next.Invoke();
            });


            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


            app.UseSpa(spa =>
                      {
                          // To learn more about options for serving an Angular SPA from ASP.NET Core,
                          // see https://go.microsoft.com/fwlink/?linkid=864501

                          spa.Options.SourcePath = "ClientApp";

                          if (env.IsDevelopment())
                          {
                              spa.UseAngularCliServer(npmScript: "start");
                          }
                      });

        }
    }
}

