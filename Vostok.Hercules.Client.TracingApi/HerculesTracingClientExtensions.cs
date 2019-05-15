using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public static class HerculesTracingClientExtensions
    {
        [NotNull]
        public static ReadTraceResult Read(
            this IHerculesTracingClient client,
            TraceReadQuery query,
            TimeSpan timeout,
            CancellationToken cancellationToken=default) =>
            client.ReadAsync(query, timeout, cancellationToken).GetAwaiter().GetResult();
        
        [NotNull]
        public static IEnumerable<ISpan> Scan(
            [NotNull] this IHerculesTracingClient client,
            [NotNull] TraceScanQuery query,
            TimeSpan perRequestTimeout,
            CancellationToken cancellationToken = default)
        {
            var state = TracePagingState.Empty;

            while (true)
            {
                var readQuery = new TraceReadQuery(query.TraceId)
                {
                    ParentSpanId = query.ParentSpanId,
                    Limit = query.BatchSize,
                    PagingState = state
                };

                var readPayload = client.Read(readQuery, perRequestTimeout, cancellationToken).Payload;

                if (readPayload.Spans.Count == 0)
                    break;
                
                foreach (var @event in readPayload.Spans)
                    yield return @event;

                if (readPayload.Next.State == null)
                    break;

                state = readPayload.Next;
            }
        }
    }
}