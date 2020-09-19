using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessLogic.ViewModel;
using HRWebApp.Utility;

namespace HRWebApp
{
    public partial class EmployeeDetails : BasePage
    {
        readonly EmployeeBLL _serviceEmployee = new EmployeeBLL();
        private long _employeeId;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            if (!IsPostBack)
            {
                string Id = Request.QueryString["employeeId"];
                if (Id != null)
                {
                    _employeeId = Convert.ToInt64(Id);
                    PopulateData();
                }
                else
                {
                    btnSave.Enabled = true;
                }
                
            }
        }

        private void PopulateData()
        {
            var emp = _serviceEmployee.GetEmployeeById(_employeeId);
            if (emp == null)
            {
                return;
            }
            txtName.Text = emp.Name;
            txtEmployeeCode.Text = emp.EmployeeCode;
            txtPhoneNo.Text = emp.PhoneNo;
            txtAddress.Text = emp.Address;
            txtEmail.Text = emp.Email;
            rbtnGender.SelectedValue = emp.Gender;
            hdnEmployeeId.Value = emp.EmployeeID.ToString();
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                EmployeeViewModel empVm = new EmployeeViewModel();

                empVm.Name = txtName.Text;
                empVm.EmployeeCode = txtEmployeeCode.Text;
                empVm.PhoneNo = txtPhoneNo.Text;
                empVm.Email = txtEmail.Text;
                empVm.Address = txtAddress.Text;
                empVm.Gender= rbtnGender.SelectedValue;

                string msg = _serviceEmployee.InsertEmployee(empVm);

                if (msg == "success")
                {
                    lblModalMsg.Text = "Data Saved Successfully";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnEmployeeId.Value))
            {
                EmployeeViewModel empVm = new EmployeeViewModel();

                empVm.Name = txtName.Text;
                empVm.EmployeeCode = txtEmployeeCode.Text;
                empVm.PhoneNo = txtPhoneNo.Text;
                empVm.Email = txtEmail.Text;
                empVm.Address = txtAddress.Text;
                empVm.Gender = rbtnGender.SelectedValue;
                empVm.EmployeeID = Convert.ToInt64(hdnEmployeeId.Value);

                string msg = _serviceEmployee.UpdateEmployee(empVm);

                if (msg == "success")
                {
                    lblModalMsg.Text = "Data Updated Successfully";
                }

            }
        }
    }
}