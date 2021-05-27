using System;
using System.Collections.Generic;

namespace PL.Models
{
    [Serializable]
    public class UserRoleViewModel
    {
        public UserViewModel User { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}