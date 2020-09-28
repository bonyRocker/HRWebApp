<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="HRWebApp.EmployeeList" %>


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
                    <asp:Button ID="btnDeleteCancel" runat="server" Text="No" OnClientClick="fnRevert();return false;" CssClass="btn btn-outline-success" /><%--OnClick="btnDeleteCancel_Click"--%>
                     
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
                                <asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control-sm " placeholder="Name"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 col-form-label">Employee Code</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtSearchEmpCode" runat="server" CssClass="form-control-sm " placeholder="Employee Code"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 col-form-label">Phone Number</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtSearchPhoneNo" runat="server" CssClass="form-control-sm " placeholder="Phone Number"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 text-center">
                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-success btn-sm mb-1 " Text="Search" />
                                <%--OnClick="btnSearch_Click"--%>
                            </div>

                        </div>
                    </div>
                </div>


                <div class="table-responsive p-3" id="EmployeeList">
                </div>
            </div>
        </div>

    </div>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            if ($('#MainContent_lblModalMsg').text() != "") {
                ShowModal();
            }

            //Prepare jtable plugin
            $('#EmployeeList').jtable({
                //title: 'Employee List',
                paging: true, //Enables paging
                pageSize: 10, //Actually this is not needed since default value is 10.
                sorting: true, //Enables sorting
                defaultSorting: 'EmployeeID ASC', //Optional. Default sorting on first load.
                actions: {
                    listAction: '/EmployeeList.aspx/LoadEmployeeList',
                    //createAction: '/EmployeeList.aspx/CreateStudent',
                    //updateAction: '/EmployeeList.aspx/UpdateEmployee',
                    // deleteAction: '/EmployeeList.aspx/DeleteStudent'
                },
                fields: {
                    EmployeeID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    Name: {
                        title: 'Name',
                        width: '23%'
                    },
                    EmployeeCode: {
                        title: 'Employee Code',

                    },
                    PhoneNo: {
                        title: 'Phone No',

                    },
                    Gender: {
                        title: 'Gender',
                        width: '13%',
                    },
                    Address: {
                        title: 'City',
                        width: '12%'

                    },
                    Edit: {
                        title: 'Edit',
                        width: '1%',
                        sorting: false,
                        create: false,
                        edit: false,
                        list: true,
                        display: function (data) {
                            if (data.record) {
                                // This if you want a custom edit action.
                                return '<button title="Edit" class="jtable-command-button jtable-edit-command-button" onclick="EditPage(' + data.record.EmployeeID + '); return false;"><span>Edit</span></button>';
                            }
                        }

                    },
                    Delete: {
                        title: 'Delete',
                        width: '1%',
                        sorting: false,
                        create: false,
                        edit: false,
                        list: true,
                        display: function (data) {
                            if (data.record) {
                                // This if you want a custom edit action.
                                return '<button title="Edit" class="jtable-command-button jtable-delete-command-button" onclick="DeleteRecord(' + data.record.EmployeeID + '); return false;"><span>Edit</span></button>';
                            }
                        }

                    },

                }
            });

            //Load student list from server
            //$('#EmployeeList').jtable('load');
            $('#EmployeeList').jtable('load', {
                name: $('#MainContent_txtSearchName').val(),
                empcode: $('#MainContent_txtSearchEmpCode').val(),
                phoneno: $('#MainContent_txtSearchPhoneNo').val(),
            });

            //Re-load records when user click 'load records' button.
            $('#MainContent_btnSearch').click(function (e) {
                e.preventDefault();
                $('#EmployeeList').jtable('load', {
                    name: $('#MainContent_txtSearchName').val(),
                    empcode: $('#MainContent_txtSearchEmpCode').val(),
                    phoneno: $('#MainContent_txtSearchPhoneNo').val(),
                });
            });

        });

        function ShowModal() {
            $('.modal').modal('show');
        }

        function fnContinue() {
            $('.modal').modal('hide');


        }

        function fnRevert() {
            $('.modal').modal('hide');
            $('#MainContent_lblModalMsg').text('');
        }

        function EditPage(empid) {

            var employeeId = empid;
            $.ajax({
                type: "POST",
                url: "EmployeeList.aspx/UpdateEmployee",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'empid':'" + employeeId + "'}",
                success: function (response) {
                    window.location = response.d;
                },
                failure: function (response) {
                    //alert(response.d);
                }
            });
        }

        function DeleteRecord(empid) {

            var employeeId = empid;
            $.ajax({
                type: "POST",
                url: "EmployeeList.aspx/DeleteEmployee",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'empid':'" + employeeId + "'}",
                success: function (response) {
                    if (response.d == "found") {
                        $('#<%=hdnEmployeeId.ClientID %>').val(empid);
                        $('#MainContent_lblModalMsg').text('Are you sure you want to delete?');
                        ShowModal();

                    }
                },
                failure: function (response) {
                    //alert(response.d);
                }
            });
        }
    </script>
</asp:Content>


<%--<asp:GridView ID="GridView1" runat="server" CssClass="table align-items-center table-flush" AutoGenerateColumns="false" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                        PageSize="40" AllowPaging="True" Width="100%" DataKeyNames="ID" Font-Names="Tahoma"
                                        Font-Size="10pt" EnableModelValidation="True" Visible="true" OnRowCommand="gvTran_RowCommand"
                                        OnRowDataBound="gvTran_RowDataBound" 
                                        onrowcancelingedit="gvTran_RowCancelingEdit" 
                                        onpageindexchanging="gvTran_PageIndexChanging">--%>

