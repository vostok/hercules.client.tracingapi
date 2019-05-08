using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vostok.Hercules.Client.TracingApi
{
    public class HerculesTracingClient : IHerculesTracingClient
    {
        public Task<ReadTraceResult> ReadAsync(TraceReadQuery query, TimeSpan timeout, CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();
    }
}