﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;
using labfix.netcore.api.mssql.Models;
using Microsoft.EntityFrameworkCore;


namespace labfix.netcore.api.mssql
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
              c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

              var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
              var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
              c.IncludeXmlComments(xmlPath);
            });

            //Connection
            var sqlConnectionString = Configuration.GetConnectionString("NORTHWND");
            services.AddDbContext<NORTHWNDContext> (Options => Options.UseSqlServer(sqlConnectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
              c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


        }
    }
}
