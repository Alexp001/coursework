using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Managers
{
    public interface IRoleManager
    {
        ICollection<RoleDto> GetAll();
    }
}
