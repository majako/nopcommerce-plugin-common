using System.Threading.Tasks;

namespace Majako.Plugin.Common.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string executingFilePath, string viewPath, object model);
    }
}