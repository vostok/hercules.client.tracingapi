using System;
using System.Linq;
using Vostok.Commons.Time;
using Vostok.Tracing.Abstractions;

namespace Vostok.Hercules.Client.TracingApi.Dto
{
    internal static class SpanDtoConverter
    {
        public static ISpan ConvertToSpan(SpanDto spanDto)
        {
            return new Span
            {
                TraceId = Guid.Parse(spanDto.TraceId),
                SpanId = Guid.Parse(spanDto.SpanId),
                ParentSpanId = spanDto.ParentSpanId != null ? Guid.Parse(spanDto.ParentSpanId) : default(Guid?),
                BeginTimestamp = new DateTimeOffset(DateTime.Parse(spanDto.BeginTimestamp)),
                EndTimestamp = spanDto.EndTimestamp == null
                    ? (DateTimeOffset?)null
                    : new DateTimeOffset(DateTime.Parse(spanDto.EndTimestamp)),
                Annotations = spanDto.Annotations.ToDictionary(pair => pair.Key, pair => (object)pair.Value)
            };
        }
    }
}