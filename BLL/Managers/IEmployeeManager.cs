using BLL.DTO;

namespace BLL.Managers
{
    public interface IEmployeeManager
    {
        EmployeeDto GetByUserId(int userId);
    }
}
