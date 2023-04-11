using DataAccess.Model;
using HRWebApp.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRWebApp
{
    public partial class JournalEntry : BasePage
    {
        PNGDBEntities db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new PNGDBEntities();
            if (!IsPostBack)
            {

                if (Cache["AccountTable"] == null)
                {
                    List<A_Account> list = GetAccounts();
                    Cache["AccountTable"] = list;
                }

                int journalCount = db.A_JournalEntry.Where(x => x.IsActive == true).Count();
                journalCount++;
                txtJournalNo.Text = "GJ" + journalCount;
            }

        }

        private List<A_Account> GetAccounts()
        {
            return db.A_Account.Where(x => x.IsTransaction == true && x.IsActive == true).ToList();
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void AddNewRow()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values  
                        DropDownList ddlAccountName = (DropDownList)gvJournal.Rows[rowIndex].Cells[1].FindControl("ddlAccountName");
                        TextBox txtAccType = (TextBox)gvJournal.Rows[rowIndex].Cells[2].FindControl("txtAccType");
                        TextBox txtDebit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtDebit");
                        TextBox txtCredit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtCredit");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Column1"] = ddlAccountName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Column2"] = txtAccType.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = txtDebit.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = txtCredit.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    gvJournal.DataSource = dtCurrentTable;
                    gvJournal.DataBind();
                }
            }
            else
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("Column1", typeof(string)));
                dt.Columns.Add(new DataColumn("Column2", typeof(string)));
                dt.Columns.Add(new DataColumn("Column3", typeof(string)));
                dt.Columns.Add(new DataColumn("Column4", typeof(string)));
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;
                dr["Column3"] = string.Empty;
                dr["Column4"] = string.Empty;
                dt.Rows.Add(dr);
                //Store the DataTable in ViewState  
                ViewState["CurrentTable"] = dt;
                gvJournal.DataSource = dt;
                gvJournal.DataBind();
            }
            //Set Previous Data on Postbacks  
            SetPreviousData();
            CalculateSumDebitCredit();
        }

        private void SetPreviousData()

        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        DropDownList ddlAccountName = (DropDownList)gvJournal.Rows[rowIndex].Cells[1].FindControl("ddlAccountName");
                        TextBox txtAccType = (TextBox)gvJournal.Rows[rowIndex].Cells[2].FindControl("txtAccType");
                        TextBox txtDebit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtDebit");
                        TextBox txtCredit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtCredit");



                        ddlAccountName.SelectedValue = dt.Rows[i]["Column1"].ToString();
                        txtAccType.Text = dt.Rows[i]["Column2"].ToString();
                        txtDebit.Text = dt.Rows[i]["Column3"].ToString();
                        txtCredit.Text = dt.Rows[i]["Column4"].ToString();



                        rowIndex++;

                    }

                }

            }

        }

        protected void gvJournal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[5].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete?')){ return false; };";
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlAccountName = (e.Row.FindControl("ddlAccountName") as DropDownList);
                List<A_Account> listAccounts = null;
                if (Cache["AccountTable"] != null)
                {
                    listAccounts = (List<A_Account>)Cache["AccountTable"];
                }
                else
                {
                    Cache["AccountTable"] = GetAccounts();
                    listAccounts = (List<A_Account>)Cache["AccountTable"];
                }
                ddlAccountName.DataSource = listAccounts;// db.A_Account.Where(x => x.IsTransaction == true && x.IsActive == true).ToList();
                ddlAccountName.DataTextField = "AccountName";
                ddlAccountName.DataValueField = "AccountId";
                ddlAccountName.DataBind();

                //Add Default Item in the DropDownList
                ddlAccountName.Items.Insert(0, new ListItem("Please select", "0"));


            }

        }

        private void CalculateSumDebitCredit()
        {
            Label lbltotalDebit = (Label)gvJournal.FooterRow.FindControl("lbltotalDebit");
            Label lbltotalCredit = (Label)gvJournal.FooterRow.FindControl("lbltotalCredit");
            decimal totaldebit = 0;
            decimal totalcredit = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtDebit = (TextBox)gvJournal.Rows[i].Cells[3].FindControl("txtDebit");
                        TextBox txtCredit = (TextBox)gvJournal.Rows[i].Cells[3].FindControl("txtCredit");

                        if (!string.IsNullOrEmpty(txtDebit.Text))
                        {
                            totaldebit += Convert.ToDecimal(txtDebit.Text);
                        }
                        if (!string.IsNullOrEmpty(txtCredit.Text))
                        {
                            totalcredit += Convert.ToDecimal(txtCredit.Text);
                        }
                    }
                }
            }

            lbltotalDebit.Text = totaldebit.ToString();
            lbltotalCredit.Text = totalcredit.ToString();
        }

        protected void gvJournal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["CurrentTable"] as DataTable;
            dt.Rows[index].Delete();
            for (int i = index; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr["RowNumber"] = i + 1;

            }
            ViewState["CurrentTable"] = dt;
            gvJournal.DataSource = ViewState["CurrentTable"] as DataTable;
            gvJournal.DataBind();

            int rowIndex = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlAccountName = (DropDownList)gvJournal.Rows[rowIndex].Cells[1].FindControl("ddlAccountName");
                    TextBox txtAccType = (TextBox)gvJournal.Rows[rowIndex].Cells[2].FindControl("txtAccType");
                    TextBox txtDebit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtDebit");
                    TextBox txtCredit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtCredit");



                    ddlAccountName.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    txtAccType.Text = dt.Rows[i]["Column2"].ToString();
                    txtDebit.Text = dt.Rows[i]["Column3"].ToString();
                    txtCredit.Text = dt.Rows[i]["Column4"].ToString();



                    rowIndex++;

                }

            }
            CalculateSumDebitCredit();

        }

        protected void ddlAccountName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RetainGridviewData();
            //EnableDisableDebitCredit();
        }

        private void EnableDisableDebitCredit()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DataRow dr = dtCurrentTable.Rows[i];
                        //extract the TextBox values  
                        DropDownList ddlAccountName = (DropDownList)gvJournal.Rows[rowIndex].Cells[1].FindControl("ddlAccountName");
                        TextBox txtAccType = (TextBox)gvJournal.Rows[rowIndex].Cells[2].FindControl("txtAccType");
                        TextBox txtDebit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtDebit");
                        TextBox txtCredit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtCredit");


                        dr["RowNumber"] = i + 1;
                        if (!string.IsNullOrEmpty(ddlAccountName.SelectedValue) && ddlAccountName.SelectedValue != "0")
                        {
                            string idstr = ddlAccountName.SelectedValue;
                            long id = Convert.ToInt64(idstr);
                            txtAccType.Text = db.A_Account.FirstOrDefault(x => x.AccountId == id).AccountType;
                            dr["Column1"] = id;
                        }
                        else
                        {
                            txtAccType.Text = "";
                        }
                        dr["Column2"] = txtAccType.Text;
                        dr["Column3"] = txtDebit.Text;
                        dr["Column4"] = txtCredit.Text;

                        rowIndex++;
                    }
                    ViewState["CurrentTable"] = dtCurrentTable;

                }
            }
        }

        protected void txtDebit_TextChanged(object sender, EventArgs e)
        {
            RetainGridviewData();
            CalculateSumDebitCredit();
        }
        protected void txtCredit_TextChanged(object sender, EventArgs e)
        {
            RetainGridviewData();
            CalculateSumDebitCredit();
        }

        public void RetainGridviewData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DataRow dr = dtCurrentTable.Rows[i];
                        //extract the TextBox values  
                        DropDownList ddlAccountName = (DropDownList)gvJournal.Rows[rowIndex].Cells[1].FindControl("ddlAccountName");
                        TextBox txtAccType = (TextBox)gvJournal.Rows[rowIndex].Cells[2].FindControl("txtAccType");
                        TextBox txtDebit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtDebit");
                        TextBox txtCredit = (TextBox)gvJournal.Rows[rowIndex].Cells[3].FindControl("txtCredit");


                        dr["RowNumber"] = i + 1;
                        if (!string.IsNullOrEmpty(ddlAccountName.SelectedValue) && ddlAccountName.SelectedValue != "0")
                        {
                            string idstr = ddlAccountName.SelectedValue;
                            long id = Convert.ToInt64(idstr);
                            List<A_Account> listAccounts = (List<A_Account>)Cache["AccountTable"];
                            string accountType = listAccounts.FirstOrDefault(x => x.AccountId == id).AccountType;
                            txtAccType.Text = accountType;
                            dr["Column1"] = id;

                            txtDebit.Enabled = true;
                            txtCredit.Enabled = true;
                            if (accountType == "Assets" || accountType == "Expense")
                            {
                                txtCredit.Enabled = false;
                            }
                            else
                            {
                                txtDebit.Enabled = false;
                            }
                        }
                        else
                        {
                            txtDebit.Enabled = false;
                            txtCredit.Enabled = false;
                            txtAccType.Text = "";
                        }
                        dr["Column2"] = txtAccType.Text;
                        dr["Column3"] = txtDebit.Text;
                        dr["Column4"] = txtCredit.Text;

                        rowIndex++;
                    }
                    ViewState["CurrentTable"] = dtCurrentTable;

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                RetainGridviewData();
                if (IsValid())
                {
                    DateTime txnDate;
                    DateTime.TryParseExact(txtDate.Text,
                           "yyyy-MM-dd",
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out txnDate);


                    A_JournalEntry journalEntry = new A_JournalEntry();
                    journalEntry.TransactionDate = txnDate;
                    journalEntry.TransactionType = "0";
                    journalEntry.IsActive = true;
                    journalEntry.ReferenceNo = txtRefNo.Text;
                    journalEntry.Status = 1;
                    journalEntry.Description = txtDescription.Text;
                    journalEntry.CreatedBy = "PNG";
                    journalEntry.CreatedOn = DateTime.Now;
                    journalEntry.FolioNumber = txtJournalNo.Text;

                    db.A_JournalEntry.Add(journalEntry);
                    db.SaveChanges();

                    List<A_JournalEntryDetails> journalEntryDetails = new List<A_JournalEntryDetails>();
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DataRow dr = dtCurrentTable.Rows[i];

                        Int64 accountId = Convert.ToInt64(dr["Column1"].ToString());
                        string accountType = dr["Column2"].ToString();
                        string debit = string.IsNullOrEmpty(dr["Column3"].ToString())?"0": dr["Column3"].ToString();
                        string credit = string.IsNullOrEmpty(dr["Column4"].ToString()) ? "0" : dr["Column4"].ToString();
                        decimal debitAmount = 0;
                        decimal creditAmount = 0;

                        A_JournalEntryDetails obja_JournalEntryDetails = new A_JournalEntryDetails();
                        obja_JournalEntryDetails.AccountId = accountId;
                        obja_JournalEntryDetails.Credit = Convert.ToDecimal(credit);
                        obja_JournalEntryDetails.Debit = Convert.ToDecimal(debit);
                        obja_JournalEntryDetails.IsActive = true;
                        obja_JournalEntryDetails.JournalEntryId = journalEntry.ID;
                        obja_JournalEntryDetails.Status = 1;
                        obja_JournalEntryDetails.CreatedBy = "PNG";
                        obja_JournalEntryDetails.CreatedDate = DateTime.Now.Date;

                        journalEntryDetails.Add(obja_JournalEntryDetails);

                    }

                    db.A_JournalEntryDetails.AddRange(journalEntryDetails);
                    db.SaveChanges();

                    SetSuccessMessageWithDismiss("Successfully Saved");
                    btnSave.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                SetErrorMessageWithDismiss(ex.Message);

            }
        }

        private bool IsValid()
        {
            int rowIndex = 0;
            decimal totalDebit = 0;
            decimal totalCredit = 0;
            try
            {
                if (string.IsNullOrEmpty(txtDate.Text))
                {
                    SetErrorMessageWithDismiss("Select Transaction Date");
                    return false;
                }
                DateTime txnDate;
                if (!DateTime.TryParseExact(txtDate.Text, "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out txnDate))
                {
                    SetErrorMessageWithDismiss("Please Insert valid date");
                    return false;
                }

                if (string.IsNullOrEmpty(txtRefNo.Text))
                {
                    SetErrorMessageWithDismiss("Please enter reference number");
                    return false;
                }

                if (ViewState["CurrentTable"] == null)
                {
                    SetErrorMessageWithDismiss("Please insert transaction");
                    return false;
                }

                if (gvJournal.Rows.Count == 0)
                {
                    SetErrorMessageWithDismiss("Please insert transaction");
                    return false;
                }

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        DataRow dr = dtCurrentTable.Rows[i];
                        string accountType = dr["Column2"].ToString();
                        string debit = dr["Column3"].ToString();
                        string credit = dr["Column4"].ToString();
                        decimal debitAmount = 0;
                        decimal creditAmount = 0;

                        if (accountType == "Assets" || accountType == "Expense")
                        {
                            if (!string.IsNullOrEmpty(credit))
                            {
                                creditAmount = Convert.ToDecimal(credit);
                                if (creditAmount > 0)
                                {
                                    SetErrorMessageWithDismiss("You can not insert credit amount for this account");
                                    return false;

                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(debit))
                            {
                                debitAmount = Convert.ToDecimal(debit);
                                if (creditAmount > 0)
                                {
                                    SetErrorMessageWithDismiss("You can not insert debit amount for this account");
                                    return false;

                                }
                            }
                        }
                        DropDownList ddlAccountName = (DropDownList)gvJournal.Rows[i].Cells[1].FindControl("ddlAccountName");
                        TextBox txtDebit = (TextBox)gvJournal.Rows[i].Cells[3].FindControl("txtDebit");
                        TextBox txtCredit = (TextBox)gvJournal.Rows[i].Cells[3].FindControl("txtCredit");

                        if (ddlAccountName.SelectedValue == "0")
                        {
                            SetErrorMessageWithDismiss("Please select account type");
                            return false;
                        }

                        if (accountType == "Assets" || accountType == "Expense")
                        {
                            if (!string.IsNullOrEmpty(txtDebit.Text))
                            {
                                totalDebit += Convert.ToDecimal(txtDebit.Text);
                            }
                            else
                            {
                                SetErrorMessageWithDismiss("Please insert debit amount");
                                return false;
                            }
                        }
                        else
                        {

                            if (!string.IsNullOrEmpty(txtCredit.Text))
                            {
                                totalCredit += Convert.ToDecimal(txtCredit.Text);
                            }
                            else
                            {
                                SetErrorMessageWithDismiss("Please insert credit amount");
                                return false;
                            }
                        }
                    }
                    if (totalCredit == 0 || totalDebit == 0)
                    {
                        SetErrorMessageWithDismiss("Debit/Credit amount can not be zero");
                        return false;
                    }
                    if (totalCredit != totalDebit)
                    {
                        SetErrorMessageWithDismiss("Debit and Credit is not equal");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}