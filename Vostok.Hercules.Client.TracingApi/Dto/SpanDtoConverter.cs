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
                BeginTimestamp = CreateDateTimeOffset(spanDto.BeginTimestampUtc, spanDto.BeginTimestampUtcOffset),
                EndTimestamp = CreateDateTimeOffset(spanDto.EndTimestampUtc, spanDto.EndTimestampUtcOffset),
                Annotations = spanDto.Annotations.ToDictionary(pair => pair.Key, pair => (object)pair.Value)
            };
        }

        private static DateTimeOffset CreateDateTimeOffset(long utcTimestamp, int utcOffset)
        {
            var dateTime = EpochHelper.FromUnixTimeMilliseconds(utcTimestamp);
            var offset = TimeSpan.FromSeconds(utcOffset);

            return new DateTimeOffset(DateTime.SpecifyKind(dateTime + offset, DateTimeKind.Unspecified), offset);
        }
    }
}