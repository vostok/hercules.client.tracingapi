using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Results;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public class ReadTraceResult : HerculesResult<ReadTracePayload>
    {
        public ReadTraceResult(HerculesStatus status, ReadTracePayload payload, [CanBeNull] string errorDetails = null)
            : base(status, payload, errorDetails)
        {
        }
    }
}