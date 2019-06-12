using System.Linq;
using Nop.Core.Plugins;
using Nop.Services.Plugins;

namespace Majako.Plugin.Common.Extensions
{
    public static class PluginFinderExtensions
    {
        public static bool IsPluginInstalled(this IPluginFinder pluginFinder, string pluginSystemName)
        {
            if (pluginFinder.GetPluginDescriptorBySystemName(pluginSystemName) == null) return false;
            return true;
        }

        public static bool IsPluginConfiguredForStore(this IPluginFinder pluginFinder, string pluginSystemName, int storeId)
        {
            var pluginDescriptor = pluginFinder.GetPluginDescriptorBySystemName(pluginSystemName, LoadPluginsMode.All);

            return !pluginDescriptor.LimitedToStores.Any() || pluginDescriptor.LimitedToStores.Contains(storeId);
        }
    }
}
