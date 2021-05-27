using System.Collections.Generic;

namespace BLL.Managers
{
    public interface IManager<T> where T: class
    {
        ICollection<T> GetAll();
        T GetById(int id);
        int Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
