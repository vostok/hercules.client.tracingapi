using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Transport;
using Vostok.Hercules.Client.Abstractions.Results;
using Vostok.Hercules.Client.TracingApi.Dto;
using Vostok.Logging.Abstractions;

namespace Vostok.Hercules.Client.TracingApi
{
    public class HerculesTracingClient : IHerculesTracingClient
    {
        private const string ServiceName = "Hercules.TracingApi";
        
        private readonly ILog log;
        private IClusterClient client;
        private ResponseAnalyzer responseAnalyzer;

        public HerculesTracingClient(HerculesTracingClientSettings settings, ILog log)
        {
            this.log = log.ForContext<HerculesTracingClient>();
            
            client = new ClusterClient(
                log,
                configuration =>
                {
                    configuration.ClusterProvider = settings.Cluster;
                    configuration.Transport = new UniversalTransport(log);
                    configuration.TargetServiceName = ServiceName;

                    settings.AdditionalSetup?.Invoke(configuration);
                });
            
            responseAnalyzer = new ResponseAnalyzer();
        }
        
        public async Task<ReadTraceResult> ReadAsync(TraceReadQuery query, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = Request.Get("/trace").WithTraceReadQuery(query);
                var result = await client.SendAsync(request, timeout, cancellationToken: cancellationToken).ConfigureAwait(false);

                var status = responseAnalyzer.Analyze(result.Response, out var errorMessage);
                
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