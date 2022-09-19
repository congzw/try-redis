using Demo.Web.AppServices;
using Demo.Web.Domain;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Demo.Web
{
    public static class StartupDemo
    {
        public static void AddDemo(this IServiceCollection services)
        {
            services.AddSingleton(DemoDb.Create());
            services.AddTransient<IDemoAppService, DemoAppService>();
            services.AddTransient<IMyCacheService, MyCacheService>();
            
            //demo for RedisCache
            services.AddStackExchangeRedisCache(options =>
            {
                //options.Configuration = $"{Configuration.GetValue<string>("RedisCache:Host")}:{Configuration.GetValue<int>("RedisCache:Port")}";
                options.Configuration = "localhost:6379";
            });
        }

        
        public static void AddDemo2(this IServiceCollection services)
        {
            services.AddSingleton(DemoDb.Create());
            services.AddTransient<IDemoAppService, DemoAppService>();
            services.AddTransient<IMyCacheService, MyCacheService2>();
            
            //If you have a master/replica setup then specify the connection using the below approach. Redis will identify the master automatically and establish the connection.
            //ConnectionMultiplexer.Connect("ServerA:6379,ServerB:6379, ServerC:6379");
            var multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        }
    }
}
