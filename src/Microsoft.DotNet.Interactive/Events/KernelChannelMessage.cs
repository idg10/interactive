namespace Microsoft.DotNet.Interactive.Events
{
    public class KernelChannelMessage : KernelEvent
    {
        public KernelChannelMessage(string channelName, string type, string content)
        {
            ChannelName = channelName;
            Type = type;
            Content = content;
        }

        public string ChannelName { get; }
        public string Type { get; }
        public string Content { get; }
    }
}
