using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ViewModel
{
    [Serializable]
    public class EmployeeViewModel
    {
        public long EmployeeID { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string EmployeeCode { get; set; }
        public string Email { get; set; }
    }
}
