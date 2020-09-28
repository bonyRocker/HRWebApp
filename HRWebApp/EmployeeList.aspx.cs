using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using HRWebApp.Utility;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;
using BusinessLogic.ViewModel;
using Utility.Helper;


namespace HRWebApp
{
    public partial class EmployeeList : BasePage
    {
        private static EmployeeBLL _serviceEmployee;

        protected void Page_Load(object sender, EventArgs e)
        {
            _serviceEmployee = new EmployeeBLL();
            HideAlert();
            //_serviceEmployee = new EmployeeBLL();

            if (!IsPostBack)
            {
                Session["CurrentPage"] = 0;
                Session["SortDirection"] = SortDirection.Ascending;
            }

        }


        [WebMethod(EnableSession = true)]
        public static object LoadEmployeeList(string name = "", string empcode = "", string phoneno = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
                int employeescount = 0;
                int pageno = 0;
                if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(empcode) || !string.IsNullOrEmpty(phoneno))
                {

                    if (!string.IsNullOrEmpty(name))
                    {
                        employees = _serviceEmployee.GetAllEmployeeByName(name);

                    }
                    else if (!string.IsNullOrEmpty(empcode))
                    {
                        employees = _serviceEmployee.GetAllEmployeeByEmpCode(empcode);

                    }
                    else if (!string.IsNullOrEmpty(phoneno))
                    {
                        employees = _serviceEmployee.GetAllEmployeeByPhoneNo(phoneno);
                    }

                    employeescount = employees.Count;
                    pageno = jtStartIndex / jtPageSize;
                    int startRow = pageno * jtPageSize;
                    employees = employees.Skip(startRow).Take(jtPageSize).ToList();
                    employees = SortEmployee(employees, jtSorting);
                    return new { Result = "OK", Records = employees, TotalRecordCount = employeescount };
                }
                else
                {


                    //Get data from database
                    employeescount = _serviceEmployee.GetEmployeeCount();
                    pageno = jtStartIndex / jtPageSize;
                    employees = _serviceEmployee.GetAllEmployee(pageno, jtPageSize);
                    employees = SortEmployee(employees, jtSorting);

                    //Return result to jTable
                    return new { Result = "OK", Records = employees, TotalRecordCount = employeescount };
                }
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        

        public static List<EmployeeViewModel> SortEmployee(List<EmployeeViewModel> list, string sortexpression)
        {
            if (string.IsNullOrEmpty(sortexpression))
            {
                return list;
            }
            string[] sort = sortexpression.Split(' ');
            SortDirection sortDirection = SortDirection.Ascending;

            string sortExpression = sort[0];
            string sortdirection = sort[1];
            if (sortdirection == "ASC")
            {
                sortDirection = SortDirection.Ascending;
            }
            else
            {
                sortDirection = SortDirection.Descending;
            }

            List<EmployeeViewModel> employees = list;//(List<EmployeeViewModel>)ViewState["EmployeeList"];//If you store an object in session state, that object must be serializable.


            if (employees != null)
            {
                employees = SortColumns<EmployeeViewModel>(employees, sortExpression, sortDirection);

            }
            return employees;
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeDetails.aspx");
        }

        [WebMethod]
        public static string UpdateEmployee(string empid)
        {
            if (string.IsNullOrEmpty(empid))
            {
                return "Default.aspx";
            }
            long employeeId = Convert.ToInt64(empid);
            return "EmployeeDetails.aspx?employeeId=" + employeeId;
        }

        [WebMethod]
        public static string DeleteEmployee(string empid)
        {
            var id = Convert.ToInt64(empid);
            if (id > 0)
            {
                return "found";
            }
            return "not found";
        }

        protected void btnDeleteConfim_Click(object sender, EventArgs e)
        {
            try
            {
                lblModalMsg.Text = string.Empty;
                var empId = Convert.ToInt64(hdnEmployeeId.Value);
                _serviceEmployee.DeleteEmplyee(empId);
                SetSuccessMessage("Data Deleted Successfully");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnDeleteCancel_Click(object sender, EventArgs e)
        {
            try
            {
                lblModalMsg.Text = string.Empty;
                return;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}