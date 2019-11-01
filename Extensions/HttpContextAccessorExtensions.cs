using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Nop.Services.Messages;

namespace Majako.Plugin.Common.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static void AddNotification(this IHttpContextAccessor httpContextAccessor, NotifyType type, string message)
        {
            var tempDataDictionaryFactory = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ITempDataDictionaryFactory>();
            var tempDataDictionary = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);

            var dataKey = $"nop.notifications.{type}";

            if (tempDataDictionary[dataKey] == null)
                tempDataDictionary[dataKey] = new List<string>();

            var tempData = (List<string>)tempDataDictionary[dataKey];

            if (tempData.All(x => x != message))
            {
                tempData.Add(message);
            }
        }
    }
}
