using System.Linq;
using System.Threading.Tasks;

namespace Demetriux.RepositoryBase
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        void Update(T item);
        T GetById(ulong id);
        void Delete(T item);
        IQueryable<T> List();
        //Async methods
        Task<int> AddAsync(T item);
        Task<T> GetByIdAsync(ulong id);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(T item);
    }
}
