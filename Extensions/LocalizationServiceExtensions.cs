using System;
using System.IO;
using System.Linq;
using Nop.Core.Data;
using Nop.Core.Domain.Localization;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;

namespace Majako.Plugin.Common.Extensions
{
    public static class LocalizationServiceExtensions
    {
        public static void InstallXmlLocaleResources(this ILocalizationService localizationService, string culture, string pluginSystemName)
        {
            var nopFileProvider = EngineContext.Current.Resolve<INopFileProvider>();
            var languageService = EngineContext.Current.Resolve<ILanguageService>();

            var languages = languageService.GetAllLanguages().Where(x => string.Equals(x.LanguageCulture, culture, StringComparison.CurrentCultureIgnoreCase));

            foreach (var language in languages)
            {
                foreach (var filePath in Directory.EnumerateFiles(nopFileProvider.MapPath($"~/Plugins/{pluginSystemName}/Resources"), $"Resources.{culture}.xml", SearchOption.TopDirectoryOnly))
                {
                    var localesXml = File.ReadAllText(filePath);
                    localizationService.ImportResourcesFromXml(language, localesXml);
                }
            }
        }

        public static void UninstallLocaleResources(this ILocalizationService localizationService, string pluginSystemName)
        {
            var localStringResourcesRepository = EngineContext.Current.Resolve<IRepository<LocaleStringResource>>();

            var resources = localStringResourcesRepository.Table.Where(x => x.ResourceName.Contains(pluginSystemName)).ToList();
            foreach (var resource in resources)
            {
                localizationService.DeletePluginLocaleResource(resource.ResourceName);
            }
        }
    }
}
