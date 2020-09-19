using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBaseRepository<T> where T : class
    {

        List<T> GetAll(Expression<Func<T, bool>> expression = null);
        T GetSingle(Expression<Func<T, bool>> expression = null);
        void Insert(T entry);
        void Update(T entry);
        void Delete(T entry);
        IQueryable<T> QueryAll(Expression<Func<T, bool>> expression = null);
    }

    public class BaseRepository<T>:IBaseRepository<T> where T: class
    {
        private EmployeeEntities _context;
        public BaseRepository(EmployeeEntities context)
        {
            _context = context;
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return  _context.Set<T>().ToList();

            }
            return  _context.Set<T>().Where(expression).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> expression = null)
        {
            return  _context.Set<T>().FirstOrDefault(expression);
        }

        public void Insert(T entry)
        {
             _context.Set<T>().Add(entry);
        }

        public void Update(T entry)
        {
            _context.Set<T>().Attach(entry);
            _context.Entry(entry).State = EntityState.Modified;
        }

        public void Delete(T entry)
        {
            _context.Set<T>().Attach(entry);
            _context.Entry(entry).State = EntityState.Modified;
        }

        public IQueryable<T> QueryAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return _context.Set<T>().AsQueryable();

            }
            return _context.Set<T>().Where(expression).AsQueryable();
        }
    }
}
