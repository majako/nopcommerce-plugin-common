using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Logging;
using Nop.Services.Logging;
using Portable.Licensing;
using Portable.Licensing.Validation;

namespace Majako.Plugin.Common.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LicenseService(
            ILogger logger,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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

                if (validationFailures.Any()) return null;

                if (license.Expiration < DateTime.UtcNow)
                {
                    sb.AppendLine("License expired. Please update license: https://www.majako.net");
                }

                if (license.AdditionalAttributes.Get("nopcommerce-version") != NopVersion.CurrentVersion)
                {
                    sb.AppendLine("Invalid License for this nopCommerce version. Please get a valid license: https://www.majako.net");
                }

                if (license.AdditionalAttributes.Get("multi-store") != "true")
                {
                    var host = _httpContextAccessor.HttpContext.Request.Host;

                    var currentHost = host.Host.Replace("www.", "");
                    var licensedHost = license.AdditionalAttributes.Get("host");

                    if (licensedHost != currentHost && currentHost != "localhost")
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
                _logger.Error("Error validating license", e);

                return null;
            }
        }
    }
}
