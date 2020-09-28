using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EmployeeRepository:BaseRepository<Employee>
    {
        private EmployeeEntities _context;
        public EmployeeRepository(EmployeeEntities context) : base(context)
        {
            _context = new EmployeeEntities();
        }

        public bool IsEmployeeExist(string empcode,string ssn)
        {
            var employee=_context.Employee.FirstOrDefault(x => x.EmployeeCode == empcode);
            if (employee==null )
            {
                return false;
            }
            return true;
        }
    }
}
