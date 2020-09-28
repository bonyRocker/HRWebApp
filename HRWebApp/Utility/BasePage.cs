using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using Utility.Helper;

namespace HRWebApp.Utility
{
    public class BasePage : System.Web.UI.Page
    {
        public bool CheckEndSession()
        {
            if (Session.Count == 0)
                return true;
            else return false;
        }

        public static void FillCombo(DropDownList dropDownList, string dataTextField, string dataValueField, IList iList, bool bHasBlank, bool bMandatory, bool isEmptyList)
        {
            dropDownList.DataTextField = dataTextField;
            dropDownList.DataValueField = dataValueField;
            if (isEmptyList == false)
            {
                dropDownList.DataSource = iList;
                dropDownList.DataBind();
            }
            else
            {
                dropDownList.DataSource = null;
                dropDownList.DataBind();
            }
            if (bHasBlank)
            {
                ListItem oItem = new ListItem();
                if (!bMandatory)
                {
                    oItem.Value = "-1";
                    oItem.Text = "Select";
                }
                dropDownList.Items.Insert(0, oItem);
            }
        }
        public static void FillCombo(DropDownList dropDownList, string dataTextField, string dataValueField, IList iList, bool bHasBlank, bool bIsAll)
        {
            dropDownList.Items.Clear();
            dropDownList.DataTextField = dataTextField;
            dropDownList.DataValueField = dataValueField;
            dropDownList.DataSource = iList;
            dropDownList.DataBind();

            if (bHasBlank)
            {
                ListItem oItem = new ListItem();
                if (!bIsAll)
                {
                    oItem.Value = "0";
                    oItem.Text = "ALL";
                }
                else
                {
                    oItem.Value = "0";
                    oItem.Text = "Select";
                }
                dropDownList.Items.Insert(0, oItem);
            }
        }
        public void FillCombo(DropDownList dropDownList, string dataTextField, string dataValueField, DataTable dataTbl, bool bHasBlank, bool bMandatory)
        {
            dropDownList.DataTextField = dataTextField;
            dropDownList.DataValueField = dataValueField;
            dropDownList.DataSource = dataTbl;
            dropDownList.DataBind();


            if (bHasBlank)
            {
                ListItem oItem = new ListItem();
                if (!bMandatory)
                {
                    oItem.Value = "0";
                    oItem.Text = "Select";
                }
                dropDownList.Items.Insert(0, oItem);
            }
        }
        public static void FillCombo(DropDownList dropDownList, string[] sArray, bool bHasBlank, bool bMandatory)
        {
            dropDownList.Items.Clear();
            ListItem oItem = new ListItem();

            if (bHasBlank)
            {
                if (!bMandatory)
                {
                    oItem.Value = "0";
                    oItem.Text = "";
                }
                dropDownList.Items.Add(oItem);
            }

            for (int i = 0; i <= sArray.GetUpperBound(0); i++)
            {
                oItem = new ListItem();
                oItem.Text = sArray[i];
                oItem.Value = Convert.ToString(i + 1);
                dropDownList.Items.Add(oItem);
            }
        }
        public static void FillCombo(DropDownList dropDownList, int[] sArray, bool bHasBlank, bool bMandatory)
        {
            dropDownList.Items.Clear();
            ListItem oItem = new ListItem();

            if (bHasBlank)
            {
                if (!bMandatory)
                {
                    oItem.Value = "0";
                    oItem.Text = "";
                }
                dropDownList.Items.Add(oItem);
            }

            for (int i = 0; i <= sArray.GetUpperBound(0); i++)
            {
                oItem = new ListItem();
                oItem.Text = sArray[i].ToString();
                oItem.Value = sArray[i].ToString();
                dropDownList.Items.Add(oItem);
            }
        }
        public void ClearApplicationCache()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        public void SetSuccessMessage(string message)
        {
            Label lblAlertSuccess = (Label)Master.FindControl("lblAlertSuccess");
            lblAlertSuccess.Text = message;
            System.Web.UI.HtmlControls.HtmlGenericControl currdiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("dvalertSuccess");
            currdiv.Visible = true;
        }
        public void SetErrorMessage(string message)
        {
            Label lblAlertFail = (Label)Master.FindControl("lblAlertFail");
            lblAlertFail.Text = message;
            System.Web.UI.HtmlControls.HtmlGenericControl currdiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("dvalertFail");
            currdiv.Visible = true;
        }

        public void HideAlert()
        {
            Label lblAlertSuccess = (Label)Master.FindControl("lblAlertSuccess");
            lblAlertSuccess.Text = string.Empty;
            System.Web.UI.HtmlControls.HtmlGenericControl currSuccessdiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("dvalertSuccess");
            currSuccessdiv.Visible = false;

            Label lblAlertFail = (Label)Master.FindControl("lblAlertFail");
            lblAlertFail.Text = string.Empty;
            System.Web.UI.HtmlControls.HtmlGenericControl currFaildiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("dvalertFail");
            currFaildiv.Visible = false;
        }

        public List<T> SortColumn<T>(List<T> list, string sortExpression,SortDirection sortDirection) where T: class
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortExpression);
            if (sortDirection == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<T>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<T>();
            }
        }

        public static List<T> SortColumns<T>(List<T> list, string sortExpression, SortDirection sortDirection) where T : class
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortExpression);
            if (sortDirection == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<T>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<T>();
            }
        }

    }
}