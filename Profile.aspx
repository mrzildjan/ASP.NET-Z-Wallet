<%@ Page Title="" Language="C#" MasterPageFile="~/User-Dashboard.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Z_Wallet.Profile" %>

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
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card">
                                    <div class="card-header">
                                        <h4 class="mb-0">Profile Picture</h4>
                                    </div>
                                    <div class="card-body text-center">
                                        <div class="rounded-circle overflow-hidden profile-picture">
                                            <img id="previewImage" src="/Content/assets/images/2.jpg" alt="Profile Picture" class="img-fluid" style="max-width: 100%; height: auto;" runat="server" />
                                        </div>
                                        <br />
                                        <div class="d-flex justify-content-center">
                                            <div class="w-75">
                                                <asp:FileUpload ID="fileUpload" runat="server" CssClass="mb-3" onchange="previewFile()" Style="width: 100%;" />
                                            </div>
                                        </div>
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary mt-3" OnClick="btnUpload_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="card">
                                    <div class="card-header">
                                        <h4 class="mb-0">Profile Information</h4>
                                    </div>
                                    <div class="card-body">
                                        <form class="needs-validation" novalidate="">
                                            <div class="row">
                                                <div class="col-md-6 mb-3">
                                                    <label for="firstName">First name</label>
                                                    <input type="text" class="form-control" placeholder="" value="John" required="">
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="lastName">Last name</label>
                                                    <input type="text" class="form-control" placeholder="" value="Doe" required="">
                                                </div>
                                                <div class="col-md-12 mb-3">
                                                    <label for="lastName">Email</label>
                                                    <input type="text" class="form-control" placeholder="" value="john@gmail.com" required="">
                                                </div>
                                                <div class="col-md-12 mb-3">
                                                    <label for="lastName">Contact</label>
                                                    <input type="text" class="form-control" placeholder="" value="09897897877" required="">
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="firstName">Username</label>
                                                    <input type="text" class="form-control" placeholder="" value="John" required="">
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="lastName">Password</label>
                                                    <input type="password" class="form-control" placeholder="" value="**********" required="">
                                                </div>
                                            </div>

                                            <div class="form-group row text-right">
                                                <div class="col col-sm-10 col-lg-12 offset-lg-0">
                                                    <button type="submit" class="btn btn-space btn-primary">Update</button>
                                                    <button class="btn btn-space btn-secondary">Cancel</button>
                                                </div>
                                            </div>
                                        </form>
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
    </style>

    <script type="text/javascript">
        function previewFile() {
            var preview = document.getElementById("previewImage");
            var file = document.getElementById("fileUpload").files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
                preview.style.display = "block";
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                // Set the default profile picture path
                preview.src = "/Content/assets/images/2.jpg";
                preview.style.display = "block";
            }
        }
    </script>
</asp:Content>
