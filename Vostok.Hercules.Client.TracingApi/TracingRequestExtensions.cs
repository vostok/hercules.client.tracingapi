using Vostok.Clusterclient.Core.Model;

namespace Vostok.Hercules.Client.TracingApi
{
    internal static class TracingRequestExtensions
    {
        public static Request WithTraceReadQuery(this Request request, TraceReadQuery query)
        {
            return request
                .WithAdditionalQueryParameter("traceId", query.TraceId)
                .WithAdditionalQueryParameter("parentSpanId", query.ParentSpanId)
                .WithAdditionalQueryParameter("limit", query.Limit)
                .WithAdditionalQueryParameter("pagingState", query.PagingState.State);
        }
    }
}