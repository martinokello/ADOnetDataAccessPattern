using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Data.Access.Interfaces
{
    public interface IRepository<TKey, T>
    {
        T GetById(TKey key);
        T[] GetAll();
        bool Add(T instance);
        bool Delete(TKey key);
        bool Update(T instance);

    }
}
