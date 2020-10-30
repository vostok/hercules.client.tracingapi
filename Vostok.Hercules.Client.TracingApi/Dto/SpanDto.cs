using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vostok.Hercules.Client.TracingApi.Dto
{
    internal class SpanDto
    {
        [JsonProperty("traceId")]
        public string TraceId;

        [JsonProperty("spanId")]
        public string SpanId;

        [JsonProperty("parentSpanId")]
        public string ParentSpanId;

        [JsonProperty("beginTimestamp")]
        public DateTimeOffset BeginTimestamp;

        [JsonProperty("endTimestamp")]
        public DateTimeOffset EndTimestamp;

        [JsonProperty("annotations")]
        public Dictionary<string, object> Annotations;
    }
}