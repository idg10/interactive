using Microsoft.DotNet.Interactive.Events;

namespace Microsoft.DotNet.Interactive.Messages
{
    public class EventKernelMessage : KernelChannelMessage
    {
        public const string MessageLabel = "kernelEvents";

        public EventKernelMessage(
            KernelEvent kernalEvent)
            : base(MessageLabel)
        {
            Event = kernalEvent;
        }

        public KernelEvent Event { get; }
    }
}
