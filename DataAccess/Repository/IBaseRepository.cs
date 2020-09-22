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
        List<T> GetAllPaging(int pageNo, int pageSize, Expression<Func<T, long>> order, Expression<Func<T, bool>> condition = null);
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

        public List<T> GetAllPaging(int pageNo, int pageSize, Expression<Func<T, long>> order, Expression<Func<T, bool>> condition = null)
        {
            int startRow = pageNo * pageSize;
            if (condition == null)
            {
                return _context.Set<T>().OrderBy(order).Skip(startRow).Take(pageSize).ToList();

            }
            return _context.Set<T>().Where(condition).OrderBy(order).Skip(startRow).Take(pageSize).ToList();
        }

        public int GetCount(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return _context.Set<T>().Count();

            }
            return _context.Set<T>().Where(expression).Count();
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
