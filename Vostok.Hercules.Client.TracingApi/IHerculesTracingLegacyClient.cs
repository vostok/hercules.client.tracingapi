using System;
using System.Threading.Tasks;
using Vostok.Hercules.Client.Abstractions.Results;

namespace Vostok.Hercules.Client.TracingApi
{
    public interface IHerculesTracingLegacyClient
    {
        Task<HerculesResult<Guid[]>> GetTraceIdsAsync(string tracePrefix, TimeSpan timeout);
    }
}