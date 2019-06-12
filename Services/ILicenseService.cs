using Portable.Licensing;

namespace Majako.Plugin.Common.Services
{
    public interface ILicenseService
    {
        License GetValidLicense(string licenseXmlString, string publicKey, string pluginSystemName);
    }
}
