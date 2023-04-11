<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JournalEntryList.aspx.cs" Inherits="HRWebApp.JournalEntryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td>Start Date</td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                <%--<asp:ImageButton ID="ibSpouseDateOfBirth" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtSpouseDateOfBirth'));return false;"></asp:ImageButton>--%>

                <%-- <asp:maskededitextender id="MaskedEditExtender2" targetcontrolid="txtDate" masktype="Date" mask="99/99/9999" runat="server" xmlns:asp="#unknown">
                </asp:maskededitextender>--%>
            </td>
            <td>End Date</td>

            <td>
                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
            </td>
            <td></td>

            <td>
                <asp:Button ID="txtButtonSearch" runat="server" Text="Search" OnClick="txtButtonSearch_Click" />
            </td>
        </tr>

    </table>
    <br />
    <div>
        <asp:Button ID="btnAddNew" runat="server" Text="Add New Record" OnClick="btnAddNew_Click" />
    </div>
    <div id="gvAddrow">


        <asp:GridView ID="gvJournal" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table align-items-center table-flush" OnRowDataBound="gvJournal_RowDataBound" OnRowDeleting="gvJournal_RowDeleting">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date"  DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="ReferenceNo" HeaderText="Reference No" />
                <asp:BoundField DataField="FolioNumber" HeaderText="Journal No" />
                <asp:BoundField DataField="Description" HeaderText="Description" />

                <asp:TemplateField HeaderText="Journal Details">
                    <ItemTemplate>
                        <asp:GridView ID="gvJournalDetails" runat="server" AutoGenerateColumns="false" >
                            <Columns>
                                <asp:BoundField DataField="AccountName" HeaderText="Account Name" />
                                <asp:BoundField DataField="Debit" HeaderText="Debit" />
                                 <asp:BoundField DataField="Credit" HeaderText="Credit" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <div>
    </div>
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/jqueryui/jquery-ui.min.js"></script>
    <script type="text/javascript">

        $('#MainContent_txtStartDate').datepicker(
            {
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true,
                yearRange: '1950:2100'
            });

        $('#MainContent_txtEndDate').datepicker(
            {
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true,
                yearRange: '1950:2100'
            });

    </script>
</asp:Content>
