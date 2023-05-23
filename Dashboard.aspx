<%@ Page Title="" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Z_Wallet.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row justify-content-center">
                    <div class="col-lg-8">
                        <div class="card text-center">
                            <div class="card-header bg-light text-white">
                                <h3 class="mb-0">Account Information</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Account Number</div>
                                            <div class="card-body">
                                                <h4 class="mb-0">1234567890</h4>
                                            </div>
                                        </div>
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Name</div>
                                            <div class="card-body">
                                                <h4 class="mb-0">John Doe</h4>
                                            </div>
                                        </div>
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Date Registered</div>
                                            <div class="card-body">
                                                <h4 class="mb-0">May 23, 2023</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Current Balance</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <h1 class="mb-0">$9,000</h1>
                                                    <div class="text-success">
                                                        <i class="fa fa-fw fa-arrow-up"></i>25%
                                                    </div>
                                                </div>
                                                <div class="progress mt-3">
                                                    <div class="progress-bar bg-success" role="progressbar" style="width: 90%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Total Sent Money</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <h1 class="mb-0">$5,000</h1>
                                                    <div class="text-success">
                                                        <i class="fa fa-fw fa-arrow-up"></i>25%
                                                    </div>
                                                </div>
                                                <div class="progress mt-3">
                                                    <div class="progress-bar bg-success" role="progressbar" style="width: 25%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary">Number of Deposits</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <h1 class="mb-0">132</h1>
                                                    <div class="text-success">
                                                        <i class="fa fa-fw fa-arrow-up"></i>25%
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary">Number of Withdrawals</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <h1 class="mb-0">20</h1>
                                                    <div class="text-danger">
                                                        <i class="fa fa-fw fa-arrow-down"></i>15%
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer">
                                <a href="#" class="card-link">View Details</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
