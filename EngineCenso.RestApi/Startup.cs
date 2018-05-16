using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngineCenso.DataAccess;
using EngineCenso.RestApi.Formaters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EngineCenso.RestApi
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
            services.AddTransient<ICensoMappingRepository, CensoMappingMongoRepository>().AddSingleton(new MongoConfig()
            {
                ConnectionString = "mongodb://mongo:27017/CensoEngine",//Configuration.GetSection("Mongo:ConnectionString").Value;
                Database = "CensoMappingModel" // Configuration.GetSection("Mongo:Database").Value;
            }); 
            services.AddMvc(setup =>
            {
                setup.InputFormatters.Insert(0, new PlainTextFormatter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
