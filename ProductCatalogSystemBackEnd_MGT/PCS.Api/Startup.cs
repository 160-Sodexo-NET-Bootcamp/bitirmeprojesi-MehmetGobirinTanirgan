using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PCS.Api.Extensions;
using PCS.Database.Context.Concrete;
using PCS.Mapping.Mappings;
using PCS.Validation.Validators.UserTypeValidators;

[assembly: ApiController]
namespace PCS.Api
{
    public class Startup
    {
        readonly string pcsCorsPolicy = "PcsCorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureSettings(Configuration);
            services.ConfigureApiBehaviorOptions();
            services.AddHealthChecks();
            services.AddJwtAuthentication(Configuration);
            services.AddHttpContextAccessor();
            services.AddDbContext<PcsDbContext>();
            services.AddAutoMapper(typeof(UserMappings));
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserCreateDtoValidator>());
            services.AddCorsWithOptions(pcsCorsPolicy);
            services.AddRedisCache(Configuration);
            services.AddControllersWithLoopIgnore();
            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PCS.Api v1"));
            }

            app.UseDefaultHealthChecks();

            app.UseHttpsRedirection();

            app.UseGlobalExceptionHandler();

            app.UseRouting();

            app.UseCors(pcsCorsPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
