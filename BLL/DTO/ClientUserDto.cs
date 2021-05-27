using System.Collections.Generic;

namespace BLL.DTO
{
    public class ClientUserDto
    {
        public ClientDto ClientObject { get; set; }
        public UserDto User { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
