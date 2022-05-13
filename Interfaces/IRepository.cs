using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T> Create(T entity);
        public Task<int> Delete(int EntityID);
        public Task<T> Update(T entity);
        public Task<T> Find(int EntityID);
        public Task<IEnumerable<T>> All();
    }
}
