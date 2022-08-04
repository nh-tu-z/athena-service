using Newtonsoft.Json;

namespace AthenaService.CollectorCommunication.Message
{
    public class BaseMessage
    {
        [JsonProperty(PropertyName = "messageTypeId")]
        public int MessageTypeId { get; set; }
        [JsonProperty(PropertyName = "itemId")]
        public Guid ItemId { get; set; }
        [JsonProperty(PropertyName = "tokenId")]
        public Guid TokenId { get; set; }
        [JsonProperty(PropertyName = "tenantId")]
        public int TenantId { get; set; }
    }
}
