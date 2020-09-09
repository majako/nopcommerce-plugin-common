using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Majako.Plugin.Common.Abstractions.Factories
{
    public interface IFactory<TFrom, TDestination>
    {
        ValueTask<TDestination> Create(TFrom value, CancellationToken cancellationToken = default);
    }
}
