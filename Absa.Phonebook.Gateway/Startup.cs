using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polly;
using Absa.Phonebook.Gateway.Services;
using Absa.Phonebook.Gateway.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Absa.Phonebook.Gateway
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

            services.Configure<ServiceSettings>(Configuration.GetSection(nameof(ServiceSettings)));

            // services.AddHttpClient<UserServiceEndpointHealthCheck>()
            //     .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
            //     .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(15)));

            services.AddHealthChecks()
                .AddCheck<UserServiceEndpointHealthCheck>("UserService_Endpoint_HealthCheck");

            //services.AddSwaggerGen(options => {
            //    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //    {
            //        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            //        In = ParameterLocation.Header,
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey
            //    });

            //    options.OperationFilter<SecurityRequirementsOperationFilter>();
            //});

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            //                .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //    });

            //var authenticationProviderKey = "TestKey";

            //services.AddAuthentication()
            //    .AddJwtBearer(authenticationProviderKey, x =>
            //    {
            //    });

            services.AddOcelot();
            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //await app.UseAuthentication()
            //         .UseOcelot();

            await app.UseOcelot();
        }
    }
}
