<%@ Page Title="Manage Members" Language="C#" MasterPageFile="~/Admin-Dashboard.Master" AutoEventWireup="true" CodeBehind="Manage-Members.aspx.cs" Inherits="Z_Wallet.Manage_Members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-users"></i>Manage Members</h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="/Admin" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="/Manage-Members" class="breadcrumb-link">Manage Member</a></li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="card">
                            <h5 class="card-header">List of Members</h5>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered first">
                                        <thead>
                                            <tr>
                                                <th>Account No.</th>
                                                <th>Full Name</th>
                                                <th>Email</th>
                                                <th>Phone Number</th>
                                                <th>Date Created</th>
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <% foreach (var member in MembersList)
                                                { %>
                                            <tr>
                                                <td><%= member.AccountNumber %></td>
                                                <td><%= member.FullName %></td>
                                                <td><%= member.Email %></td>
                                                <td><%= member.PhoneNumber %></td>
                                                <td><%= member.DateCreated %></td>
                                                <td>
                                                    <span class="badge <%= member.StatusBadgeClass %> text-white"><%= member.Status %></span>
                                                </td>
                                                <td class="align-right text-center">
                                                    <a href="/User-Members.aspx?accountNumber=<%= member.AccountNumber %>" class="text-primary font-weight-bold text-xs" data-toggle="tooltip" data-original-title="Edit">
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
    </main> 
</asp:Content>
