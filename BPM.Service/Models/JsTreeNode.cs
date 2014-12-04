using System;
using System.Collections.Generic;
using System.Linq;

using BPM.Data;

using Newtonsoft.Json;

namespace BPM.Service.Models
{
    public class JsTreeNode
    {
        public JsTreeNode(string id, string text)
        {
            ID = id;
            Text = text;
            Children = new List<JsTreeNode>();
        }

        public JsTreeNode(Department department)
        {
            ID = department.ID;
            Text = department.Name;
            Children = new List<JsTreeNode>();
            foreach (var d in department.Departments)
            {
                Children.Add(new JsTreeNode(d));
            }
            foreach (var user in department.Users)
            {
                var userNode = new JsTreeNode(user.ID, user.Name)
                               {
                                   Icon = "Images/user.png"
                               };
                Children.Add(userNode);
                foreach (var role in user.Roles)
                {
                    var roleNode = new JsTreeNode(Guid.NewGuid().ToString(), role.Name)
                                   {
                                       Icon = "Images/role.png"
                                   };
                    userNode.Children.Add(roleNode);
                    foreach (var permissionClass in role.GetPermissionClasses())
                    {
                        var permissionClassNode = new JsTreeNode(Guid.NewGuid().ToString(), permissionClass);
                        roleNode.Children.Add(permissionClassNode);
                        var allPermission = Protocols.AllPermissions[permissionClass];
                        foreach (var permission in role.RolePermissions.Select(rp => rp.Permission).Where(allPermission.Contains))
                        {
                            permissionClassNode.Children.Add(new JsTreeNode(Guid.NewGuid().ToString(), Protocols.DisplayedPermissions[permission])
                                                             {
                                                                 Icon = "Images/permission.png"
                                                             });
                        }
                    }
                }
            }
            foreach (var role in department.Roles)
            {
                var roleNode = new JsTreeNode(Guid.NewGuid().ToString(), role.Name)
                               {
                                   Icon = "Images/role.png"
                               };
                Children.Add(roleNode);
                foreach (var permissionClass in role.GetPermissionClasses())
                {
                    var permissionClassNode = new JsTreeNode(Guid.NewGuid().ToString(), permissionClass);
                    roleNode.Children.Add(permissionClassNode);
                    var allPermission = Protocols.AllPermissions[permissionClass];
                    foreach (var permission in role.RolePermissions.Select(rp => rp.Permission).Where(allPermission.Contains))
                    {
                        permissionClassNode.Children.Add(new JsTreeNode(Guid.NewGuid().ToString(), Protocols.DisplayedPermissions[permission])
                                                         {
                                                             Icon = "Images/permission.png"
                                                         });
                    }
                }
            }
        }

        [JsonProperty("children")]
        public List<JsTreeNode> Children { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("state")]
        public JsTreeNodeState State { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class JsTreeNodeState
    {
        public JsTreeNodeState(bool opened = false, bool disabled = false, bool selected = false)
        {
            Opened = opened;
            Disabled = disabled;
            Selected = selected;
        }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("opened")]
        public bool Opened { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }
}