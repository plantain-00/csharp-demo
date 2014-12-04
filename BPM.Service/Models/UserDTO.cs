using System.Collections.Generic;

using Newtonsoft.Json;

namespace BPM.Service.Models
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("realName")]
        public string RealName { get; set; }

        [JsonProperty("departmentName")]
        public string DepartmentName { get; set; }

        [JsonProperty("roles")]
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}