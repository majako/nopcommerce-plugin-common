using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Framework.Controllers;

namespace Majako.Plugin.Misc.Common.BaseClasses
{
   public class JsonController : BaseAdminController
    {
        protected new IActionResult Ok(object data)
        {
            return JsonResult(data, (int)HttpStatusCode.OK);
        }

        private IActionResult JsonResult(object data, int? statusCode)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(data, jsonSerializerSettings),
                ContentType = "application/json",
                StatusCode = statusCode
            };

            return jsonResult;
        }
    }
}
