using cookhatAPI.Connection;
using cookhatAPI.DAL;
using cookhatAPI.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace cookhatAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            //string connectionstring = Configuration.GetConnectionString("SQLConnectionString-Prod");
            services.AddDbContext<cookhatDBContext>(db =>
              db.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString")));
            //services.AddTransient<IDbConnectionFactory>(db => new SqlConnectionFactory(
            //   Environment.GetEnvironmentVariable("SqlConnectionString")));
            services.AddTransient<IDbConnectionFactory>(db => new SqlConnectionFactory(
               Configuration.GetConnectionString("SQLConnectionString")));
            services.AddTransient<IRecipe, RecipeDAL>();
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                builder.WithOrigins("https://davaras.azurewebsites.net").AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddTransient<IChef, ChefDAL>();
            services.AddTransient<IIngredient, IngredientDAL>();
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("ApiCorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
