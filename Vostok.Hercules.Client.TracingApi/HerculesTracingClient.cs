using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Transport;
using Vostok.Hercules.Client.Abstractions.Results;
using Vostok.Hercules.Client.TracingApi.Dto;
using Vostok.Logging.Abstractions;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public class HerculesTracingClient : IHerculesTracingClient
    {
        private const string ServiceName = "Hercules.TracingApi";

        private readonly ILog log;
        private IClusterClient client;

        public HerculesTracingClient([NotNull] HerculesTracingClientSettings settings, [CanBeNull] ILog log)
        {
            settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.log = log = (log ?? LogProvider.Get()).ForContext<HerculesTracingClient>();

            client = new ClusterClient(
                log,
                configuration =>
                {
                    configuration.ClusterProvider = settings.Cluster;
                    configuration.Transport = new UniversalTransport(log);
                    configuration.TargetServiceName = ServiceName;

                    settings.AdditionalSetup?.Invoke(configuration);
                });
        }

        public async Task<ReadTraceResult> ReadAsync(TraceReadQuery query, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = Request.Get("/trace").WithTraceReadQuery(query);
                var result = await client.SendAsync(request, timeout, cancellationToken: cancellationToken).ConfigureAwait(false);

                var status = ResponseAnalyzer.Analyze(result.Response, out var errorMessage);

                var payload = default(ReadTracePayload);
                if (status == HerculesStatus.Success)
                    payload = CreateReadTracePayload(JsonConvert.DeserializeObject<TraceResponseDto>(result.Response.Content.ToString()));

                return new ReadTraceResult(status, payload, errorMessage);
            }
            catch (Exception error)
            {
                log.Error(error);
                return new ReadTraceResult(HerculesStatus.UnknownError, default, error.Message);
            }
        }

        private ReadTracePayload CreateReadTracePayload(TraceResponseDto responseDto)
        {
            return new ReadTracePayload(responseDto.Result.Select(SpanDtoConverter.ConvertToSpan).ToList(), new TracePagingState(responseDto.PagingState));
        }
    }
}