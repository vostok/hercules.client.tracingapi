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
        public string BeginTimestamp;

        [JsonProperty("endTimestamp")]
        public string EndTimestamp;

        [JsonProperty("annotations")]
        public Dictionary<string, string> Annotations;
    }
}