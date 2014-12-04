using System.Collections.Generic;

using BPM.Data;

using Newtonsoft.Json;

namespace BPM.Service.Models
{
    public class RoleDTO
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permissions")]
        public IEnumerable<Permission> Permissions { get; set; }
    }
}