<%@ Page Title="" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Z_Wallet.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-piggy-bank"></i>Withdrawal Transactions</h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="#" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="#" class="breadcrumb-link">Withdraw</a></li>
                                    </ol>
                                </nav>
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
                                                <th>Account Number</th>
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
                                                        <td><%# Eval("AccountNumber") %></td>
                                                        <td><%# Eval("TransactionType") %></td>
                                                        <td><%# Eval("TransactionSender") %></td>
                                                        <td><%# Eval("TransactionReceiver") %></td>
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
</asp:Content>
