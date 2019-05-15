using Newtonsoft.Json;

namespace Vostok.Hercules.Client.TracingApi.Dto
{
    internal class TraceResponseDto
    {
        [JsonProperty("result")]
        public SpanDto[] Result;
        
        [JsonProperty("pagingState")]
        public string PagingState;
    }
}