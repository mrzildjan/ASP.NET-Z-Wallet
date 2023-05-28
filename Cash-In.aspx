<%@ Page Title="Cash-In" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Cash-In.aspx.cs" Inherits="Z_Wallet.Cash_In" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-file"></i>Cash In</h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="/Dashboard" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="/Cash-In" class="breadcrumb-link">Cash In</a></li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 mx-auto">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">Current Balance</h4>
                            </div>
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <asp:Label ID="lblCurrentBalance" runat="server" CssClass="mb-0 font-weight-bold font-24" Text='&#8369;<%# Eval("CurrentBalance") %>'></asp:Label>
                                    <div class="text-success">
                                        <% decimal currentBalance = Convert.ToDecimal(lblCurrentBalance.Text.Replace("₱", ""));
                                            decimal maxBalance = 50000.0m;
                                            decimal progressPercentage = (currentBalance / maxBalance) * 100;
                                        %>
                                        <i class="fa fa-fw fa-arrow-up"></i><%: progressPercentage.ToString("0.00") %>%
                                    </div>
                                </div>
                                <div class="progress mt-3">
                                    <%-- Calculate the progress percentage based on the current balance --%>
                                    <% decimal currentBalanceInt = Convert.ToDecimal(lblCurrentBalance.Text.Replace("₱", ""));
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
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="depositAmount">Cash In Amount</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="depositAmount" runat="server" CssClass="form-control" placeholder="Enter amount"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <span class="input-group-text">PHP</span>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator ID="depositRequiredFieldValidator" runat="server" ControlToValidate="depositAmount" ErrorMessage="Enter a Cash-In Amount." CssClass="text-danger" ValidationGroup="cashInValidation" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="depositAmountRangeValidator" runat="server" ControlToValidate="depositAmount" Type="Double" ErrorMessage="Amount must be between PHP100 and PHP10000." MinimumValue="100" MaximumValue="10000" CssClass="text-danger" ValidationGroup="cashInValidation" Display="Dynamic" SetFocusOnError="true"></asp:RangeValidator>
                                            <asp:RegularExpressionValidator ID="depositAmountRegexValidator" runat="server" ControlToValidate="depositAmount" ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="<br />Amount must be between PHP100 and PHP10000." ValidationGroup="cashInValidation" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row text-right">
                                    <div class="col-lg-12">
                                        <asp:Button ID="btnCashin" runat="server" Text="Cash In" CssClass="btn btn-primary" ValidationGroup="cashInValidation" OnClick="btnCashin_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <div class="form-group row text-center">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblSuccessMessage" runat="server" CssClass="text-success font-16" Visible="false"></asp:Label>
                                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger font-16" Visible="false"></asp:Label>
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
