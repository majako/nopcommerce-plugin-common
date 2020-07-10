using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Autofac;
using Autofac.Core;

namespace Majako.Plugin.Common.Infrastructure.Modules
{
    public class HttpClientModule<TService> : Module //TODO: Move this to Majako.Plugin.Common
    {
        private readonly Func<IComponentContext, HttpClient> clientConfigurator;

        public HttpClientModule(Func<IComponentContext, HttpClient> clientConfigurator)
        {
            this.clientConfigurator = clientConfigurator;
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);

            if (registration.Activator.LimitType == typeof(TService))
            {
                registration.Preparing += Prepare;
            }
        }

        private void Prepare(object sender, PreparingEventArgs @event)
        {

            @event.Parameters = @event.Parameters.Union(new[]
            {
                new ResolvedParameter(IsValid, CreateClient)
            });

            bool IsValid(System.Reflection.ParameterInfo parameterInfo, IComponentContext context) => parameterInfo.ParameterType == typeof(HttpClient);

            object CreateClient(System.Reflection.ParameterInfo parameterInfo, IComponentContext context) => clientConfigurator(context); //We can't use IHttpClientFactory because we need to init httpclient to set message handler
        }
    }
}