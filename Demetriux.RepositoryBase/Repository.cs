using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demetriux.RepositoryBase
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        private readonly DbContext context;

        public DbContext Context { get { return this.context; } }

        private Int32 Save()
        {
            Int32 res = 0;
            try
            {
                res = this.Context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return res;
        }

        private Task<Int32> SaveAsync()
        {
            return this.Context.SaveChangesAsync();
        }

        public Repository(DbContext context)
        {
            if (context == null)
                throw new ArgumentException("The parameter context passed is null or invalid!");
            
            this.context = context;
        }

        public void Add(T item)
        {
            this.Context.Set<T>().Add(item);
            this.Save();
        }

        public void Update(T item)
        {
            this.Context.Entry<T>(item).State = EntityState.Modified;
            this.Save();
        }

        public T GetById(ulong id)
        {
            return this.Context.Set<T>().Find(id);
        }

        public void Delete(T item)
        {
            this.Context.Set<T>().Remove(item);
            this.Save();
        }

        public IQueryable<T> List()
        {
            return this.Context.Set<T>();
        }

        //Async methods
        public Task<int> AddAsync(T item)
        {
            this.Context.Set<T>().Add(item);
            return this.SaveAsync();
        }

        public Task<T> GetByIdAsync(ulong id)
        {
            return this.Context.Set<T>().FindAsync(id);
        }

        public Task<int> UpdateAsync(T item)
        {
            this.Context.Entry<T>(item).State = EntityState.Modified;
            return this.SaveAsync();
        }

        public Task<int> DeleteAsync(T item)
        {
            this.Context.Set<T>().Remove(item);
            return this.SaveAsync();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
