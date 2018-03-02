using Majako.Plugin.Common.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Majako.Plugin.Common
{
    public class NopStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddScoped<CamelCaseJsonResultFilter>();
        }

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public int Order => 2000;
    }
}
