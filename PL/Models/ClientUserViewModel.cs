using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class ClientUserViewModel
    {
        public ClientViewModel ClientObject { get; set; }
        public UserViewModel User { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}