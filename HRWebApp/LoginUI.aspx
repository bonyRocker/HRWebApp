<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="LoginUI.aspx.cs" Inherits="HRWebApp.LoginUI1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-login">
        <div class="row justify-content-center">
            <div class="col-xl-10 col-lg-12 col-md-9">
                <div class="card shadow-sm my-5">
                    <div class="card-body p-0">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="login-form">
                                    <div class="text-center">
                                        <h1 class="h4 text-gray-900 mb-4">Login</h1>
                                    </div>
                                    <form class="user" runat="server">
                                        <div class="form-group">
                                            <%--<input type="email" class="form-control" id="exampleInputEmail" aria-describedby="emailHelp"
                                                    placeholder="Enter Email Address">--%>
                                            <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="Enter User Id"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <%--<input type="password" class="form-control" id="exampleInputPassword" placeholder="Password">--%>
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
                                        </div>
                                        <div class="text-left">
                                            <%--<a class="font-weight-bold small" href="register.html">Forget Password?</a>--%>
                                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="font-weight-bold small">Forget Password?</asp:HyperLink>
                                        </div>
                                        <hr>
                                        <div class="form-group">
                                            <%--<a href="index.html" class="btn btn-primary btn-block">Login</a>--%>
                                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                                        </div>
                                        <hr>
                                        <div class="text-center">
                                            <img src="img/makasoft.png" alt="makasoft" width="400" height="100" />
                                        </div>
                                        <hr>
                                         <div id="alertLogin" class="alert alert-danger" role="alert" runat="server" Visible="False">
                                           <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                        </div>
                                    </form>
                                    <hr>
                                    <%--<div class="text-center">
                                        <a class="font-weight-bold small" href="register.html">Create an Account!</a>
                                    </div>--%>
                                    <div class="text-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
