<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="HRWebApp.Employee" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Modal Center -->
    <div class="modal fade" id="modalConfirmation" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalCenterTitle">Message</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>

                </div>
                <div class="modal-footer">

                    <asp:Button ID="btnDeleteConfim" runat="server" Text="Yes" OnClick="btnDeleteConfim_Click" OnClientClick="fnContinue()" CssClass="btn btn-outline-danger" />
                    <asp:Button ID="btnDeleteCancel" runat="server" Text="No" OnClick="btnDeleteCancel_Click" OnClientClick="fnRevert()" CssClass="btn btn-outline-success" />

                </div>
            </div>
        </div>
    </div>
    <!-- Modal Center -->


    <div class="row">
        <asp:HiddenField ID="hdnEmployeeId" runat="server" />
        <div class="col-lg-12">
            <div class="card mb-4">
                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                    <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success mb-1" Text="Add" OnClick="btnAdd_Click" />
                </div>

                <div class="row">
                    <div class="col-lg-12 ">

                        <div class="form-group form-inline">
                            <label class="col-sm-1 col-form-label">Name</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 col-form-label">Employee Code</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtSearchEmpCode" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 col-form-label">Phone Number</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtSearchPhoneNo" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-success mb-1" Text="Search" OnClick="btnSearch_Click" />
                            </div>

                        </div>
                    </div>
                </div>


           
                <%--<div class="table-responsive p-3">

                    <label class="col-sm-1 col-form-label">Name</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                    </div>
                    <label class="col-sm-2 col-form-label">Employee Code</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                    </div>
                    <label class="col-sm-2 col-form-label">Phone Number</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>

                    </div>
                </div>--%>
                <div class="table-responsive p-3">
                    <asp:Label ID="lblTotalRecoreds" runat="server" Text="" CssClass="table align-items-center table-flush"></asp:Label>
                    <asp:GridView ID="gvEmployee" runat="server" CssClass="table align-items-center table-flush" AutoGenerateColumns="false" EmptyDataText="No Record Found."
                        Width="100%" DataKeyNames="EmployeeID" Font-Names="Tahoma"
                        Font-Size="10pt" EnableModelValidation="True" Visible="true" OnRowCommand="gvEmployee_RowCommand" OnRowDeleting="gvEmployee_RowDeleting" AllowSorting="True" OnSorting="gvEmployee_Sorting">
                        <PagerSettings Position="TopAndBottom" />

                        <Columns>
                            <asp:BoundField HeaderText="EmployeeID" DataField="EmployeeID" HeaderStyle-Width="50" Visible="False"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#663300">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <%-- <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-Width="100"
                                Visible="true" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-ForeColor="#663300">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="Employee Name" DataField="Name"
                                Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#663300" SortExpression="Name">
                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Employee Code" DataField="EmployeeCode"
                                Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#663300" SortExpression="EmployeeCode">
                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Phone Number" DataField="PhoneNo"
                                Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#663300" SortExpression="PhoneNo">
                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Gender" DataField="Gender"
                                Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#663300" SortExpression="Gender">
                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Address" DataField="Address"
                                Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#663300" SortExpression="Address">
                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnUpdate" CssClass="btn btn-primary mb-1" Text="Update"
                                        CommandArgument='<%# Eval("EmployeeID") %>' CommandName="update" />
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger mb-1" Text="Delete"
                                        CommandArgument='<%# Eval("EmployeeID") %>' CommandName="delete" />
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>


                        </Columns>
                        <HeaderStyle BackColor="#DADADA" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="#f7f7f7" HorizontalAlign="Left" VerticalAlign="Top" />
                        <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" />
                    </asp:GridView>
                    <%--<asp:LinkButton ID="lnkFirst" runat="server">First</asp:LinkButton>--%>
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server"
                                Text='<%#Eval("Text") %>'
                                CommandArgument='<%#Eval("Value") %>'
                                Enabled='<%#Eval("Enabled") %>'
                                OnClick="Page_Changed"
                                ForeColor="#267CB2"
                                Font-Bold="true" />
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<asp:LinkButton ID="lnkLast" runat="server">Last</asp:LinkButton>--%>
                </div>
            </div>
        </div>

    </div>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            if ($('#MainContent_lblModalMsg').text() != "") {
                ShowModal();
            }
        });

        function ShowModal() {
            $('.modal').modal('show');
        }

        function fnContinue() {
            $('.modal').modal('hide');


        }

        function fnRevert() {
            $('.modal').modal('hide');

        }
    </script>
</asp:Content>


<%--<asp:GridView ID="GridView1" runat="server" CssClass="table align-items-center table-flush" AutoGenerateColumns="false" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                        PageSize="40" AllowPaging="True" Width="100%" DataKeyNames="ID" Font-Names="Tahoma"
                                        Font-Size="10pt" EnableModelValidation="True" Visible="true" OnRowCommand="gvTran_RowCommand"
                                        OnRowDataBound="gvTran_RowDataBound" 
                                        onrowcancelingedit="gvTran_RowCancelingEdit" 
                                        onpageindexchanging="gvTran_PageIndexChanging">--%>

