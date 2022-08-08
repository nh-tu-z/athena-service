using Newtonsoft.Json;

namespace AthenaService.CollectorCommunication.Message
{
    public class SocketMessage : BaseMessage
    {
        [JsonProperty(PropertyName = "actionTypeId")]
        public int ActionTypeId { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; } = string.Empty;
    }
}
