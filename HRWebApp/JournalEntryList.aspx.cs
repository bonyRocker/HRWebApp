using DataAccess.Model;
using DataAccess.Repository;
using HRWebApp.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRWebApp
{
    public partial class JournalEntryList : BasePage
    {
        PNGDBEntities db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new PNGDBEntities();
            if (!IsPostBack)
            {
                gvJournal.DataSource = null;
                gvJournal.DataBind();

            }

        }

        
        protected void txtButtonSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            db = new PNGDBEntities();
            DateTime txnStartDate;
            if (!DateTime.TryParseExact(txtStartDate.Text, "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out txnStartDate))
            {
                SetErrorMessageWithDismiss("Please Insert valid Start date");
                return;
            }
            DateTime txnEndDate;
            if (!DateTime.TryParseExact(txtEndDate.Text, "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out txnEndDate))
            {
                SetErrorMessageWithDismiss("Please Insert valid End date");
                return;
            }

            var list = db.A_JournalEntry.Where(x => x.IsActive == true && DbFunctions.TruncateTime(x.TransactionDate) >= txnStartDate && DbFunctions.TruncateTime(x.TransactionDate) <= txnEndDate).ToList();

            if (list.Count() > 0)
            {
                gvJournal.DataSource = list;
                gvJournal.DataBind();
            }
            else
            {
                gvJournal.DataSource = null;
                gvJournal.DataBind();
            }
        }

        protected void gvJournal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = (GridView)e.Row.FindControl("gvJournalDetails");
                string idStr = gvJournal.DataKeys[e.Row.RowIndex].Values[0].ToString();

                string query = @" select jd.Id,a.AccountName,jd.Debit,jd.Credit from A_JournalEntryDetails jd
                                    inner join A_Account a on jd.AccountId = a.AccountId
                                    where jd.JournalEntryId = @JournalEntryId";
                SqlParameter[] parameters = { new SqlParameter("@JournalEntryId", SqlDbType.Int) };
                parameters[0].Value = Convert.ToInt64(idStr);



                DataSet ds = DataAccessLayer.ExecuteDataSet(CommandType.Text, query, parameters);
                gv.DataSource = ds;
                gv.DataBind();
            }
        }

        protected void gvJournal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Int64 id = (Int64)gvJournal.DataKeys[e.RowIndex].Value;
                if (id > 0)
                {
                    var journal = db.A_JournalEntry.FirstOrDefault(x => x.ID == id);
                    var journalList = db.A_JournalEntryDetails.Where(x => x.JournalEntryId == id);
                    db.A_JournalEntryDetails.RemoveRange(journalList);
                    db.A_JournalEntry.Remove(journal);
                    db.SaveChanges();

                    SetSuccessMessageWithDismiss("Successfully Deleted");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                SetErrorMessageWithDismiss(ex.Message);
            }
            
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("JournalEntry.aspx");
        }
    }
}