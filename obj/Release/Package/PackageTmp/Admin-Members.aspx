<%@ Page Title="Admin Members" Language="C#" MasterPageFile="~/Admin-Dashboard.Master" AutoEventWireup="true" CodeBehind="Admin-Members.aspx.cs" Inherits="Z_Wallet.Admin_Members" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid  dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-users"></i>Admin Members </h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="/Admin" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="/Admin-Members" class="breadcrumb-link">Admin Member</a></li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="card">
                            <h5 class="card-header">List of Admin Members</h5>
                            <div class="card-body">
                                <a class="btn btn-sm btn-success" href="/Add-Admin-Members"><i class="fa fa-fw fa-user-plus"></i>Add Member</a><br />
                                <br />
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered first">
                                        <thead>
                                            <tr>
                                                <th>Admin ID</th>
                                                <th>Full Name</th>
                                                <th>Email</th>
                                                <th>Date Created</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <% foreach (var member in AdminList)
                                                { %>
                                            <tr>
                                                <td><%= member.AdminID %></td>
                                                <td><%= member.FullName %></td>
                                                <td><%= member.Email %></td>
                                                <td><%= member.DateCreated %></td>
                                                <td class="align-right text-center">
                                                    <a href="/Edit-Admin-Account.aspx?AdminID=<%= member.AdminID %>" class="text-primary font-weight-bold text-xs" data-toggle="tooltip" data-original-title="Edit">
                                                        <i class="fa fa-edit fa-lg"></i>
                                                    </a>
                                                </td>
                                            </tr>
                                            <% } %>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
