using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public static class HerculesTracingClientExtensions
    {
        [NotNull]
        public static ReadTraceResult Read(
            [NotNull] this IHerculesTracingClient client,
            [NotNull] TraceReadQuery query,
            TimeSpan timeout,
            CancellationToken cancellationToken = default) =>
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

        [NotNull]
        public static async Task<IList<ISpan>> ScanAsync(
            [NotNull] this IHerculesTracingClient client,
            [NotNull] TraceScanAsyncQuery query,
            TimeSpan perRequestTimeout,
            CancellationToken cancellationToken = default)
        {
            var result = new List<ISpan>();
            var state = TracePagingState.Empty;

            while (true)
            {
                var readQuery = new TraceReadQuery(query.TraceId)
                {
                    ParentSpanId = query.ParentSpanId,
                    Limit = query.BatchSize,
                    PagingState = state
                };

                var readPayload = (await client.ReadAsync(readQuery, perRequestTimeout, cancellationToken).ConfigureAwait(false)).Payload;

                result.AddRange(readPayload.Spans);

                if (readPayload.Spans.Count == 0 || readPayload.Next.State == null || result.Count > query.Limit)
                    break;

                state = readPayload.Next;
            }

            return result;
        }
    }
}