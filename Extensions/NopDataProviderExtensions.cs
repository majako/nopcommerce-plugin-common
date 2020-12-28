using System.Text;
using Nop.Core.Infrastructure;
using Nop.Data;

namespace Majako.Plugin.Common.Extensions
{
    public static class NopDataProviderExtensions
    {
        public static void ExecuteNonQueryFromFile(this INopDataProvider dataProvider, string filePath)
        {
            var fileProvider = EngineContext.Current.Resolve<INopFileProvider>();
            filePath = fileProvider.MapPath(filePath);
            if (!fileProvider.FileExists(filePath))
                return;

            dataProvider.ExecuteNonQuery(fileProvider.ReadAllText(filePath, Encoding.Default));
        }
    }
}