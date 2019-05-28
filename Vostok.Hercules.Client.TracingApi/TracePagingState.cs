using JetBrains.Annotations;

namespace Vostok.Hercules.Client.TracingApi
{
    [PublicAPI]
    public class TracePagingState
    {
        public static readonly TracePagingState Empty = new TracePagingState(null);

        public TracePagingState(string state)
        {
            State = state;
        }

        [CanBeNull]
        public string State { get; }
    }
}