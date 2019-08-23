using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Results;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public interface IHerculesTracingLegacyClient
    {
        Task<HerculesResult<IList<Guid>>> GetTraceIdsAsync(string tracePrefix, TimeSpan timeout);
    }
}