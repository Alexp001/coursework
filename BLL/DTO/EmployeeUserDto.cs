using System.Collections.Generic;

namespace BLL.DTO
{
    public class EmployeeUserDto
    {
        public EmployeeDto EmployeeObject { get; set; }
        public UserDto User { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
