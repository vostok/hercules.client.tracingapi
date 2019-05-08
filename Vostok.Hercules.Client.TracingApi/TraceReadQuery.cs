using System;
using JetBrains.Annotations;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public class TraceReadQuery
    {
        public TraceReadQuery(Guid traceId)
        {
            TraceId = traceId;
        }

        public Guid TraceId { get; set; }
        public Guid? ParentSpanId { get; set; }
        public int? Limit { get; set; }
        public TracePagingState PagingState { get; set; }
    }
}