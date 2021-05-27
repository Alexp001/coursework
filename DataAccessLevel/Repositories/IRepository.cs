using System.Collections.Generic;

namespace DataAccessLevel.Repositories
{
    public interface IRepository<T> where T: class
    {
        List<T> GetAll();
        T GetById(int id);
        int Create(T item);
        void Update(T item);
        void Delete(int id);      
    }
}
