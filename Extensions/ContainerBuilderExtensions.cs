using System;
using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using Nop.Core;
using Nop.Core.Data;
using Nop.Data;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Majako.Plugin.Common.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterPluginDataContextAndRegisterType<TContext, TEntity>
            (this ContainerBuilder builder, string contextName) 
            where TContext : DbContext, IDbContext
            where TEntity : BaseEntity
        {
            builder.RegisterPluginDataContext<TContext>(contextName);
            builder.RegisterType<EfRepository<TEntity>>()
                .As<IRepository<TEntity>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(contextName))
                .InstancePerLifetimeScope();
            
        }
    }
}