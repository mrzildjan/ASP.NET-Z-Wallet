<%@ Page Title="" Language="C#" MasterPageFile="~/Admin-Dashboard.Master" AutoEventWireup="true" CodeBehind="User-Members.aspx.cs" Inherits="Z_Wallet.User_Members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-user"></i>Member Forms</h2>
                            <nav aria-label="breadcrumb">
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="\Admin" class="breadcrumb-link">Dashboard</a></li>
                                    <li class="breadcrumb-item"><a href="\Verification-Form" class="breadcrumb-link">Verification Form</a></li>
                                </ol>
                            </nav>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">Verification Status</h4>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 mb-3">
                                        <label for="accountStatus">Status</label>
                                        <div class="status-indicator">
                                            <asp:DropDownList ID="ddlAccountStatus" runat="server" CssClass='<%# GetStatusBadgeClass(ddlAccountStatus.SelectedValue) %>'
                                                OnSelectedIndexChanged="ddlAccountStatus_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Verified" Value="Verified"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                <asp:ListItem Text="Denied" Value="Denied"></asp:ListItem>
                                                <asp:ListItem Text="Suspended" Value="Suspended"></asp:ListItem>
                                                <asp:ListItem Text="Unverified" Value="Unverified"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 mb-3 text-center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-space btn-primary" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-space btn-secondary" OnClick="btnBack_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">ID Type</h4>
                            </div>
                            <div class="card-body">
                                <div class="col-md-12 mb-3">
                                    <label for="IDType">ID Type <span class="required">*</span></label>
                                    <asp:DropDownList ID="ddlIDType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select ID Type" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                        <asp:ListItem Text="Driver's License" Value="DriverLicense"></asp:ListItem>
                                        <asp:ListItem Text="Social Security System (SSS)" Value="SSS"></asp:ListItem>
                                        <asp:ListItem Text="Government Service Insurance System (GSIS)" Value="GSIS"></asp:ListItem>
                                        <asp:ListItem Text="Unified Multi-Purpose ID (UMID)" Value="UMID"></asp:ListItem>
                                        <asp:ListItem Text="Postal ID" Value="PostalID"></asp:ListItem>
                                        <asp:ListItem Text="Professional Regulation Commission (PRC)" Value="PRC"></asp:ListItem>
                                        <asp:ListItem Text="National Bureau of Investigation (NBI) Clearance" Value="NBI"></asp:ListItem>
                                        <asp:ListItem Text="Voter's ID" Value="VoterID"></asp:ListItem>
                                        <asp:ListItem Text="Barangay ID" Value="BarangayID"></asp:ListItem>
                                        <asp:ListItem Text="Police Clearance" Value="PoliceClearance"></asp:ListItem>
                                        <asp:ListItem Text="PhilHealth ID" Value="PhilHealth"></asp:ListItem>
                                        <asp:ListItem Text="Senior Citizen ID" Value="SeniorCitizen"></asp:ListItem>
                                        <asp:ListItem Text="Philippine Identification" Value="PhilID"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="card" id="cardfileUpload1" runat="server">
                            <div class="card-header">
                                <h4 class="mb-0">Front ID Picture <span class="required">*</span></h4>
                            </div>
                            <div class="card-body text-center">
                                <div class="rounded-circle overflow-hidden id-picture">
                                    <asp:Image ID="previewIDImage1" runat="server" CssClass="img-fluid" Style="object-fit: cover;" />
                                </div>
                                <br />
                                <div class="d-flex justify-content-center">
                                    <div class="w-75">
                                        <asp:FileUpload ID="fileUpload1" runat="server" CssClass="mb-3" Style="opacity: 0; position: absolute;" onchange="previewFile1()" />
                                    </div>
                                </div>
                                <asp:Button ID="btnChooseFile1" runat="server" Text="Choose File" CssClass="btn btn-secondary mt-3 mr-1" OnClientClick="chooseFile1(); return false;" />
                            </div>
                        </div>
                        <div class="card fileUpload2" id="cardfileUpload2" runat="server">
                            <div class="card-header">
                                <h4 class="mb-0">Back ID Picture <span class="required">*</span></h4>
                            </div>
                            <div class="card-body text-center">
                                <div class="rounded-circle overflow-hidden id-picture">
                                    <asp:Image ID="previewIDImage2" runat="server" CssClass="img-fluid" Style="object-fit: cover;" />
                                </div>
                                <br />
                                <div class="d-flex justify-content-center">
                                    <div class="w-75">
                                        <asp:FileUpload ID="fileUpload2" runat="server" CssClass="mb-3" Style="opacity: 0; position: absolute;" onchange="previewFile2()" />
                                    </div>
                                </div>
                                <asp:Button ID="btnChooseFile2" runat="server" Text="Choose File" CssClass="btn btn-secondary mt-3 mr-1" OnClientClick="chooseFile2(); return false;" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-8">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">Profile Information</h4>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label for="firstName">First name <span class="required">*</span></label>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="MiddleName">Middle name</label>
                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="LastName">Last name <span class="required">*</span></label>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="txtBirthDate">Birth Date <span class="required">*</span></label>
                                        <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" type="date" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="ddlGender">Gender <span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="Nationality">Nationality <span class="required">*</span></label>
                                        <asp:TextBox ID="txtNationality" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="PlaceOfBirth">Place of Birth <span class="required">*</span></label>
                                        <asp:TextBox ID="txtPlaceOfBirth" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="Religion">Religion <span class="required">*</span></label>
                                        <asp:TextBox ID="txtReligion" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">Address Information</h4>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label for="AddressLine1">Address Line 1 <span class="required">*</span></label>
                                        <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="AddressLine2">Address Line 2</label>
                                        <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="CityAddress">City <span class="required">*</span></label>
                                        <asp:TextBox ID="txtCityAddress" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="ProvinceAddress">Province <span class="required">*</span></label>
                                        <asp:TextBox ID="ProvinceAddress" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="PostalCode">Postal Code <span class="required">*</span></label>
                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="Country">Country <span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlCountries" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row text-right">
                                    <div class="col-12">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-space btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-space btn-secondary" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <div class="form-group row text-center">
                                    <div class="col-12">
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

        <style>
            .id-picture {
                width: 250px;
                height: 250px;
                margin: 0 auto;
            }

                .id-picture img {
                    width: 100%;
                    height: 100%;
                    object-fit: cover;
                }

            .custom-file-input {
                position: relative;
                overflow: hidden;
                margin-top: 8px;
            }

            .custom-file-label {
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                padding: 0.375rem 0.75rem;
                line-height: 1.5;
                color: #495057;
                background-color: #fff;
                border: 1px solid #ced4da;
                border-radius: 0.25rem;
                transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
                cursor: pointer;
                background-image: url('path/to/your/image.jpg');
                background-size: cover;
                background-position: center;
            }

            .custom-file-input:focus ~ .custom-file-label {
                border-color: #80bdff;
                box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
            }

            .status-indicator {
                display: inline-block;
                padding: 5px;
                border-radius: 5px;
            }

            .status-text {
                font-weight: bold;
                padding: 5px 5px;
                color: #fff;
            }

            .status-verified {
                background-color: #28a745; /* Green */
            }

            .status-pending {
                background-color: #dc3545; /* Red */
            }

            .required {
                color: red;
            }
        </style>

        <script>
            function chooseFile1() {
                var fileInput = document.getElementById('<%=fileUpload1.ClientID%>');
                fileInput.click();
            }

            function previewFile1() {
                var preview = document.getElementById('<%=previewIDImage1.ClientID%>');
                var fileInput = document.getElementById('<%=fileUpload1.ClientID%>');
                var file = fileInput.files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                    preview.style.display = "block";
                };

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                    preview.style.display = "none";
                }
            }

            function chooseFile2() {
                var fileInput = document.getElementById('<%=fileUpload2.ClientID%>');
                fileInput.click();
            }

            function previewFile2() {
                var preview = document.getElementById('<%=previewIDImage2.ClientID%>');
                var fileInput = document.getElementById('<%=fileUpload2.ClientID%>');
                var file = fileInput.files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                    preview.style.display = "block";
                };

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                    preview.style.display = "none";
                }
            }
        </script>
    </main>
</asp:Content>
