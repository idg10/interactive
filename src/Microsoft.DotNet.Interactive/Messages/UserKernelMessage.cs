using Newtonsoft.Json.Linq;

namespace Microsoft.DotNet.Interactive.Messages
{
    public class UserKernelMessage : KernelChannelMessage
    {
        public UserKernelMessage(
            string label,
            JToken payload)
            : base(label)
        {
            Payload = payload;
        }

        public JToken Payload { get; }
    }
}
