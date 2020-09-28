<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="HRWebApp.EmployeeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <!-- Modal Center -->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
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
                    <%-- <button type="button" class="btn btn-outline-primary" data-dismiss="modal">Close</button>--%>
                    <button type="button" class="btn btn-outline-primary" onclick="fnContinue();">Ok</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Center -->
    <div class="row">

        <div class="col-lg-12">


            <!-- Horizontal Form -->
            <div class="card mb-4">
                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 font-weight-bold text-primary">Employee Form</h6>
                </div>
                <div class="card-body">
                    <asp:HiddenField ID="hdnEmployeeId" runat="server" />
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Name</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Email</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Employee Code</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control" placeholder="Employee Code"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Phone No</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" placeholder="Phone No"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Address</label>
                        <div class="col-sm-5">
                            <%--<textarea class="form-control" id="exampleFormControlTextarea1" rows="3" placeholder="Address"></textarea>--%>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Gender</label>
                        <div class="col-sm-3" style="margin-left: -20px">
                            <div class="custom-control custom-radio">
                                <asp:RadioButtonList ID="rbtnGender" runat="server">
                                    <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                </asp:RadioButtonList>
                                <%-- <asp:RadioButton ID="rbtnMale" runat="server" GroupName="gender" Text="Male" />--%>
                            </div>
                            <div class="custom-control custom-radio">
                                <%--<input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio2">Female</label>--%>
                                <%-- <asp:RadioButton ID="rbtnFemale" runat="server" GroupName="gender" Text="Female" />--%>
                            </div>
                        </div>
                    </div>

                     <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Photo</label>
                        <div class="col-sm-5">
                            <asp:FileUpload ID="fileuploadImage" runat="server"  />
                           
                        </div>
                    </div>

                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label"></label>
                        <div class="col-sm-5" >
                            <asp:Image ID="imgEmpPhoto" runat="server"  Width="200px" Height="150px" ImageUrl="~/EmployeeImage/blank.jpeg" /> <%----%>
                        </div>
                    </div>


                    <div class="form-group row">
                        <div class="col-sm-10">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
                        </div>
                    </div>

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
            window.location.href = "EmployeeList.aspx";

        }
    </script>
</asp:Content>
