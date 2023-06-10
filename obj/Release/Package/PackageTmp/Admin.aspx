<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Admin-Dashboard.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Z_Wallet.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="dashboard-finance">
                <div class="container-fluid dashboard-content">
                    <div class="row">
                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                            <div class="page-header">
                                <h3 class="mb-2">Dashboard</h3>
                                <div class="page-breadcrumb">
                                    <nav aria-label="breadcrumb">
                                        <ol class="breadcrumb">
                                            <li class="breadcrumb-item"><a href="#" class="breadcrumb-link">Dashboard</a></li>
                                            <li class="breadcrumb-item active" aria-current="page">Home</li>
                                        </ol>
                                    </nav>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Admin ID</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="adminIDLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-primary-light mt-1">
                                        <i class="fa fa-user fa-fw fa-sm text-primary"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Welcome Admin,</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="adminNameLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-primary-light mt-1">
                                        <i class="fa fa-user fa-fw fa-sm text-primary"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Admin Members</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="adminMembersLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-success-light mt-1">
                                        <i class="fa fa-user fa-fw fa-sm text-success"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr>
                    <div class="row">
                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Total Users</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="totalUsersLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-primary-light mt-1">
                                        <i class="fa fa-users fa-fw fa-sm text-primary"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Total Verified Members</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="verifiedMembersLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-success-light mt-1">
                                        <i class="fa fa-users fa-fw fa-sm text-success"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Total Unverified Members</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="unverfiedMembersLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-danger-light mt-1">
                                        <i class="fa fa-users fa-fw fa-sm text-danger"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Total Pending Members</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="pendingMembersLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-info-light mt-1">
                                        <i class="fa fa-users fa-fw fa-sm text-info"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Daily Transactions</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="dailyTransactionsLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-secondary-light mt-1">
                                        <i class="fa fa-chart-line fa-fw fa-sm text-secondary"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-inline-block">
                                        <h5 class="text-muted">Total Transactions</h5>
                                        <h2 class="mb-0">
                                            <asp:Label ID="totalTransactionsLabel" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="float-right icon-circle-medium  icon-box-lg  bg-secondary-light mt-1">
                                        <i class="fa fa-chart-line fa-fw fa-sm text-secondary"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
