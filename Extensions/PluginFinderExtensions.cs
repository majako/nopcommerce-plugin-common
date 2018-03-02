using Nop.Core.Plugins;

namespace Majako.Plugin.Common.Extensions
{
    public static class PluginFinderExtensions
    {
        public static bool IsPluginInstalled(this IPluginFinder pluginFinder, string pluginSystemName)
        {
            if (pluginFinder.GetPluginDescriptorBySystemName(pluginSystemName) == null) return false;
            return true;
        }
    }
}
