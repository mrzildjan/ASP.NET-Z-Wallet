<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Z_Wallet.Dashboard" %>

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
                                                <asp:Label ID="lblAccountNumber" runat="server" CssClass="mb-0 font-weight-bold font-20"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Name</div>
                                            <div class="card-body">
                                                <asp:Label ID="lblName" runat="server" CssClass="mb-0 font-weight-bold font-18"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Date Registered</div>
                                            <div class="card-body">
                                                <asp:Label ID="lblDateRegistered" runat="server" CssClass="mb-0 font-weight-bold font-18"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Current Balance</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <asp:Label ID="lblCurrentBalance" runat="server" CssClass="mb-0 font-weight-bold font-24" Text='&#8369;<%# Eval("CurrentBalance") %>'></asp:Label>
                                                    <div class="text-success">
                                                        <% decimal currentBalance = Convert.ToDecimal(lblCurrentBalance.Text);
                                                            decimal maxBalance = 50000.0m;
                                                            decimal progressPercentage = (currentBalance / maxBalance) * 100;
                                                        %>
                                                        <i class="fa fa-fw fa-arrow-up"></i><%: progressPercentage.ToString("0.00") %>%
                                                    </div>
                                                </div>
                                                <div class="progress mt-3">
                                                    <%-- Calculate the progress percentage based on the current balance --%>
                                                    <% decimal currentBalanceInt = Convert.ToDecimal(lblCurrentBalance.Text);
                                                        decimal maxBalanceInt = 50000m;
                                                        decimal progressPercentageInt = (currentBalanceInt / maxBalanceInt * 100);
                                                        if (progressPercentageInt > 100)
                                                        {
                                                            progressPercentageInt = 100;
                                                        }

                                                        // Check if progress is 90% or more and apply different class based on it
                                                        string progressClass = progressPercentageInt >= 90 ? "bg-danger" : "bg-success";
                                                    %>
                                                    <div class="progress-bar <%: progressClass %>" role="progressbar" style="width: <%: progressPercentageInt %>%;" aria-valuenow="<%: progressPercentageInt %>" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Total Sent Money</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <asp:Label ID="lblTotalSendMoney" runat="server" CssClass="mb-0 font-weight-bold font-24"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary text-white">Total Receive Money</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <asp:Label ID="lblTotalReceiveMoney" runat="server" CssClass="mb-0 font-weight-bold font-24"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary">Total Cash In</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <asp:Label ID="lblTotalCashIn" runat="server" CssClass="mb-0 font-weight-bold font-24"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card mb-3">
                                            <div class="card-header bg-primary">Total Cash Out</div>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <asp:Label ID="lblTotalCashout" runat="server" CssClass="mb-0 font-weight-bold font-24"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer">
                                <a href="\Profile" class="card-link">View Details</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
