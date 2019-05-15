using System;
using JetBrains.Annotations;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Topology;

namespace Vostok.Hercules.Client.TracingApi
{
    /// <summary>
    /// Represents configuration of <see cref="HerculesTracingClient"/>.
    /// </summary>
    [PublicAPI]
    public class HerculesTracingClientSettings
    {
        public HerculesTracingClientSettings([NotNull] IClusterProvider cluster)
        {
            Cluster = cluster ?? throw new ArgumentNullException(nameof(cluster));
        }

        /// <summary>
        /// <para>An <see cref="IClusterProvider"/> implementation that provides replicas of Hercules management API service.</para>
        /// </summary>
        [NotNull]
        public IClusterProvider Cluster { get; }

        /// <summary>
        /// <para>An optional delegate that can be used to tune underlying <see cref="IClusterClient"/> instance.</para>
        /// </summary>
        [CanBeNull]
        public ClusterClientSetup AdditionalSetup { get; set; }
    }
}