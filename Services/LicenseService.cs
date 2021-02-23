using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Majako.Plugin.Common.Infrastructure;
using Nop.Core;
using Nop.Core.Domain.Logging;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using Portable.Licensing;
using Portable.Licensing.Validation;

namespace Majako.Plugin.Common.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILogger _logger;
        private readonly ISettingService _settingService;

        public LicenseService(
            ILogger logger,
            ISettingService settingService
            )
        {
            _logger = logger;
            _settingService = settingService;
        }

        public License GetValidLicense(string licenseXmlString, string publicKey, string pluginSystemName)
        {
            try
            {
                var license = License.Load(licenseXmlString);

                var validationFailures = license.Validate()
                    .Signature(publicKey)
                    .AssertValidLicense()
                    .ToList();

                var sb = new StringBuilder();

                foreach (var failure in validationFailures)
                {
                    sb.AppendLine(failure.GetType().Name + ": " + failure.Message + " - " + failure.HowToResolve);
                }

                if (validationFailures.Any())
                {
                    if (sb.Length > 0)
                    {
                        _logger.InsertLog(LogLevel.Warning, $"Invalid license for plugin: {pluginSystemName}", sb.ToString());
                    }
                    return null;   
                }

                if (license.AdditionalAttributes.Get(Constants.LicenseAttributeNames.NopCommerceVersion) != NopVersion.CurrentVersion)
                {
                    sb.AppendLine("Invalid License for this nopCommerce version. Please get a valid license: https://www.majako.net");
                }

                if (license.AdditionalAttributes.Get(Constants.LicenseAttributeNames.License) != "multiple domain")
                {
                    var host = HttpContext.Current.Request.Url.Host;

                    var currentHost = host.Replace("www.", "");
                    var licensedHost = license.AdditionalAttributes.Get("store url");

                    if (!string.Equals(currentHost, licensedHost, StringComparison.InvariantCultureIgnoreCase) && currentHost != "localhost")
                    {
                        sb.AppendLine($"Invalid License for current host {currentHost}. Licensed host is {licensedHost}. Please get a valid license: https://www.majako.net");
                    }
                }

                if (sb.Length > 0)
                {
                    _logger.InsertLog(LogLevel.Warning, $"Invalid license for plugin: {pluginSystemName}", sb.ToString());
                    return null;
                }
            
                return license;
            }
            catch (Exception e)
            {
                // ref: https://github.com/majako/skapa/issues/2#issuecomment-769647163
                // _logger.Error("Error validating license", e);
                return null;
            }
        }
        
        public async Task<string> DownloadLicense(string signature)
        {
            if(string.IsNullOrEmpty(signature)) throw  new ArgumentNullException(nameof(signature));

            var licenseDomainSetting = _settingService.GetSetting("Majako.Plugin.Common.LicenseDomain");
            var licenseDomain = licenseDomainSetting == null ? "https://www.majako.net" : licenseDomainSetting.Value;

            using (var client = new HttpClient())
            {
                // TODO make domain a setting
                var response = await client.GetAsync($"{licenseDomain}/Plugins/Licensing/GetLicense?signature={WebUtility.UrlEncode(signature)}");

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
