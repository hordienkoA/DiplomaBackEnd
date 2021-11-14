using System.Reflection;
using Diploma.DependencyInjection;
using EFCoreConfiguration.Models.Contexts;
using MediatR;

namespace Diploma
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
            services.AddSqlServerCustom(Configuration.GetConnectionString("DefaultConnection"));
            services.AddLocalizationCustom();
            services.AddIdentityCustom();
            services.AddServicesCustom();
            services.AddRepositories();
            services.AddAutomapperCustom();
            services.AddMvcCustom();
            services.AddAuthenticationCustom(Configuration);
            services.AddSwaggerCustom();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diploma v1"));
            }
            app.ApplyMigrations(context);
            app.ConfigureLocalizationCustom();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCorsCustom();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
