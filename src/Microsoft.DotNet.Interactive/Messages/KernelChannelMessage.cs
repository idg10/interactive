using Microsoft.DotNet.Interactive.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.DotNet.Interactive.Messages
{
    public abstract class KernelChannelMessage
    {
        protected KernelChannelMessage(string label)
        {
            Label = label;
        }

        public string Label { get; }

        public static KernelChannelMessage Deserialize(string json)
        {
            var jsonObject = JObject.Parse(json);

            return Deserialize(jsonObject);
        }

        internal static KernelChannelMessage Deserialize(JToken json)
        {
            string label = json["label"].Value<string>();
            KernelChannelMessage result = label switch
            {
                CommandKernelMessage.MessageLabel => new CommandKernelMessage(KernelCommandEnvelope.Deserialize(json["payload"])),
                _ when label.StartsWith("user/") => new UserKernelMessage(label, json["payload"]),
                _ => null
            };
            return result;
        }

        public static string Serialize(KernelChannelMessage channelMessage)
        {
            if (channelMessage is EventKernelMessage eventMessage)
            {
                var envelopeModel = KernelEventEnvelope.SerializeToModel(eventMessage.Event);
                return JsonConvert.SerializeObject(
                    new { label = channelMessage.Label, payload = envelopeModel },
                    Serializer.JsonSerializerSettings);
            }

            return JsonConvert.SerializeObject(
                channelMessage,
                Serializer.JsonSerializerSettings);
        }
    }
}
