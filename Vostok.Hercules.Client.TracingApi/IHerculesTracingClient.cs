using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public interface IHerculesTracingClient
    {
        [NotNull]
        Task<ReadTraceResult> ReadAsync(
            TraceReadQuery query,
            TimeSpan timeout,
            CancellationToken cancellationToken = default);
    }
}