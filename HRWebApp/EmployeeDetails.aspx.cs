using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessLogic.ViewModel;
using HRWebApp.Utility;
using System.IO;

namespace HRWebApp
{
    public partial class EmployeeDetails : BasePage
    {
        private readonly EmployeeBLL _serviceEmployee = new EmployeeBLL();
        private long _employeeId;
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                try
                {
                    string employeeId = Request.QueryString["employeeId"];
                    if (employeeId != null)
                    {
                        _employeeId = Convert.ToInt64(employeeId);
                        PopulateData();
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnUpdate.Enabled = false;
                    }

                }
                catch (Exception ex)
                {
                    SetErrorMessageWithDismiss(ex.Message);
                }

            }
        }

        private void PopulateData()
        {
            try
            {
                var employee = _serviceEmployee.GetEmployeeById(_employeeId);
                if (employee == null)
                {
                    SetErrorMessageWithDismiss("No Employee Found ! ! !");
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = false;
                    return;
                }
                txtName.Text = employee.Name;
                txtEmployeeCode.Text = employee.EmployeeCode;
                txtPhoneNo.Text = employee.PhoneNo;
                txtAddress.Text = employee.Address;
                txtEmail.Text = employee.Email;
                rbtnGender.SelectedValue = employee.Gender;
                hdnEmployeeId.Value = employee.EmployeeID.ToString();
                if (employee.PhotoByte != null)
                {
                    imgEmpPhoto.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])employee.PhotoByte);
                }

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblModalMsg.Text = string.Empty;
                if (ValidateEmployeeInsert())
                {

                    EmployeeViewModel oEmpVm = new EmployeeViewModel();

                    oEmpVm.Name = txtName.Text;
                    oEmpVm.EmployeeCode = txtEmployeeCode.Text;
                    oEmpVm.PhoneNo = txtPhoneNo.Text;
                    oEmpVm.Email = txtEmail.Text;
                    oEmpVm.Address = txtAddress.Text;
                    oEmpVm.Gender = rbtnGender.SelectedValue;

                    if (fileuploadImage.HasFile)
                    {
                        string uploadFolder = Request.PhysicalApplicationPath + "EmployeeImage\\";
                        string extension = Path.GetExtension(fileuploadImage.PostedFile.FileName);
                        var filename = txtEmployeeCode.Text + extension;
                        var fullpath = uploadFolder + filename;
                        fileuploadImage.SaveAs(uploadFolder + filename);

                        using (Stream fs = fileuploadImage.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] image = br.ReadBytes((Int32)fs.Length);

                                // byte[] image = fileuploadImage.FileBytes;
                                oEmpVm.PhotoByte = image;
                                oEmpVm.PhotoPath = fullpath;

                            }
                        }

                    }


                    string msg = _serviceEmployee.InsertEmployee(oEmpVm);

                    if (msg == "success")
                    {
                        lblModalMsg.Text = "Data Saved Successfully";
                    }

                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool ValidateEmployeeInsert()
        {
            lblModalMsg.Text = string.Empty;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                SetErrorMessageWithDismiss("Employee Name Is Required");
                return false;
            }
            if (string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                SetErrorMessageWithDismiss("Employee Code Is Required");
                return false;
            }
            if(_serviceEmployee.IsEmployeeExist(txtEmployeeCode.Text))
            {
                SetErrorMessageWithDismiss("Employee Code Already Exist");
                return false;
            }
            return true;
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
                if (fileuploadImage.HasFile)
                {
                    string uploadFolder = Request.PhysicalApplicationPath + "EmployeeImage\\";
                    string extension = Path.GetExtension(fileuploadImage.PostedFile.FileName);
                    var filename = txtEmployeeCode.Text + extension;
                    var fullpath = uploadFolder + filename;
                    fileuploadImage.SaveAs(uploadFolder + filename);

                    using (Stream fs = fileuploadImage.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] image = br.ReadBytes((Int32)fs.Length);

                            // byte[] image = fileuploadImage.FileBytes;
                            empVm.PhotoByte = image;
                            empVm.PhotoPath = fullpath;

                        }
                    }

                }

                string msg = _serviceEmployee.UpdateEmployee(empVm);

                if (msg == "success")
                {
                    lblModalMsg.Text = "Data Updated Successfully";
                }

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");
        }
    }
}