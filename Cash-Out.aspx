<%@ Page Title="" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Cash-Out.aspx.cs" Inherits="Z_Wallet.Cash_Out" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid  dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-money-bill-alt"></i>Withdraw Money </h2>
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
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header">
                                        <h4 class="mb-0">Withdraw Money</h4>
                                        <div class="">
                                            <a href="#" class="btn btn-brand" data-toggle="modal" data-target="#exampleModal"><i class="fa fa-fw fa-plus"></i>Add Method</a>
                                            <!-- Modal -->
                                            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Withdraw Amount</h5>
                                                            <a href="#" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </a>
                                                        </div>
                                                        <div class="modal-body">
                                                            <form class="needs-validation" novalidate="">
                                                                <div class="row">
                                                                    <div class="col-md-12 mb-3">
                                                                        <label for="lastName">Amount</label>
                                                                        <input type="number" class="form-control" placeholder="5000" required="">
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row text-right">
                                                                    <div class="col col-sm-10 col-lg-12 offset-lg-0">
                                                                        <button type="submit" class="btn btn-space btn-primary btn-block">Submit</button>
                                                                    </div>
                                                                </div>
                                                            </form>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                                                <div class="card">
                                                    <div class="card-body">
                                                        <div class="metric-value d-inline-block">
                                                            <div class="row">
                                                                <div class="col-lg-4">
                                                                    <img src="../assets/images/gcash.png" width="150" />
                                                                </div>
                                                                <div class="col-lg-6" style="margin-left: 10px">
                                                                    <p>John Doe</p>
                                                                    <p>Limit: 50,000 - 100,000 USD</p>
                                                                    <p>Charge: 50 USD +3%</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                                                <div class="card">
                                                    <div class="card-body">
                                                        <div class="metric-value d-inline-block">
                                                            <div class="metric-value d-inline-block">
                                                                <div class="row">
                                                                    <div class="col-lg-4">
                                                                        <img src="../assets/images/coins.ph.png" width="100" />
                                                                    </div>
                                                                    <div class="col-lg-6" style="margin-left: 10px">
                                                                        <p>John Doe</p>
                                                                        <p>Limit: 50,000 - 100,000 USD</p>
                                                                        <p>Charge: 50 USD +3%</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                                                <div class="card">
                                                    <div class="card-body">
                                                        <div class="metric-value d-inline-block">
                                                            <div class="metric-value d-inline-block">
                                                                <div class="row">
                                                                    <div class="col-lg-4">
                                                                        <img src="../assets/images/paymaya.png" width="110" />
                                                                    </div>
                                                                    <div class="col-lg-6" style="margin-left: 10px">
                                                                        <p>John Doe</p>
                                                                        <p>Limit: 50,000 - 100,000 USD</p>
                                                                        <p>Charge: 50 USD +3%</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 col-12">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="metric-value d-inline-block">
                                                        <div class="metric-value d-inline-block">
                                                            <div class="row">
                                                                <div class="col-lg-4">
                                                                    <img src="../assets/images/paypal.png" width="130" />
                                                                </div>
                                                                <div class="col-lg-6" style="margin-left: 10px">
                                                                    <p>John Doe</p>
                                                                    <p>Limit: 50,000 - 100,000 USD</p>
                                                                    <p>Charge: 50 USD +3%</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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
