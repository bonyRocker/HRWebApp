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
        private bool _disposed = false;
        private readonly EmployeeEntities _context;
        private TransactionScope Transaction;
        private EmployeeRepository _employeeRepository;

        public UnitOfWork()
        {
            _context= new EmployeeEntities();
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
            if (Transaction == null)
                Transaction = new TransactionScope();
        }
        public void CommitTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Complete();
                this.Transaction.Dispose();
                this.Transaction = null;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

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

    }
}
