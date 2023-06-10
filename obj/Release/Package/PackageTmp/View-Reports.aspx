<%@ Page Title="View Reports" Language="C#" MasterPageFile="~/Admin-Dashboard.Master" AutoEventWireup="true" CodeBehind="View-Reports.aspx.cs" Inherits="Z_Wallet.View_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-users"></i>Reports </h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="/Admin" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="/View-Reports" class="breadcrumb-link">View Reports</a></li>
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
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <% foreach (var report in ReportsList)
                                                { %>
                                            <tr>
                                                <td><%= report.AccountNumber %></td>
                                                <td><%= report.FullName %></td>
                                                <td><%= report.Email %></td>
                                                <td><%= report.PhoneNumber %></td>
                                                <td>
                                                    <span class="badge <%= report.StatusBadgeClass %> text-white"><%= report.Status %></span>
                                                </td>
                                                <td class="align-right text-center">
                                                    <a href="/Member-Reports.aspx?accountNumber=<%= report.AccountNumber %>" class="text-primary font-weight-bold text-xs" data-toggle="tooltip" data-original-title="View">
                                                        <i class="fa fa-eye text-info fa-lg"></i>
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
