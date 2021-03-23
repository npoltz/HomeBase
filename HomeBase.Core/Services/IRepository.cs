using System.Collections.Generic;

namespace HomeBase.Core.Services
{
    public interface IRepository<T>
    {
        public IList<T> Get(int? take);
        public T Get(string id);
        public T Create(T obj);
        public void Update(string id, T obj);
        public void Remove(T obj);
        public void Remove(string id);
    }
}