using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Nop.Web.Framework.Controllers;

namespace Majako.Plugin.Common.Extensions
{
    public static class FilterExtensions
    {
        public static bool IsCallingActionMethod<T>(this ActionExecutedContext context, string actionName, IList<string> httpMethods) where T : Controller
        {
            return context.IsCallingActionMethod<T>(new List<string> {actionName}, httpMethods);
        }

        public static bool IsCallingActionMethod<T>(this ActionExecutedContext context, IList<string> actionNames, string httpMethod) where T : Controller
        {
            return context.IsCallingActionMethod<T>(actionNames, new List<string> { httpMethod });
        }

        public static bool IsCallingActionMethod<T>(this ActionExecutedContext context, string actionName, string httpMethod) where T : Controller
        {
            return context.IsCallingActionMethod<T>(new List<string> { actionName }, new List<string> { httpMethod });
        }

        public static bool IsCallingActionMethod<T>(this ActionExecutedContext context, IList<string> actionNames, IList<string> httpMethods) where T : Controller
        {
            if (!(context.ActionDescriptor is ActionDescriptor actionDescriptor)) return false;

            return actionDescriptor.ControllerDescriptor.ControllerType == typeof(T) &&
                   actionNames.Contains(actionDescriptor.ActionName) &&
                   httpMethods.Contains(context.HttpContext.Request.HttpMethod);
        }

        public static bool IsCallingActionMethod(this ActionExecutedContext context, string controllerName, IList<string> actionNames, IList<string> httpMethods)
        {
            if (!(context.ActionDescriptor is ActionDescriptor actionDescriptor)) return false;

            return actionDescriptor.ControllerDescriptor.ControllerName == controllerName &&
                   actionNames.Contains(actionDescriptor.ActionName) &&
                   httpMethods.Contains(context.HttpContext.Request.HttpMethod);
        }
        //
        // public static bool HasFormValue(this FilterContext context, string formValue)
        // {
        //     return context.HttpContext.Request.Form.Keys.Any(x => x.Equals(formValue, StringComparison.InvariantCultureIgnoreCase));
        // }
        //
        // public static bool HasQueryStringParam(this FilterContext context, string queryStringParam)
        // {
        //     return context.HttpContext.Request.Query.Keys.Any(x => x.Equals(queryStringParam, StringComparison.InvariantCultureIgnoreCase));
        // }
        //
        // public static string GetQueryStringValue(this FilterContext context, string queryStringParam)
        // {
        //     return context.HttpContext.Request.Query[queryStringParam];
        // }
        //
        // public static string GetFormValue(this FilterContext context, string formValue)
        // {
        //     if (context.HasFormValue(formValue))
        //     {
        //         return context.HttpContext.Request.Form[formValue];
        //     }
        //
        //     return null;
        // }
        //
        // public static string GetQueryValue(this FilterContext context, string key)
        // {
        //     if (context.HttpContext.Request.Query.Any(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)))
        //     {
        //         return context.HttpContext.Request.Query[key];
        //     }
        //
        //     return null;
        // }

        // public static bool IsValidForRequest(this ActionExecutedContext context, string[] submitButtonNames, FormValueRequirement requirement = FormValueRequirement.Equal, bool validateNameOnly = false)
        // {
        //     if (context.HttpContext.Request.Method != WebRequestMethods.Http.Post) return false;
        //
        //     foreach (var buttonName in submitButtonNames)
        //     {
        //         try
        //         {
        //             switch (requirement)
        //             {
        //                 case FormValueRequirement.Equal:
        //                     {
        //                         if (validateNameOnly)
        //                         {
        //                             //"name" only
        //                             if (context.HttpContext.Request.Form.Keys.Any(x => x.Equals(buttonName, StringComparison.InvariantCultureIgnoreCase)))
        //                                 return true;
        //                         }
        //                         else
        //                         {
        //                             //validate "value"
        //                             //do not iterate because "Invalid request" exception can be thrown
        //                             string value = context.HttpContext.Request.Form[buttonName];
        //                             if (!string.IsNullOrEmpty(value))
        //                                 return true;
        //                         }
        //                     }
        //                     break;
        //                 case FormValueRequirement.StartsWith:
        //                     {
        //                         if (validateNameOnly)
        //                         {
        //                             //"name" only
        //                             if (context.HttpContext.Request.Form.Keys.Any(x => x.StartsWith(buttonName, StringComparison.InvariantCultureIgnoreCase)))
        //                                 return true;
        //                         }
        //                         else
        //                         {
        //                             //validate "value"
        //                             foreach (var formValue in context.HttpContext.Request.Form.Keys)
        //                                 if (formValue.StartsWith(buttonName, StringComparison.InvariantCultureIgnoreCase))
        //                                 {
        //                                     var value = context.HttpContext.Request.Form[formValue];
        //                                     if (!string.IsNullOrEmpty(value))
        //                                         return true;
        //                                 }
        //                         }
        //                     }
        //                     break;
        //             }
        //         }
        //         catch (Exception exc)
        //         {
        //             //try-catch to ensure that no exception is throw
        //             Debug.WriteLine(exc.Message);
        //         }
        //     }
        //     return false;
        // }
    }
}
