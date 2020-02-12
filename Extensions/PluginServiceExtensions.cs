using System.Linq;
using Nop.Services.Plugins;

namespace Majako.Plugin.Common.Extensions
{
    public static class PluginServiceExtensions
    {
        public static bool IsPluginInstalled(this IPluginService pluginService, string pluginSystemName)
        {
            if (pluginService.GetPluginDescriptorBySystemName<IPlugin>(pluginSystemName) == null) return false;
            return true;
        }

        public static bool IsPluginConfiguredForStore(this IPluginService pluginService, string pluginSystemName, int storeId)
        {
            var pluginDescriptor = pluginService.GetPluginDescriptorBySystemName<IPlugin>(pluginSystemName, LoadPluginsMode.All);

            return !pluginDescriptor.LimitedToStores.Any() || pluginDescriptor.LimitedToStores.Contains(storeId);
        }
    }
}
