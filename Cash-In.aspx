<%@ Page Title="" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Cash-In.aspx.cs" Inherits="Z_Wallet.Cash_In" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-credit-card"></i>Deposit Transactions </h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="#" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="#" class="breadcrumb-link">Deposit</a></li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="card">
                            <h5 class="card-header">List of Deposit Transactions </h5>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table id="example" class="table table-striped table-bordered second" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>Transaction Code</th>
                                                <th>Email</th>
                                                <th>Deposit Amount</th>
                                                <th>Currency</th>
                                                <th>Date/Time</th>
                                                <th>Payment</th>
                                                <th>Status</th>
                                                <th>Remarks</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>TRSCTN-234-21</td>
                                                <td>john@gmail.com</td>
                                                <td>100</td>
                                                <td>USD</td>
                                                <td>12-12-21 10:30AM</td>
                                                <td>
                                                    <img src="../assets/images/paymaya.png" width="50" /></td>
                                                <td><span class="badge bg-success text-white">successful</span></td>
                                                <td>Remarks</td>
                                            </tr>
                                            <tr>
                                                <td>TRSCTN-375-21</td>
                                                <td>john@gmail.com</td>
                                                <td>100</td>
                                                <td>USD</td>
                                                <td>12-12-21 10:30AM</td>
                                                <td>
                                                    <img src="../assets/images/gcash.png" width="70" /></td>
                                                <td><span class="badge bg-info text-white">pending</span></td>
                                                <td>Remarks</td>
                                            </tr>
                                            <tr>
                                                <td>TRSCTN-897-21</td>
                                                <td>john@gmail.com</td>
                                                <td>100</td>
                                                <td>USD</td>
                                                <td>12-12-21 10:30AM</td>
                                                <td>
                                                    <img src="../assets/images/gcash.png" width="70" /></td>
                                                <td><span class="badge bg-success text-white">successful</span></td>
                                                <td>Remarks</td>
                                            </tr>
                                            <tr>
                                                <td>TRSCTN-567-21</td>
                                                <td>john@gmail.com</td>
                                                <td>100</td>
                                                <td>USD</td>
                                                <td>12-12-21 10:30AM</td>
                                                <td>
                                                    <img src="../assets/images/paypal.png" width="70" /></td>
                                                <td><span class="badge bg-success text-white">successful</span></td>
                                                <td>Remarks</td>
                                            </tr>
                                            <tr>
                                                <td>TRSCTN-345-21</td>
                                                <td>john@gmail.com</td>
                                                <td>100</td>
                                                <td>USD</td>
                                                <td>12-12-21 10:30AM</td>
                                                <td>
                                                    <img src="../assets/images/coins.ph.png" width="60" /></td>
                                                <td><span class="badge bg-danger text-white">rejected</span></td>
                                                <td>Remarks</td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th>Transaction Code</th>
                                                <th>Email</th>
                                                <th>Deposit Amount</th>
                                                <th>Currency</th>
                                                <th>Date/Time</th>
                                                <th>Payment</th>
                                                <th>Status</th>
                                                <th>Remarks</th>
                                            </tr>
                                        </tfoot>
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
