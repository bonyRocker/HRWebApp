using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DataAccess.Repository;

namespace BusinessLogic
{
    public class EmployeeBLL
    {
        private UnitOfWork _oUnitOfWork;
        public EmployeeBLL()
        {
            _oUnitOfWork = new UnitOfWork();
        }
        public List<EmployeeViewModel> GetAllEmployee()
        {
            try
            {
                List<EmployeeViewModel> empVMList = new List<EmployeeViewModel>();
                var employees = _oUnitOfWork.employeeRepository.GetAll(x=>x.IsActive==true);
                if (employees.Count > 0)
                {
                    foreach (var employee in employees)
                    {
                        EmployeeViewModel empVM = new EmployeeViewModel();

                        empVM.EmployeeID = employee.EmployeeID;
                        empVM.Name = employee.Name;
                        empVM.PhoneNo = employee.PhoneNo;
                        empVM.Gender = employee.Gender;
                        empVM.Address = employee.Address;
                        empVM.EmployeeCode = employee.EmployeeCode;
                        empVM.Email = employee.Email;
                       

                        empVMList.Add(empVM);
                    }
                }

                return empVMList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EmployeeViewModel> GetAllEmployee(int pageNo,int pageSize)
        {
            try
            {
                List<EmployeeViewModel> empVMList = new List<EmployeeViewModel>();
                var employees = _oUnitOfWork.employeeRepository.GetAllWithPagination(pageNo, pageSize, x => x.EmployeeID, x => x.IsActive == true);
                if (employees.Count > 0)
                {
                    foreach (var employee in employees)
                    {
                        EmployeeViewModel empVM = new EmployeeViewModel();

                        empVM.EmployeeID = employee.EmployeeID;
                        empVM.Name = employee.Name;
                        empVM.PhoneNo = employee.PhoneNo;
                        empVM.Gender = employee.Gender;
                        empVM.Address = employee.Address;
                        empVM.EmployeeCode = employee.EmployeeCode;
                        empVM.Email = employee.Email;


                        empVMList.Add(empVM);
                    }
                }

                return empVMList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string InsertEmployee(EmployeeViewModel empVm)
        {

            try
            {
                Employee objEmployee = new Employee();
                objEmployee.Name = empVm.Name;
                objEmployee.EmployeeCode = empVm.EmployeeCode;
                objEmployee.Email = empVm.Email;
                objEmployee.PhoneNo = empVm.PhoneNo;
                objEmployee.Address = empVm.Address;
                objEmployee.Gender = empVm.Gender;
                objEmployee.PhotoByte = empVm.PhotoByte;
                objEmployee.PhotoPath = empVm.PhotoPath;
                objEmployee.CreatedBy = "Admin";
                objEmployee.CreatedOn = DateTime.Now;
                objEmployee.IsActive = true;

                _oUnitOfWork.employeeRepository.Insert(objEmployee);
               _oUnitOfWork.Save();
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public EmployeeViewModel GetEmployeeById(long _employeeId)
        {
            try
            {
                var employee = _oUnitOfWork.employeeRepository.GetSingle(x => x.EmployeeID == _employeeId && x.IsActive==true);
                if (employee != null)
                {
                    EmployeeViewModel empvm=new EmployeeViewModel();
                    empvm.EmployeeID = employee.EmployeeID;
                    empvm.Name = employee.Name;
                    empvm.PhoneNo = employee.PhoneNo;
                    empvm.Gender = employee.Gender;
                    empvm.Address = employee.Address;
                    empvm.EmployeeCode = employee.EmployeeCode;
                    empvm.Email = employee.Email;
                    empvm.PhotoPath = employee.PhotoPath;
                    empvm.PhotoByte = employee.PhotoByte;

                    return empvm;
                }

                return null;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public string UpdateEmployee(EmployeeViewModel empVm)
        {
            try
            {
                Employee objEmployee = _oUnitOfWork.employeeRepository.GetSingle(x => x.EmployeeID == empVm.EmployeeID);
                objEmployee.Name = empVm.Name;
                objEmployee.EmployeeCode = empVm.EmployeeCode;
                objEmployee.Email = empVm.Email;
                objEmployee.PhoneNo = empVm.PhoneNo;
                objEmployee.Address = empVm.Address;
                objEmployee.Gender = empVm.Gender;
                objEmployee.UpdatedBy = "Admin";
                objEmployee.UpdatedOn = DateTime.Now;
                objEmployee.PhotoByte = empVm.PhotoByte;
                objEmployee.PhotoPath = empVm.PhotoPath;

                _oUnitOfWork.employeeRepository.Update(objEmployee);
                _oUnitOfWork.Save();
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object DeleteEmplyee(long empId)
        {
            try
            {
                var employee = _oUnitOfWork.employeeRepository.GetSingle(x => x.EmployeeID == empId && x.IsActive==true);
                if (employee != null)
                {
                    employee.IsActive = false;
                    _oUnitOfWork.employeeRepository.Delete(employee);
                    _oUnitOfWork.Save();
                }

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetEmployeeCount()
        {
            return _oUnitOfWork.employeeRepository.GetCount(x=>x.IsActive==true);
        }

        public List<EmployeeViewModel> GetAllEmployeeByName(string name)
        {
            List<EmployeeViewModel> empVMList = new List<EmployeeViewModel>();
            var employees = _oUnitOfWork.employeeRepository.GetAll(x => x.IsActive==true && x.Name.Contains(name));
            if (employees.Count > 0)
            {
                foreach (var employee in employees)
                {
                    EmployeeViewModel empVM = new EmployeeViewModel();

                    empVM.EmployeeID = employee.EmployeeID;
                    empVM.Name = employee.Name;
                    empVM.PhoneNo = employee.PhoneNo;
                    empVM.Gender = employee.Gender;
                    empVM.Address = employee.Address;
                    empVM.EmployeeCode = employee.EmployeeCode;
                    empVM.Email = employee.Email;


                    empVMList.Add(empVM);
                }
            }

            return empVMList;
        }

        public List<EmployeeViewModel> GetAllEmployeeByEmpCode(string empCode)
        {
            List<EmployeeViewModel> empVMList = new List<EmployeeViewModel>();
            var employees = _oUnitOfWork.employeeRepository.GetAll(x => x.IsActive == true && x.EmployeeCode.Contains(empCode));
            if (employees.Count > 0)
            {
                foreach (var employee in employees)
                {
                    EmployeeViewModel empVM = new EmployeeViewModel();

                    empVM.EmployeeID = employee.EmployeeID;
                    empVM.Name = employee.Name;
                    empVM.PhoneNo = employee.PhoneNo;
                    empVM.Gender = employee.Gender;
                    empVM.Address = employee.Address;
                    empVM.EmployeeCode = employee.EmployeeCode;
                    empVM.Email = employee.Email;


                    empVMList.Add(empVM);
                }
            }
            return empVMList;
        }

        public List<EmployeeViewModel> GetAllEmployeeByPhoneNo(string phone)
        {
            List<EmployeeViewModel> empVMList = new List<EmployeeViewModel>();
            var employees = _oUnitOfWork.employeeRepository.GetAll(x => x.IsActive == true && x.PhoneNo.Contains(phone));
            if (employees.Count > 0)
            {
                foreach (var employee in employees)
                {
                    EmployeeViewModel empVM = new EmployeeViewModel();

                    empVM.EmployeeID = employee.EmployeeID;
                    empVM.Name = employee.Name;
                    empVM.PhoneNo = employee.PhoneNo;
                    empVM.Gender = employee.Gender;
                    empVM.Address = employee.Address;
                    empVM.EmployeeCode = employee.EmployeeCode;
                    empVM.Email = employee.Email;


                    empVMList.Add(empVM);
                }
            }
            return empVMList;
        }

        public bool IsEmployeeExist(string empCode)
        {
            return _oUnitOfWork.employeeRepository.IsEmployeeExist(empCode, "");

        }
    }
}
