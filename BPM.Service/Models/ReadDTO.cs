using System;

using Newtonsoft.Json;

namespace BPM.Service.Models
{
    public class ReadDTO
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("dendDate")]
        public DateTime SendDate { get; set; }

        [JsonProperty("processId")]
        public string ProcessID { get; set; }

        [JsonProperty("senderName")]
        public string SenderName { get; set; }
    }
}