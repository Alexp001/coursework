using System.Collections.Generic;

namespace BLL.DTO
{
    public class UserRoleDto
    {
        public UserDto User { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
