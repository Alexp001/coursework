using System.Collections.Generic;

namespace PL.Models
{
    public class EmployeeUserViewModel
    {
        public EmployeeViewModel EmployeeObject { get; set; }
        public UserViewModel User { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}