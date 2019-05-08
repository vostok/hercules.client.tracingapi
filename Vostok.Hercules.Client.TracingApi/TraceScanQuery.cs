using System;
using JetBrains.Annotations;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public class TraceScanQuery
    {
        public TraceScanQuery(Guid traceId)
        {
            TraceId = traceId;
        }

        public Guid TraceId { get; set; }
        public Guid? ParentSpanId { get; set; }
        public int BatchSize { get; set; }
    }
}