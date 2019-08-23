using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Transport;
using Vostok.Hercules.Client.Abstractions.Results;
using Vostok.Logging.Abstractions;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public class HerculesTracingLegacyClient : IHerculesTracingLegacyClient
    {
        private const string ServiceName = "Hercules.LegacyTracingApi";

        private readonly ILog log;
        private readonly IClusterClient client;

        public HerculesTracingLegacyClient([NotNull] HerculesTracingLegacyClientSettings settings, [CanBeNull] ILog log)
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

        public async Task<HerculesResult<IList<Guid>>> GetTraceIdsAsync(string tracePrefix, TimeSpan timeout)
        {
            try
            {
                var request = Request.Get("/tracing").WithAdditionalQueryParameter("prefix", tracePrefix);

                var result = await client.SendAsync(request, timeout).ConfigureAwait(false);

                var status = ResponseAnalyzer.Analyze(result.Response, out var errorMessage);

                var payload = status == HerculesStatus.Success
                    ? JsonConvert
                        .DeserializeObject<List<string>>(result.Response.Content.ToString())
                        .Select(Guid.Parse)
                        .ToList()
                    : null;

                return new HerculesResult<IList<Guid>>(status, payload, errorMessage);
            }
            catch (Exception error)
            {
                log.Error(error);
                return new HerculesResult<IList<Guid>>(HerculesStatus.UnknownError, null, error.Message);
            }
        }
    }
}