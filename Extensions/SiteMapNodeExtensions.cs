using System.Collections.Generic;
using System.Linq;
using Nop.Web.Framework.Menu;

namespace Majako.Plugin.Common.Extensions
{
    public static class SiteMapNodeExtensions
    {
        public static void GeneratePluginAdminMenu(this SiteMapNode rootNode, string pluginName, string systemName, IList<SiteMapNode> nodes)
        {
            var majakoNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName.ToLower() == "majako");

            if (majakoNode == null)
            {
                majakoNode = new SiteMapNode
                {
                    Title = "Majako",
                    SystemName = "majako",
                    Visible = true,
                    IconClass = "fa fa-cloud"
                };

                rootNode.ChildNodes.Insert(rootNode.ChildNodes.Count, majakoNode);
            }

            var pluginsNode = majakoNode.ChildNodes.FirstOrDefault(x => x.SystemName.ToLower() == "majako.plugins");

            if (pluginsNode == null)
            {
                pluginsNode = new SiteMapNode
                {
                    Title = "Plugins",
                    SystemName = "majako.plugins",
                    Visible = true,
                    IconClass = "fa fa-plug"
                };

                majakoNode.ChildNodes.Insert(0, pluginsNode);
            }

            var pluginNode = new SiteMapNode
            {
                Title = pluginName,
                SystemName = systemName.ToLower(),
                Visible = true,
                IconClass = "fa fa-dot-circle-o"
            };

            pluginsNode.ChildNodes.Insert(pluginsNode.ChildNodes.Count, pluginNode);

            var index = 0;
            foreach (var node in nodes)
            {
                pluginNode.ChildNodes.Insert(index, node);
                index++;
            }
        }
    }
}
