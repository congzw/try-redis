using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace Demo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NbSites", Version = "v1" });
            });

            services.AddDemo();

            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //services.AddSingleton<IRedisCacheService, RedisCacheService>();
            //services.AddDbContext<EmployeeContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("EmployeeDB"),
            //        sqlServerOptionsAction: sqlOptions =>
            //        {
            //            sqlOptions.EnableRetryOnFailure();
            //        });
            //});
 
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = $"{Configuration.GetValue<string>("RedisCache:Host")}:{Configuration.GetValue<int>("RedisCache:Port")}";
            //});


            
            //if (_hostEnvironment.IsDevelopment())
            //{
            //    services.AddDistributedMemoryCache();//Use this for only DEV testing purpose
            //}
            //else
            //{
            //    var multiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
            //    {
            //        EndPoints =
            //        {
            //            $"{Configuration.GetValue<string>("RedisCache:Host")}:
            //            {Configuration.GetValue<int>("RedisCache:Port")
            //        }" }
            //    });
            //    services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            //    }
            //}

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //app.UseSwagger();
            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoWebApi v1"));

            //hack virtual app path bugs!!!
            app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "DemoWebApi v1"));
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
