using Newtonsoft.Json;

namespace AthenaService.CollectorCommunication.Message
{
    public class CollectorMessage : BaseMessage
    {
        [JsonProperty(PropertyName = "state")]
        public int State { get; set; }
    }
}
