using System;
using System.Linq;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Hercules.Client.TracingApi.Dto;

namespace Vostok.Hercules.Client.TracingApi.Tests
{
    [TestFixture]
    internal class SpanDtoConverter_Tests
    {
        [Test]
        public void Should_convert_to_ISpan_correctly_when_all_fields_are_present()
        {
            const string spanJson = "{" +
                                    "\"traceId\":\"1a2b3c4d-9bec-40b0-839b-cc51e2abcdef\"," +
                                    "\"spanId\":\"7a99a678-def0-4567-abad-ba7fc38ffa13\"," +
                                    "\"parentSpanId\":\"abcdef12-acde-4675-9322-f96cc1234567\"," +
                                    "\"beginTimestamp\":\"2019-04-22T13:15:33.913000000+05:00\"," +
                                    "\"endTimestamp\":\"2019-04-22T13:15:34.013000000+05:00\"," +
                                    "\"annotations\":{\"SomeString\":\"Some brilliant string\",\"SomeInt\":42}" +
                                    "}";
            var spanDto = JsonSerializer.Deserialize<SpanDto>(spanJson, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

            var expectedSpan = new Span
            {
                TraceId = Guid.Parse("1a2b3c4d-9bec-40b0-839b-cc51e2abcdef"),
                SpanId = Guid.Parse("7a99a678-def0-4567-abad-ba7fc38ffa13"),
                ParentSpanId = Guid.Parse("abcdef12-acde-4675-9322-f96cc1234567"),
                BeginTimestamp = new DateTimeOffset(2019, 4, 22, 13, 15, 33, 913, 5.Hours()),
                EndTimestamp = new DateTimeOffset(2019, 4, 22, 13, 15, 34, 013, 5.Hours())
            };

            var span = SpanDtoConverter.ConvertToSpan(spanDto);
            span.Should().BeEquivalentTo(expectedSpan, config => config.Excluding(s => s.Annotations));

            span.Annotations["SomeString"].ToString().Should().Be("Some brilliant string");
            span.Annotations["SomeInt"].ToString().Should().Be("42");
        }

        [Test]
        public void Should_convert_to_ISpan_correctly_when_parentSpanId_is_absent()
        {
            const string spanJson = "{" +
                                    "\"traceId\":\"1a2b3c4d-9bec-40b0-839b-cc51e2abcdef\"," +
                                    "\"spanId\":\"7a99a678-def0-4567-abad-ba7fc38ffa13\"," +
                                    "\"beginTimestamp\":\"2019-04-22T13:15:33.913000000+05:00\"," +
                                    "\"endTimestamp\":\"2019-04-22T13:15:34.013000000+05:00\"," +
                                    "\"annotations\":{\"SomeKey\":\"Some brilliant string\"}" +
                                    "}";
            var spanDto = JsonSerializer.Deserialize<SpanDto>(spanJson, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

            var expectedSpan = new Span
            {
                TraceId = Guid.Parse("1a2b3c4d-9bec-40b0-839b-cc51e2abcdef"),
                SpanId = Guid.Parse("7a99a678-def0-4567-abad-ba7fc38ffa13"),
                BeginTimestamp = new DateTimeOffset(2019, 4, 22, 13, 15, 33, 913, 5.Hours()),
                EndTimestamp = new DateTimeOffset(2019, 4, 22, 13, 15, 34, 013, 5.Hours())
            };

            var span = SpanDtoConverter.ConvertToSpan(spanDto);
            span.Should().BeEquivalentTo(expectedSpan, config => config.Excluding(s => s.Annotations));

            span.Annotations["SomeKey"].ToString().Should().Be("Some brilliant string");
        }
    }
}