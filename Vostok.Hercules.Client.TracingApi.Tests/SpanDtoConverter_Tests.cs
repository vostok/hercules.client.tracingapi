using System;
using System.Collections.Generic;
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
            var spanDto = new SpanDto
            {
                TraceId = "1a2b3c4d-9bec-40b0-839b-cc51e2abcdef",
                SpanId = "7a99a678-def0-4567-abad-ba7fc38ffa13",
                ParentSpanId = "abcdef12-acde-4675-9322-f96cc1234567",
                BeginTimestampUtc = 1555920933913,
                EndTimestampUtc = 1555920934013,
                BeginTimestampUtcOffset = 18000,
                EndTimestampUtcOffset = 18000,
                Annotations = new Dictionary<string, string>
                {
                    ["SomeKey"] = "Some brilliant string"
                }
            };

            var expectedSpan = new Span
            {
                TraceId = Guid.Parse("1a2b3c4d-9bec-40b0-839b-cc51e2abcdef"),
                SpanId = Guid.Parse("7a99a678-def0-4567-abad-ba7fc38ffa13"),
                ParentSpanId = Guid.Parse("abcdef12-acde-4675-9322-f96cc1234567"),
                BeginTimestamp = new DateTimeOffset(2019, 4, 22, 13, 15, 33, 913, 5.Hours()),
                EndTimestamp = new DateTimeOffset(2019, 4, 22, 13, 15, 34, 013,5.Hours()),
                Annotations = new Dictionary<string, object>
                {
                    ["SomeKey"] = "Some brilliant string"
                }
            };
            
            SpanDtoConverter.ConvertToSpan(spanDto).Should().BeEquivalentTo(expectedSpan);
        }
        
        [Test]
        public void Should_convert_to_ISpan_correctly_when_parentSpanId_is_absent()
        {
            var spanDto = new SpanDto
            {
                TraceId = "1a2b3c4d-9bec-40b0-839b-cc51e2abcdef",
                SpanId = "7a99a678-def0-4567-abad-ba7fc38ffa13",
                BeginTimestampUtc = 1555920933913,
                EndTimestampUtc = 1555920934013,
                BeginTimestampUtcOffset = 18000,
                EndTimestampUtcOffset = 18000,
                Annotations = new Dictionary<string, string>
                {
                    ["SomeKey"] = "Some brilliant string"
                }
            };

            var expectedSpan = new Span
            {
                TraceId = Guid.Parse("1a2b3c4d-9bec-40b0-839b-cc51e2abcdef"),
                SpanId = Guid.Parse("7a99a678-def0-4567-abad-ba7fc38ffa13"),
                BeginTimestamp = new DateTimeOffset(2019, 4, 22, 13, 15, 33, 913, 5.Hours()),
                EndTimestamp = new DateTimeOffset(2019, 4, 22, 13, 15, 34, 013,5.Hours()),
                Annotations = new Dictionary<string, object>
                {
                    ["SomeKey"] = "Some brilliant string"
                }
            };
            
            SpanDtoConverter.ConvertToSpan(spanDto).Should().BeEquivalentTo(expectedSpan);
        }
    }
}