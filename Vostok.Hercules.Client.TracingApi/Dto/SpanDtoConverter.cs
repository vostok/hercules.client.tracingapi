using System;
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
                ParentSpanId = spanDto.ParentSpanId == null ? default(Guid?) : Guid.Parse(spanDto.ParentSpanId),
                BeginTimestamp = spanDto.BeginTimestamp,
                EndTimestamp = spanDto.EndTimestamp == null ? (DateTimeOffset?)null : spanDto.EndTimestamp,
                Annotations = spanDto.Annotations
            };
        }
    }
}