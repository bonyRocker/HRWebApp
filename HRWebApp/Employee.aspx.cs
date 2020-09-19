using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using HRWebApp.Utility;


namespace HRWebApp
{
    public partial class Employee : BasePage
    {
        EmployeeBLL _serviceEmployee = new EmployeeBLL();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployees();
            }

        }

        private void LoadEmployees()
        {
            var employees = _serviceEmployee.GetAllEmployee();

            if (employees != null)
            {
                if (employees.Count > 0)
                {
                    gvEmployee.DataSource = employees;
                    gvEmployee.DataBind();
                }
            }
            //EmployeeEntities
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeDetails.aspx");
        }

        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "update")
            {
                var id = Convert.ToInt64(e.CommandArgument.ToString());
                if (id > 0)
                {
                    Response.Redirect("EmployeeDetails.aspx?employeeId=" + id);
                }
            }

            else if (e.CommandName == "delete")
            {
                var id = Convert.ToInt64(e.CommandArgument.ToString());
                if (id > 0)
                {
                    hdnEmployeeId.Value = id.ToString();
                    lblModalMsg.Text = "Are you sure you want to delete this employee?";
                }
            }
        }

        protected void gvEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnDeleteConfim_Click(object sender, EventArgs e)
        {
            try
            {
                lblModalMsg.Text = string.Empty;
                var empId = Convert.ToInt64(hdnEmployeeId.Value);
                _serviceEmployee.DeleteEmplyee(empId);
                LoadEmployees();
                SetSuccessMessage("Data Deleted Successfully");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}