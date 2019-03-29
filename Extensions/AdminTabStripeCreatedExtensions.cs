using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Extensions;

namespace Majako.Plugin.Common.Extensions
{
    public static class AdminTabStripeCreatedExtensions
    {
        public static void AddTab(this AdminTabStripCreated adminTabStripCreatedEvent, string tabStripName, string tabTitle, string viewName, object model)
        {
            var tabContent = adminTabStripCreatedEvent.Helper.Partial(viewName, model)
                .RenderHtmlContent()
                .Replace("</script>", "<\\/script>"); //we need escape a closing script tag to prevent terminating the script block early
            
            var tabId = Guid.NewGuid();
            var unishopProductTab = new HtmlString($@"
                <script type='text/javascript'>
                    $(document).ready(function() {{
                        $(`
                            <li>
                                <a data-tab-name='{tabId}' data-toggle='tab' href='#{tabId}'>
                                    {tabTitle}
                                </a>
                            </li>
                        `).appendTo('#{tabStripName} .nav-tabs:first');
                        $(`
                            <div class='tab-pane' id='{tabId}'>
                                {tabContent}
                            </div>
                        `).appendTo('#{tabStripName} .tab-content:first');
                    }});
                </script>");

            adminTabStripCreatedEvent.BlocksToRender.Add(unishopProductTab);
        }
    }
}
