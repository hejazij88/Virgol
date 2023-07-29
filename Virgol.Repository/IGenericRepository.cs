using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgol.Repository
{
    public interface IGenericRepository<T>:IDisposable
    {
        IEnumerable<T> GetAll();

       T GetEntitiy(int Id);
        bool add(T entity);
        bool remove(T entity);
        bool remove(int Id);
        bool Update(T entity);

        void save();





    }
}
