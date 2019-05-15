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
        
        [JsonProperty("beginTimestampUtc")]
        public long BeginTimestampUtc;
        
        [JsonProperty("beginTimestampUtcOffset")]
        public int BeginTimestampUtcOffset;
        
        [JsonProperty("endTimestampUtc")]
        public long EndTimestampUtc;
        
        [JsonProperty("endTimestampUtcOffset")]
        public int EndTimestampUtcOffset;
        
        [JsonProperty("annotations")]
        public Dictionary<string, string> Annotations;
    }
}