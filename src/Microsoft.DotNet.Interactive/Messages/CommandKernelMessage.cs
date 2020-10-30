using Microsoft.DotNet.Interactive.Server;

namespace Microsoft.DotNet.Interactive.Messages
{
    public class CommandKernelMessage : KernelChannelMessage
    {
        public const string MessageLabel = "commands";

        public CommandKernelMessage(IKernelCommandEnvelope commandEnvelope)
            : base(MessageLabel)
        {
            CommandEnvelope = commandEnvelope;
        }

        // Cheating slightly by putting the envelope in here directly. Logically, this should really be
        // a KernelCommand, which we should then swap out for an envelope during serialization, similar
        // to how EventKernelMessage.Event works. However, since commands only ever come into the kernel
        // from the outside (the kernel never sends a command back up to the client) it's slightly
        // simpler to avoid that extra work.
        public IKernelCommandEnvelope CommandEnvelope { get; }
    }
}
