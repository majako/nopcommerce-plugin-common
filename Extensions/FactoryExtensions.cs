using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Majako.Plugin.Common.Abstractions.Factories;

namespace Majako.Plugin.Common.Extensions
{
    public static class FactoryExtensions
    {
        public static void AddFactory<TFactory, TFrom, TDestination>(this ContainerBuilder source) where TFactory : IFactory<TFrom, TDestination>
        {
            source.RegisterType<TFactory>()
                .As<IFactory<TFrom, TDestination>>()
                .InstancePerLifetimeScope();
        }
    }
}
