using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vostok.Hercules.Client.TracingApi.Dto
{
    internal class SpanDto
    {
        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }

        [JsonPropertyName("spanId")]
        public string SpanId { get; set; }

        [JsonPropertyName("parentSpanId")]
        public string ParentSpanId { get; set; }

        [JsonPropertyName("beginTimestamp")]
        public DateTimeOffset BeginTimestamp { get; set; }

        [JsonPropertyName("endTimestamp")]
        public DateTimeOffset EndTimestamp { get; set; }

        [JsonPropertyName("annotations")]
        public Dictionary<string, object> Annotations { get; set; }
    }
}