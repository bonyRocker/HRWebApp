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
    public partial class Employee : BasePage
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
            int pageNo = (int)ViewState["CurrentPage"];
            int pageSize = GlobalConstant.PageSize;
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            int totalRecords = 0;

            employees = _serviceEmployee.GetAllEmployee(pageNo, pageSize);
            totalRecords = _serviceEmployee.GetEmployeeCount();


            ViewState["EmployeeList"] = employees;

            if (employees != null)
            {
                if (employees.Count > 0)
                {
                    gvEmployee.DataSource = employees;
                    gvEmployee.DataBind();

                    BindPager(totalRecords, pageNo, pageSize);

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

        private void BindPager(int totalRecordCount, int currentPageIndex, int pageSize)
        {
            double getPageCount = (double)((decimal)totalRecordCount / (decimal)pageSize);
            int pageCount = (int)Math.Ceiling(getPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 1)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    if (i == 1)
                    {
                        pages.Add(new ListItem("First", i.ToString(), i != currentPageIndex + 1));
                    }
                    else if ( i == pageCount)
                    {
                        pages.Add(new ListItem("Last", i.ToString(), i != currentPageIndex + 1));
                    }
                    else
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPageIndex + 1));
                    }     
                }
            }

            rptPager.DataSource = pages;
            rptPager.DataBind();
            lblTotalRecoreds.Text = "Total Records: " + totalRecordCount;
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = Convert.ToInt32(((sender as LinkButton).CommandArgument));
            ViewState["CurrentPage"] = pageIndex - 1;
            LoadEmployeesGv();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int pageSize = GlobalConstant.PageSize;
            rptPager.Visible = false;

            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            if (!string.IsNullOrEmpty(txtSearchName.Text))
            {
                employees = _serviceEmployee.GetAllEmployeeByName(txtSearchName.Text);
                //totalRecords = employees.Count;
            }
            else if (!string.IsNullOrEmpty(txtSearchEmpCode.Text))
            {
                employees = _serviceEmployee.GetAllEmployeeByEmpCode(txtSearchEmpCode.Text);
                //totalRecords = employees.Count;
            }
            else if (!string.IsNullOrEmpty(txtSearchPhoneNo.Text))
            {
                employees = _serviceEmployee.GetAllEmployeeByPhoneNo(txtSearchPhoneNo.Text);
                //totalRecords = employees.Count;
            }
            else 
            {
                LoadEmployeesGv();
                rptPager.Visible = true;
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


    }
}