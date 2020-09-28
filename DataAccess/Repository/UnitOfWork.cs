using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly EmployeeEntities _context;
        private TransactionScope _transaction;
        private bool _disposed = false;

        private EmployeeRepository _employeeRepository;

        public UnitOfWork()
        {
            _context = new EmployeeEntities();
        }


        #region Initialize Repositories

        public EmployeeRepository employeeRepository
        {
            get
            {
                return _employeeRepository == null ? new EmployeeRepository(_context) : _employeeRepository;
            }
        }

        #endregion


        #region Operation

        public void BeginTrnsaction()
        {
            if (_transaction == null)
                _transaction = new TransactionScope();
        }
        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Complete();
                _transaction.Dispose();
                _transaction = null;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        #region GarbageCollction
        public void Dispose()
        {
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _context.Dispose();
            }

            _disposed = true;
        }
        #endregion
    }
}
