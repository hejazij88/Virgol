using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        VirgolContext context;
        DbSet<T> DbContext;

        public GenericRepository(VirgolContext context)
        {
            this.context = context;
            DbContext = context.Set<T>();
        }
        public bool add(T entity)
        {
            try
            {
                DbContext.Add(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerable<T> GetAll()
        {
            return DbContext.ToList();
        }

        public T GetEntitiy(int Id)
        {
            return DbContext.Find(Id);
        }

        public bool remove(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool remove(int Id)
        {
            try
            {
               T entity= GetEntitiy(Id);
                context.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void save()
        {
            context.SaveChanges();
        }

        public bool Update(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
