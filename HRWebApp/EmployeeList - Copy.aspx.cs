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
        EmployeeBLL _serviceEmployee = new EmployeeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HideAlert();
            if (!IsPostBack)
            {
                ViewState["CurrentPage"] = 0;
                ViewState["SortDirection"] = SortDirection.Ascending;
                LoadEmployeesGv();

            }

        }

        private void LoadEmployeesGv()
        {

            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            int totalRecords = 0;

            employees = _serviceEmployee.GetAllEmployee();
            totalRecords = _serviceEmployee.GetEmployeeCount();


            ViewState["EmployeeList"] = employees;

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
                LoadEmployeesGv();
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

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = ViewState["SortDirection"] == null ? SortDirection.Ascending : (SortDirection)ViewState["SortDirection"];
            ViewState["SortDirection"] = sortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            List<EmployeeViewModel> employees = (List<EmployeeViewModel>)ViewState["EmployeeList"];//If you store an object in session state, that object must be serializable.


            if (employees != null)
            {
                employees = SortColumn<EmployeeViewModel>(employees, sortExpression, sortDirection);
                gvEmployee.DataSource = employees;
                gvEmployee.DataBind();
            }
        }
      
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int pageSize = GlobalConstant.PageSize;

            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            if (!string.IsNullOrEmpty(txtSearchName.Text))
            {
                employees = _serviceEmployee.GetAllEmployeeByName(txtSearchName.Text);

            }
            else if (!string.IsNullOrEmpty(txtSearchEmpCode.Text))
            {
                employees = _serviceEmployee.GetAllEmployeeByEmpCode(txtSearchEmpCode.Text);

            }
            else if (!string.IsNullOrEmpty(txtSearchPhoneNo.Text))
            {
                employees = _serviceEmployee.GetAllEmployeeByPhoneNo(txtSearchPhoneNo.Text);

            }
            else
            {
                LoadEmployeesGv();
                return;
            }

            ViewState["EmployeeList"] = employees;
            lblTotalRecoreds.Text = "Total Records: " + employees.Count;
            if (employees != null)
            {
                if (employees.Count > 0)
                {
                    gvEmployee.DataSource = employees;
                }
                else
                {
                    gvEmployee.DataSource = null;
                }
                gvEmployee.DataBind();
            }

        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployee.PageIndex = e.NewPageIndex;
            ViewState["CurrentPage"] = e.NewPageIndex;
            LoadEmployeesGv();
        }


    }
}