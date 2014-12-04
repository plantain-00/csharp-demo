using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace BPM.Service.Models
{
    public class DepartmentDTO
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("parentId")]
        public string ParentID { get; set; }

        [JsonProperty("departments")]
        public ICollection<DepartmentDTO> Departments { get; set; }

        [JsonProperty("parentDepartmentName")]
        public string ParentDepartmentName { get; set; }

        [JsonProperty("users")]
        public IEnumerable<UserDTO> Users { get; set; }
        [JsonProperty("roles")]
        public IEnumerable<RoleDTO> Roles { get; set; }

        public List<string> GetChildDepartments()
        {
            var result = new List<string>();
            if (Departments.Count != 0)
            {
                result.AddRange(Departments.Select(d => d.ID));
                foreach (var department in Departments)
                {
                    result.AddRange(department.GetChildDepartments());
                }
            }
            return result;
        }

        public List<string> GetUsers()
        {
            var result = new List<string>();
            if (Users.Count() != 0)
            {
                result.AddRange(Users.Select(d => d.ID));
            }
            foreach (var department in Departments)
            {
                result.AddRange(department.GetUsers());
            }
            return result;
        }
    }
}