using System.Text;
using AutoMapper;
using EngineCenso.DataAccess;
using EngineCenso.RestApi.Filters;
using EngineCenso.RestApi.Formaters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            services.AddScoped<LoggingActionFilter>();

            services.AddSingleton(new JwtConfig() { SignKey = Configuration.GetSection("Jwt:SignKey").Value });
            services.AddSingleton<IHashingAlgorithm, Pbkdf2Hashing>();
            services.AddTransient<IEngineCensoContext, EngineCensoContext>().AddSingleton(new MongoConfig()
            {
                ConnectionString = Configuration.GetSection("Mongo:ConnectionString").Value,
                Database = Configuration.GetSection("Mongo:Database").Value
            });
            services.AddTransient<ICensoMappingRepository, CensoMappingMongoRepository>();
            services.AddTransient<IUserProvider, MongoUserProvider>();

            services.AddAutoMapper();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:SignKey").Value)),
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
                    };
                });

            
            services.AddMvc(setup =>
            {
                setup.InputFormatters.Insert(0, new PlainTextFormatter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
