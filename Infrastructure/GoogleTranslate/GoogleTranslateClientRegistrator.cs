using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Autofac;
using Majako.Plugin.Common.Abstraction.GoogleTranslate;
using Majako.Plugin.Common.Extensions;
using Majako.Plugin.Common.Infrastructure.Modules;

namespace Majako.Plugin.Common.Infrastructure.GoogleTranslate
{
    public static class GoogleTranslateClientRegistrator
    {
        public static void AddGoogleTranslateClient(this ContainerBuilder source)
        {
            source.NotNull(nameof(source));
            source.RegisterType<GoogleTranslateClient>()
                .As<IGoogleTranslateClient>()
                .InstancePerLifetimeScope();
            source.RegisterModule(new HttpClientModule<GoogleTranslateClient>(pptions =>
            {
                var result = new HttpClient();
                result.BaseAddress = new Uri("https://translate.googleapis.com");
                return result;
            }));
        }
    }
}
