<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JournalEntry.aspx.cs" Inherits="HRWebApp.JournalEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td>Date</td>
            <td>
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                <%--<asp:ImageButton ID="ibSpouseDateOfBirth" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtSpouseDateOfBirth'));return false;"></asp:ImageButton>--%>

                <%-- <asp:maskededitextender id="MaskedEditExtender2" targetcontrolid="txtDate" masktype="Date" mask="99/99/9999" runat="server" xmlns:asp="#unknown">
                </asp:maskededitextender>--%>
            </td>
            <td>Journal No.</td>

            <td>
                <asp:TextBox ID="txtJournalNo" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Reference No</td>
            <td>
                <asp:TextBox ID="txtRefNo" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td>Description</td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Height="55px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <div id="gvAddrow">
        <asp:Button ID="btnAddRow" runat="server" Text="Add Transaction" OnClick="btnAddRow_Click" />
        <asp:Button ID="btnSave" runat="server" Text="Save Journal" OnClick="btnSave_Click" />

        <asp:GridView ID="gvJournal" runat="server" ShowFooter="true" AutoGenerateColumns="false" OnRowDataBound="gvJournal_RowDataBound" OnRowDeleting="gvJournal_RowDeleting">
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                <asp:TemplateField HeaderText="Account Name">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlAccountName" runat="server" OnSelectedIndexChanged="ddlAccountName_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acctount Type">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAccType" runat="server" Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debit">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDebit" runat="server" OnTextChanged="txtDebit_TextChanged" TextMode="Number" AutoPostBack="true" Text="0"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lbltotalDebit" runat="server" Text="0"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCredit" runat="server" OnTextChanged="txtCredit_TextChanged" TextMode="Number" AutoPostBack="true" Text="0"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lbltotalCredit" runat="server" Text="0"></asp:Label>
                    </FooterTemplate>
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

        $('#MainContent_txtDate').datepicker(
            {
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true,
                yearRange: '1950:2100'
            });

    </script>
</asp:Content>
