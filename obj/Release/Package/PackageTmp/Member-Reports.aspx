<%@ Page Title="Members Reports" Language="C#" MasterPageFile="~/Admin-Dashboard.Master" AutoEventWireup="true" CodeBehind="Member-Reports.aspx.cs" Inherits="Z_Wallet.Member_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-piggy-bank"></i>Member Reports</h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="/Admin" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="/Member-Reports" class="breadcrumb-link">Member Reports</a></li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="card">
                            <h5 class="card-header">Filter Transactions</h5>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="fromDate">From Date:</label>
                                        <asp:TextBox ID="fromDate" runat="server" CssClass="form-control" TextMode="Date" onchange="updateToDate();"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="toDate">To Date:</label>
                                        <asp:TextBox ID="toDate" runat="server" CssClass="form-control" TextMode="Date" onchange="updateToDate();"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row text-center">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblFilterSuccessMessage" runat="server" CssClass="text-success font-16" Visible="false"></asp:Label>
                                        <asp:Label ID="lblFilterErrorMessage" runat="server" CssClass="text-danger font-16" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-12">
                                        <div class="d-flex justify-content-end">
                                            <asp:Button ID="btnApplyFilter" runat="server" Text="Apply" CssClass="btn btn-primary mr-2" OnClick="btnApplyFilter_Click" OnClientClick="disableEarlierDates(); return validateFilter();" />
                                            <asp:Button ID="btnResetFilter" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnResetFilter_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="card">
                            <h5 class="card-header">List of Withdraw Transactions</h5>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table id="example" class="table table-striped table-bordered second" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>Transaction Code</th>
                                                <th>Type</th>
                                                <th>Sender</th>
                                                <th>Receiver</th>
                                                <th>Amount</th>
                                                <th>Time Date</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptTransactions" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("TransactionID") %></td>
                                                        <td><%# Eval("TransactionType") %></td>
                                                        <td><%# Eval("TransactionSender") == string.Empty ? "Not Applicable" : Eval("TransactionSender") %></td>
                                                        <td><%# Eval("TransactionReceiver") == string.Empty ? "Not Applicable" : Eval("TransactionReceiver") %></td>
                                                        <td><%# Eval("TransactionAmount") %></td>
                                                        <td><%# Convert.ToDateTime(Eval("TransactionDate")).ToString("dd-MM-yy hh:mm tt") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
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
    <script type="text/javascript">
        var toDateInput = document.getElementById('<%= toDate.ClientID %>');

        function updateToDate() {
            var fromDate = document.getElementById('<%= fromDate.ClientID %>');

            toDateInput.min = fromDate.value;

            if (toDateInput.value < toDateInput.min) {
                toDateInput.value = toDateInput.min;
            }
        }

        function disableEarlierDates() {
            var fromDate = document.getElementById('<%= fromDate.ClientID %>');

            if (toDateInput.value < fromDate.value) {
                toDateInput.value = fromDate.value;
            }

            toDateInput.min = fromDate.value;
        }
    </script>
</asp:Content>
