using BLL.DTO;

namespace BLL.Managers
{
    public interface IClientManager
    {
        ClientDto GetByUserId(int id);
    }
}
