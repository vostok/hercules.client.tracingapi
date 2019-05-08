using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Queries;
using Vostok.Tracing.Abstractions;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public class ReadTracePayload
    {
        public ReadTracePayload(
            [NotNull] IList<ISpan> spans,
            [NotNull] TracePagingState next)
        {
            Spans = spans ?? throw new ArgumentNullException(nameof(spans));
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Events read from the timeline.
        /// </summary>
        [NotNull]
        public IList<ISpan> Spans { get; }

        /// <summary>
        /// <para>Coordinates for the next sequential read (if the requested time range was not completely consumed).</para>
        /// <para>Put them in a <see cref="ReadTimelineQuery"/> to continue reading from this point.</para>
        /// <para>Don't use them for reads with a different time range from the one used in this operation!</para>
        /// </summary>
        [NotNull]
        public TracePagingState Next { get; }
    }
}