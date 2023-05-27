<%@ Page Title="Profile" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Z_Wallet.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="dashboard-wrapper">
            <div class="container-fluid dashboard-content">
                <div class="row">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="page-header">
                            <h2 class="pageheader-title"><i class="fa fa-fw fa-user"></i>My Profile </h2>
                            <div class="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="#" class="breadcrumb-link">Dashboard</a></li>
                                        <li class="breadcrumb-item"><a href="#" class="breadcrumb-link">Profile</a></li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">Profile Picture</h4>
                            </div>
                            <div class="card-body text-center">
                                <div class="rounded-circle overflow-hidden profile-picture">
                                    <asp:Image ID="previewImage" runat="server" CssClass="img-fluid" Style="max-width: 100%; height: auto;" ImageUrl="~/Content/assets/images/2.jpg" />
                                </div>
                                <br />
                                <div class="d-flex justify-content-center">
                                    <div class="w-75">
                                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="mb-3" Style="display: none;" onchange="previewFile()" />
                                    </div>
                                </div>
                                <asp:Button ID="btnChooseFile" runat="server" Text="Choose File" CssClass="btn btn-secondary mt-3 mr-1" OnClientClick="chooseFile();return false;" />
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary mt-3" OnClick="btnUpload_Click" />
                                <asp:RegularExpressionValidator ID="revImageFile" runat="server" ControlToValidate="fileUpload" ErrorMessage="<br />Invalid file format. Please choose an image." ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" CssClass="text-danger"></asp:RegularExpressionValidator>
                                <div class="form-group row text-center">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblAvatarSuccessMessage" runat="server" CssClass="text-success font-16" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">Account Status</h4>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label for="accountStatus">Status</label>
                                        <div class="status-indicator">
                                            <asp:Label ID="lblAccountStatus" runat="server" CssClass="status-text">Active</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row text-right">
                                    <div class="col col-sm-10 col-lg-12 offset-lg-0">
                                        <asp:Button ID="btnReactivate" runat="server" Text="Reactivate" CssClass="btn btn-primary mt-3 mr-1" OnClick="btnReactivate_Click" />
                                        <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" CssClass="btn btn-secondary mt-3" OnClick="btnDeactivate_Click" />
                                    </div>
                                </div>
                                <div class="form-group row text-center">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblStatusSuccessMessage" runat="server" CssClass="text-success font-16" Visible="false"></asp:Label>
                                        <asp:Label ID="lblStatusErrorMessage" runat="server" CssClass="text-danger font-16" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">Profile Information</h4>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label for="firstName">First name</label>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="lastName">Last name</label>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-12 mb-3">
                                        <label for="email">Email</label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-12 mb-3">
                                        <label for="contact">Contact</label>
                                        <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="numValidator" runat="server" ErrorMessage="<br />Invalid Phone Number!" ValidationExpression="^(09|\+639)\d{9}$" CssClass="text-danger" ControlToValidate="txtContact"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group row text-right">
                                    <div class="col col-sm-10 col-lg-12 offset-lg-0">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-space btn-primary" OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-space btn-secondary" OnClick="btnCancel_Click" />
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
                        <br />
                        <div class="card">
                            <div class="card-header">
                                <h4 class="mb-0">Change Password</h4>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label for="oldPassword">Old Password</label>
                                        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="newPassword">New Password</label>
                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="confirmNewPassword">Confirm New Password</label>
                                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder=""></asp:TextBox>
                                        <asp:CompareValidator ID="cvPasswordMatch" runat="server" ControlToValidate="txtConfirmNewPassword" ControlToCompare="txtNewPassword" ErrorMessage="Passwords do not match." CssClass="text-danger"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group row text-right">
                                    <div class="col col-sm-10 col-lg-12 offset-lg-0">
                                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-space btn-primary" OnClick="btnChangePassword_Click" />
                                        <asp:Button ID="btnCancelPassword" runat="server" Text="Cancel" CssClass="btn btn-space btn-secondary" OnClick="btnCancelPassword_Click" />
                                    </div>
                                </div>
                                <div class="form-group row text-center">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblPasswordSuccessMessage" runat="server" CssClass="text-success font-16" Visible="false"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblPasswordErrorMessage" runat="server" CssClass="text-danger font-16" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

    <style>
        .profile-picture {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            overflow: hidden;
            margin: 0 auto;
        }

            .profile-picture img {
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

        .status-active {
            background-color: #28a745; /* Green */
        }

        .status-inactive {
            background-color: #dc3545; /* Red */
        }
    </style>

    <script>
        function chooseFile() {
            var fileInput = document.getElementById('<%=fileUpload.ClientID%>');
            fileInput.click();
        }

        function previewFile() {
            var preview = document.getElementById('<%=previewImage.ClientID%>');
            var fileInput = document.getElementById('<%=fileUpload.ClientID%>');
            var file = fileInput.files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
                preview.style.display = "block";
            };

            if (file) {
                reader.readAsDataURL(file);
                document.getElementById('<%= lblAvatarSuccessMessage.ClientID %>').style.display = 'none';
            } else {
                preview.src = "";
                preview.style.display = "none";
            }
        }
    </script>
</asp:Content>
