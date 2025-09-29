using System.Text.Json.Serialization;

namespace Vostok.Hercules.Client.TracingApi.Dto
{
    internal class TraceResponseDto
    {
        [JsonPropertyName("result")]
        public SpanDto[] Result { get; set; }

        [JsonPropertyName("pagingState")]
        public string PagingState { get; set; }
    }
}