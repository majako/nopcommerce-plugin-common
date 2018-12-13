using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.UI;

namespace Majako.Plugin.Common.Extensions
{
    public static class MvcExtensions
    {
        public static T Model<T, TResult>(this IActionResult actionResult) 
            where T : BaseNopModel
            where TResult : ViewResult
        {
            if (!(actionResult is TResult result)) return null;
            return result.Model as T;
        }

        public static T PartialViewResultModel<T, TResult>(this IActionResult actionResult)
            where T : BaseNopModel
            where TResult : PartialViewResult
        {
            if (!(actionResult is TResult result)) return null;
            return result.Model as T;
        }

        public static void AddNotification(this Controller constroller, NotifyType type, string message, bool persistForTheNextRequest)
        {
            var dataKey = $"nop.notifications.{type}";
            if (persistForTheNextRequest)
            {
                if (constroller.TempData[dataKey] == null)
                    constroller.TempData[dataKey] = new List<string>();
                ((List<string>)constroller.TempData[dataKey]).Add(message);
            }
            else
            {
                if (constroller.ViewData[dataKey] == null)
                    constroller.ViewData[dataKey] = new List<string>();
                ((List<string>)constroller.ViewData[dataKey]).Add(message);
            }
        }
    }
}
