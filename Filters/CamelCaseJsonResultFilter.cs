using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Majako.Plugin.Common.Filters
{
    public class CamelCaseJsonResultFilter : IAsyncResultFilter
    {
        private readonly JsonSerializerSettings _globalSettings;

        public CamelCaseJsonResultFilter(IOptions<MvcJsonOptions> optionsAccessor)
        {
            _globalSettings = optionsAccessor.Value.SerializerSettings;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var settings = _globalSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            if (context.Result is JsonResult originResult)
            {
                context.Result = new JsonResult(originResult.Value, settings);
            }

            await next();
        }
    }
}
