using System;
using JetBrains.Annotations;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public class TraceScanAsyncQuery
    {
        public TraceScanAsyncQuery(Guid traceId, int batchSize, int limit)
        {
            TraceId = traceId;
            BatchSize = batchSize;
            Limit = limit;
        }

        public Guid TraceId { get; }
        public Guid? ParentSpanId { get; set; }
        public int BatchSize { get; }
        public int Limit { get; }
    }
}