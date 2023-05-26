<%@ Page Title="Cash-Out" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Cash-Out.aspx.cs" Inherits="Z_Wallet.Cash_Out" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <!-- Add the modal for password verification -->
        <div id="passwordModal" class="modal" tabindex="-1" role="dialog" aria-labelledby="passwordModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="passwordModalLabel">Verify Password</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="password">Password</label>
                            <asp:TextBox ID="password" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnVerifyPassword" runat="server" Text="Verify" CssClass="btn btn-primary" OnClick="btnVerifyPassword_Click" OnClientClick="return validateModalInput();" />
                        <asp:Button ID="btnCancelPassword" runat="server" Text="Cancel" CssClass="btn btn-secondary" data-dismiss="modal" />
                        <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" runat="server" ControlToValidate="password" ErrorMessage="Password is required" ValidationGroup="passwordValidation" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-file"></i>Cash Out</h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="/Dashboard" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="/Cash-Out" class="breadcrumb-link">Cash Out</a></li>
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
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="withdrawAmount">Cash Out Amount</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="withdrawAmount" runat="server" CssClass="form-control" placeholder="Enter amount"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <span class="input-group-text">PHP</span>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator ID="withdrawRequiredFieldValidator" runat="server" ControlToValidate="withdrawAmount" ErrorMessage="Enter a Cash-Out Amount." CssClass="text-danger" ValidationGroup="cashOutValidation" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="withdrawAmountRangeValidator" runat="server" ControlToValidate="withdrawAmount" Type="Double" ErrorMessage="Amount must be between PHP100 and PHP10000." MinimumValue="100" MaximumValue="10000" CssClass="text-danger" ValidationGroup="cashOutValidation" Display="Dynamic" SetFocusOnError="true"></asp:RangeValidator>
                                            <asp:RegularExpressionValidator ID="withdrawAmountRegexValidator" runat="server" ControlToValidate="withdrawAmount" ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="<br />Amount must be between PHP100 and PHP10000." ValidationGroup="cashOutValidation" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row text-right">
                                    <div class="d-flex justify-content-end col-lg-12">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <!-- Add ScriptManager for UpdatePanel -->
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnCashOut" runat="server" Text="Cash Out" CssClass="btn btn-primary mr-1" ValidationGroup="cashOutValidation" OnClick="btnCashOut_Click" OnClientClick="return validateCashOut();" data-toggle="modal" data-target="#passwordModal" />
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
        <script>
            function validatePassword() {
                var password = document.getElementById('<%= password.ClientID %>').value;
                if (password === '') {
                    alert('Please enter your password.');
                    return false;
                }
                return true;
            }

            function validateCashOut() {
                var withdrawAmount = document.getElementById('<%= withdrawAmount.ClientID %>').value;
                if (withdrawAmount === '') {
                    alert('Please enter a cash-out amount.');
                    return false;
                }

                var amount = parseFloat(withdrawAmount);
                if (isNaN(amount) || amount < 100 || amount > 10000) {
                    alert('Amount must be between 100 and 10,000.');
                    return false;
                }

                return true;
            }

            function validateModalInput() {
                var modalWithdrawAmount = document.getElementById('<%= withdrawAmount.ClientID %>').value;
                if (modalWithdrawAmount === '') {
                    alert('Please enter a cash-out amount.');
                    return false;
                }

                var amount = parseFloat(modalWithdrawAmount);
                if (isNaN(amount) || amount < 100 || amount > 10000) {
                    alert('Amount must be between 100 and 10,000.');
                    return false;
                }

                return true;
            }
        </script>
    </main>
</asp:Content>