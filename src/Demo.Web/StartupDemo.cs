using Demo.Web.AppServices;
using Demo.Web.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Web
{
    public static class StartupDemo
    {
        public static void AddDemo(this IServiceCollection services)
        {
            services.AddSingleton(DemoDb.Create());
            services.AddTransient<IDemoAppService, DemoAppService>();
            services.AddTransient<IMyCacheService, MyCacheService>();

            services.AddStackExchangeRedisCache(options =>
            {
                //options.Configuration = $"{Configuration.GetValue<string>("RedisCache:Host")}:{Configuration.GetValue<int>("RedisCache:Port")}";
                options.Configuration = "localhost:6379";
            });
        }
    }
}
